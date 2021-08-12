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
	[TestOf(typeof(MatterCreateStrategyV1))]
	internal class MatterCreateStrategyV1Fixture
	{
		private const string _CREATE_URL = "relativity-environment/v1/workspaces/-1/matters";
		private const string _CLIENT_NAME = "Test Client Name";
		private const string _STATUS = "Test Status";
		private const int _CLIENT_ID = 1;
		private const int _STATUS_ID = 2;
		private const int _MATTER_ID = 3;

		private readonly Client _client = new Client { Name = _CLIENT_NAME };

		private Mock<IRestService> _mockRestService;
		private Mock<IMatterStatusGetChoiceIdByNameStrategy> _mockMatterStatusGetChoiceIdByNameStrategy;
		private Mock<IRequireStrategy<Client>> _mockClientRequireStrategy;
		private MatterCreateStrategyV1 _sut;

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

			_sut = new MatterCreateStrategyV1(_mockRestService.Object, _mockMatterStatusGetChoiceIdByNameStrategy.Object, _mockClientRequireStrategy.Object);
		}

		[Test]
		public void Create_WithNull_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.Create(null));
		}

		[Test]
		public void Create_WithValidEntity_ShouldCallRestServiceWithExpectedUrl()
		{
			Matter matter = GetTestMatter();
			MatterDtoV1 expectedDto = GetExpectedMatterDtoV1(matter);
			SetupRestServicePostResponse(expectedDto);

			_sut.Create(matter);

			VerifyRestServicePostAsyncWasCalled(expectedDto);
		}

		[Test]
		public void Create_WithValidEntity_ShouldReturnEntityWithFilledArtifactId()
		{
			Matter matter = GetTestMatter();
			MatterDtoV1 expectedDto = GetExpectedMatterDtoV1(matter);
			SetupRestServicePostResponse(expectedDto);

			Matter result = _sut.Create(matter);

			result.ArtifactID.Should().Be(_MATTER_ID);
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
			MatterDtoV1 expectedDto = GetExpectedMatterDtoV1(matter);
			SetupRestServicePostResponse(expectedDto);

			await _sut.CreateAsync(matter).ConfigureAwait(false);

			VerifyRestServicePostAsyncWasCalled(expectedDto);
		}

		[Test]
		public async Task CreateAsync_WithValidEntity_ShouldReturnEntityWithFilledArtifactId()
		{
			Matter matter = GetTestMatter();
			MatterDtoV1 expectedDto = GetExpectedMatterDtoV1(matter);
			SetupRestServicePostResponse(expectedDto);

			Matter result = await _sut.CreateAsync(matter).ConfigureAwait(false);

			result.ArtifactID.Should().Be(_MATTER_ID);
		}

		private static MatterDtoV1 GetExpectedMatterDtoV1(Matter matter)
		{
			var expectedDto = new MatterDtoV1(matter, _STATUS_ID);
			expectedDto.MatterRequest.Client.Value = new Artifact(_CLIENT_ID);
			expectedDto.MatterRequest.Status.Value = new Artifact(_STATUS_ID);
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

		private void SetupRestServicePostResponse(MatterDtoV1 expectedDto)
		{
			_mockRestService.
				Setup(restService => restService.PostAsync<int>(_CREATE_URL, It.Is<MatterDtoV1>(e => CompareMatterDtoV1(e, expectedDto)), 2, null)).Returns(Task.FromResult(_MATTER_ID));
		}

		private void VerifyRestServicePostAsyncWasCalled(MatterDtoV1 expectedDto)
		{
			_mockRestService.Verify(restService => restService.PostAsync<int>(_CREATE_URL, It.Is<MatterDtoV1>(e => CompareMatterDtoV1(e, expectedDto)), 2, null), Times.Once);
		}

		private static bool CompareMatterDtoV1(MatterDtoV1 entity, MatterDtoV1 expectedDto)
		{
			return entity.MatterRequest.Name.Equals(expectedDto.MatterRequest.Name) &&
				entity.MatterRequest.Number.Equals(expectedDto.MatterRequest.Number) &&
				entity.MatterRequest.Keywords.Equals(expectedDto.MatterRequest.Keywords) &&
				entity.MatterRequest.Notes.Equals(expectedDto.MatterRequest.Notes) &&
				entity.MatterRequest.Status.Value.ArtifactID == expectedDto.MatterRequest.Status.Value.ArtifactID &&
				entity.MatterRequest.Client.Value.ArtifactID == expectedDto.MatterRequest.Client.Value.ArtifactID;
		}
	}
}
