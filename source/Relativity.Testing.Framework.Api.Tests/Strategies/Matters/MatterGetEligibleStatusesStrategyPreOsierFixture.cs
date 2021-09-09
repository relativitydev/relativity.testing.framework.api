using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(MatterGetEligibleStatusesStrategyPreOsier))]
	internal class MatterGetEligibleStatusesStrategyPreOsierFixture
	{
		private const string _GET_ALL_URL = "Relativity.Services.Matter.IMatterModule/Matter%20Manager/GetStatusChoicesForMatterAsync";
		private readonly ArtifactIdNamePair[] _expectedStatusesResponse = new[]
		{
			new ArtifactIdNamePair
			{
				ArtifactID = 1,
				Name = "Test Status Name"
			}
		};

		private Mock<IRestService> _mockRestService;
		private MatterGetEligibleStatusesStrategyPreOsier _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();

			_sut = new MatterGetEligibleStatusesStrategyPreOsier(_mockRestService.Object);
		}

		[Test]
		public void GetAll_ShouldCallRestServiceWithExpectedUrl()
		{
			_sut.GetAll();
			VerifyRestServicePostAsyncWasCalled();
		}

		[Test]
		public void GetAll_ShouldReturnRestServiceResponse()
		{
			SetupMockRestService();

			ArtifactIdNamePair[] result = _sut.GetAll();

			result.Should().BeEquivalentTo(_expectedStatusesResponse);
		}

		private void VerifyRestServicePostAsyncWasCalled()
		{
			_mockRestService.Verify(restService => restService.Post<ArtifactIdNamePair[]>(_GET_ALL_URL, null, 2, null), Times.Once);
		}

		private void SetupMockRestService()
		{
			_mockRestService
				.Setup(restService => restService.Post<ArtifactIdNamePair[]>(_GET_ALL_URL, null, 2, null))
				.Returns(_expectedStatusesResponse);
		}
	}
}
