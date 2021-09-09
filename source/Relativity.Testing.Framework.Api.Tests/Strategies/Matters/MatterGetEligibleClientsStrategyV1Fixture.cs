using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(MatterGetEligibleClientsStrategyV1))]
	internal class MatterGetEligibleClientsStrategyV1Fixture
	{
		private const string _GET_ALL_URL = "relativity-environment/v1/workspaces/-1/eligible-clients";
		private readonly ArtifactIdNamePair[] _expectedClientsResponse = new[]
		{
			new ArtifactIdNamePair
			{
				ArtifactID = 1,
				Name = "Test Client Name"
			}
		};

		private Mock<IRestService> _mockRestService;
		private MatterGetEligibleClientsStrategyV1 _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();

			_sut = new MatterGetEligibleClientsStrategyV1(_mockRestService.Object);
		}

		[Test]
		public void GetAll_ShouldCallRestServiceWithExpectedUrl()
		{
			_sut.GetAll();
			VerifyRestServiceGetAsyncWasCalled();
		}

		[Test]
		public void GetAll_ShouldReturnRestServiceResponse()
		{
			SetupMockRestService();

			ArtifactIdNamePair[] result = _sut.GetAll();

			result.Should().BeEquivalentTo(_expectedClientsResponse);
		}

		private void SetupMockRestService()
		{
			_mockRestService
				.Setup(restService => restService.Get<ArtifactIdNamePair[]>(_GET_ALL_URL, null))
				.Returns(_expectedClientsResponse);
		}

		private void VerifyRestServiceGetAsyncWasCalled()
		{
			_mockRestService.Verify(restService => restService.Get<ArtifactIdNamePair[]>(_GET_ALL_URL, null), Times.Once);
		}
	}
}
