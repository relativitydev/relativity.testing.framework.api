using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(MatterGetEligibleStatusesStrategyV1))]
	public class MatterGetEligibleStatusesStrategyV1Fixture
	{
		private const string _GET_ALL_URL = "relativity-environment/v1/workspaces/-1/matters/eligible-statuses";
		private readonly ArtifactIdNamePair[] _expectedStatusesResponse = new[]
		{
			new ArtifactIdNamePair
			{
				ArtifactID = 1,
				Name = "Test Status Name"
			}
		};

		private Mock<IRestService> _mockRestService;
		private MatterGetEligibleStatusesStrategyV1 _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();

			_sut = new MatterGetEligibleStatusesStrategyV1(_mockRestService.Object);
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
			SetupRestServiceMock();

			ArtifactIdNamePair[] result = _sut.GetAll();

			result.Should().BeEquivalentTo(_expectedStatusesResponse);
		}

		private void VerifyRestServiceGetAsyncWasCalled()
		{
			_mockRestService.Verify(restService => restService.Get<ArtifactIdNamePair[]>(_GET_ALL_URL, null), Times.Once);
		}

		private void SetupRestServiceMock()
		{
			_mockRestService
				.Setup(restService => restService.Get<ArtifactIdNamePair[]>(_GET_ALL_URL, null))
				.Returns(_expectedStatusesResponse);
		}
	}
}
