using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(MatterCreateStrategyPreOsier))]
	internal class MatterCreateStrategyPreOsierFixture
	{
		private const string _CREATE_URL = "Relativity.Services.Matter.IMatterModule/Matter%20Manager/CreateSingleAsync";

		private const string _CLIENT_NAME = "Test Client Name";
		private const string _STATUS = "Test Status";
		private const int _CLIENT_ID = 1;
		private const int _STATUS_ID = 2;
		private const int _MATTER_ID = 3;

		private readonly Client _client = new Client { Name = _CLIENT_NAME };

		private Mock<IRestService> _mockRestService;
		private Mock<IMatterStatusGetChoiceIdByNameStrategy> _mockMatterStatusGetChoiceIdByNameStrategy;
		private Mock<IRequireStrategy<Client>> _mockClientRequireStrategy;
		private MatterCreateStrategyPreOsier _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockMatterStatusGetChoiceIdByNameStrategy = new Mock<IMatterStatusGetChoiceIdByNameStrategy>();
			_mockMatterStatusGetChoiceIdByNameStrategy.
				Setup(getStatusStrategy => getStatusStrategy.GetId(_STATUS)).Returns(_STATUS_ID);
			_mockClientRequireStrategy = new Mock<IRequireStrategy<Client>>();
			var requiredClient = new Client
			{
				ArtifactID = _CLIENT_ID,
				Name = _CLIENT_NAME
			};
			_mockClientRequireStrategy.
				Setup(clientRequireStrategy => clientRequireStrategy.Require(_client)).Returns(requiredClient);

			_sut = new MatterCreateStrategyPreOsier(_mockRestService.Object, _mockMatterStatusGetChoiceIdByNameStrategy.Object, _mockClientRequireStrategy.Object);
		}

		[Test]
		public void CreateAsync_WithNull_ThrowsArgumentNullException()
		{
			Assert.ThrowsAsync<ArgumentNullException>(() =>
				_sut.CreateAsync(null));
		}

		[Test]
		public async Task CreateAsync_WithValidEntity_ShouldCallRestServiceWithExpectedUrl()
		{
			Matter matter = GetTestMatter();
			MatterDtoPreOsier expectedDto = GetExpectedMatterDtoPreOsier(matter);
			SetupRestServicePostResponse(expectedDto);

			await _sut.CreateAsync(matter).ConfigureAwait(false);

			_mockRestService.Verify(restService => restService.PostAsync<int>(_CREATE_URL, It.Is<MatterDtoPreOsier>(e => CompareMatterDtoPreOsier(e, expectedDto)), 2, null), Times.Once);
		}

		[Test]
		public async Task CreateAsync_WithValidEntity_ShouldReturnEntityWithFilledArtifactId()
		{
			Matter matter = GetTestMatter();
			MatterDtoPreOsier expectedDto = GetExpectedMatterDtoPreOsier(matter);
			SetupRestServicePostResponse(expectedDto);

			Matter result = await _sut.CreateAsync(matter).ConfigureAwait(false);

			result.ArtifactID.Should().Be(_MATTER_ID);
		}

		private static MatterDtoPreOsier GetExpectedMatterDtoPreOsier(Matter matter)
		{
			var expectedDto = new MatterDtoPreOsier(matter, _STATUS_ID);
			expectedDto.MatterDTO.Client.ArtifactID = _CLIENT_ID;
			expectedDto.MatterDTO.Status.ArtifactID = _STATUS_ID;
			return expectedDto;
		}

		private Matter GetTestMatter()
		{
			return new Matter
			{
				Name = "Test Matter Name",
				Number = "3",
				Status = _STATUS,
				Client = _client,
				Keywords = "Test Keywords",
				Notes = "Test Notes"
			};
		}

		private void SetupRestServicePostResponse(MatterDtoPreOsier expectedDto)
		{
			_mockRestService.
				Setup(restService => restService.PostAsync<int>(_CREATE_URL, It.Is<MatterDtoPreOsier>(e => CompareMatterDtoPreOsier(e, expectedDto)), 2, null)).Returns(Task.FromResult(_MATTER_ID));
		}

		private static bool CompareMatterDtoPreOsier(MatterDtoPreOsier entity, MatterDtoPreOsier expectedDto)
		{
			return entity.MatterDTO.Name.Equals(expectedDto.MatterDTO.Name) &&
				entity.MatterDTO.Number.Equals(expectedDto.MatterDTO.Number) &&
				entity.MatterDTO.Keywords.Equals(expectedDto.MatterDTO.Keywords) &&
				entity.MatterDTO.Notes.Equals(expectedDto.MatterDTO.Notes) &&
				entity.MatterDTO.Status.ArtifactID == expectedDto.MatterDTO.Status.ArtifactID &&
				entity.MatterDTO.Client.ArtifactID == expectedDto.MatterDTO.Client.ArtifactID;
		}
	}
}
