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
	[TestOf(typeof(MatterGetEligibleStatusesStrategyPreOsier))]
	internal class MatterGetEligibleStatusesStrategyPreOsierFixture
	{
		private readonly string _getAllAsyncUrl = "Relativity.Services.Matter.IMatterModule/Matter%20Manager/GetStatusChoicesForMatterAsync";

		private Mock<IRestService> _mockRestService;
		private MatterGetEligibleStatusesStrategyPreOsier _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();

			_sut = new MatterGetEligibleStatusesStrategyPreOsier(_mockRestService.Object);
		}

		[Test]
		public async Task GetAllAsync_ShouldCallRestServiceWithExpectedUrl()
		{
			await _sut.GetAllAsync().ConfigureAwait(false);
			_mockRestService.Verify(restService => restService.PostAsync<ArtifactIdNamePair[]>(_getAllAsyncUrl, null, 2, null), Times.Once);
		}

		[Test]
		public async Task GetAllAsync_ShouldReturnRestServiceResponse()
		{
			var testStatus = new ArtifactIdNamePair
			{
				ArtifactID = 1,
				Name = "Test Status Name"
			};
			ArtifactIdNamePair[] expectedResponse = new[] { testStatus };
			_mockRestService
				.Setup(restService => restService.PostAsync<ArtifactIdNamePair[]>(_getAllAsyncUrl, null, 2, null))
				.Returns(Task.FromResult(expectedResponse));

			ArtifactIdNamePair[] result = await _sut.GetAllAsync().ConfigureAwait(false);

			result.Should().BeEquivalentTo(expectedResponse);
		}
	}
}
