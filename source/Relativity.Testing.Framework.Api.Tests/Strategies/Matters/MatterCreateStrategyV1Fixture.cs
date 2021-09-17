using System;
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
		private Mock<IMatterGetByIdStrategy> _mockMatterGetByIdStrategy;
		private MatterCreateStrategyV1 _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockMatterStatusGetChoiceIdByNameStrategy = new Mock<IMatterStatusGetChoiceIdByNameStrategy>();
			_mockMatterStatusGetChoiceIdByNameStrategy.
				Setup(getStatusStrategy => getStatusStrategy.GetId(_STATUS)).Returns(_STATUS_ID);
			_mockClientRequireStrategy = new Mock<IRequireStrategy<Client>>();
			_mockMatterGetByIdStrategy = new Mock<IMatterGetByIdStrategy>();

			var requiredClient = new Client
			{
				ArtifactID = _CLIENT_ID,
				Name = _CLIENT_NAME
			};
			_mockClientRequireStrategy.
				Setup(clientRequireStrategy => clientRequireStrategy.Require(_client)).Returns(requiredClient);

			_sut = new MatterCreateStrategyV1(_mockRestService.Object, _mockMatterStatusGetChoiceIdByNameStrategy.Object, _mockClientRequireStrategy.Object, _mockMatterGetByIdStrategy.Object);
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
			MatterCreateRequestV1 expectedCreateRequest = GetExpectedMatterCreateRequestV1(matter);
			SetupRestServicePostResponse(expectedCreateRequest);

			_sut.Create(matter);

			VerifyRestServicePostWasCalled(expectedCreateRequest);
		}

		private static MatterCreateRequestV1 GetExpectedMatterCreateRequestV1(Matter matter)
		{
			var expectedCreateRequest = new MatterCreateRequestV1(matter, _STATUS_ID);
			expectedCreateRequest.MatterRequest.Client.Value = new Artifact(_CLIENT_ID);
			expectedCreateRequest.MatterRequest.Status.Value = new Artifact(_STATUS_ID);
			return expectedCreateRequest;
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

		private void SetupRestServicePostResponse(MatterCreateRequestV1 expectedCreateRequest)
		{
			_mockRestService.
				Setup(restService => restService.Post<int>(_CREATE_URL, It.Is<MatterCreateRequestV1>(e => CompareMatterCreateRequestV1(e, expectedCreateRequest)), 2, null)).Returns(_MATTER_ID);
		}

		private void VerifyRestServicePostWasCalled(MatterCreateRequestV1 expectedCreateRequest)
		{
			_mockRestService.Verify(restService => restService.Post<int>(_CREATE_URL, It.Is<MatterCreateRequestV1>(e => CompareMatterCreateRequestV1(e, expectedCreateRequest)), 2, null), Times.Once);
		}

		private static bool CompareMatterCreateRequestV1(MatterCreateRequestV1 entity, MatterCreateRequestV1 expectedCreateRequest)
		{
			return entity.MatterRequest.Name.Equals(expectedCreateRequest.MatterRequest.Name) &&
				entity.MatterRequest.Number.Equals(expectedCreateRequest.MatterRequest.Number) &&
				entity.MatterRequest.Keywords.Equals(expectedCreateRequest.MatterRequest.Keywords) &&
				entity.MatterRequest.Notes.Equals(expectedCreateRequest.MatterRequest.Notes) &&
				entity.MatterRequest.Status.Value.ArtifactID == expectedCreateRequest.MatterRequest.Status.Value.ArtifactID &&
				entity.MatterRequest.Client.Value.ArtifactID == expectedCreateRequest.MatterRequest.Client.Value.ArtifactID;
		}
	}
}
