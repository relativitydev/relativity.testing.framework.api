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
	[TestOf(typeof(MatterGetEligibleClientsStrategyV1))]
	internal class MatterGetEligibleClientsStrategyV1Fixture
	{
		private readonly string _getAllAsyncUrl = "relativity-environment/v1/workspaces/-1/eligible-clients";

		private Mock<IRestService> _mockRestService;
		private MatterGetEligibleClientsStrategyV1 _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();

			_sut = new MatterGetEligibleClientsStrategyV1(_mockRestService.Object);
		}

		[Test]
		public async Task GetAllAsync_ShouldCallRestServiceWithExpectedUrl()
		{
			await _sut.GetAllAsync().ConfigureAwait(false);
			_mockRestService.Verify(restService => restService.GetAsync<ArtifactIdNamePair[]>(_getAllAsyncUrl, null), Times.Once);
		}

		[Test]
		public async Task GetAllAsync_ShouldReturnRestServiceResponse()
		{
			var testStatus = new ArtifactIdNamePair
			{
				ArtifactID = 1,
				Name = "Test Client Name"
			};
			ArtifactIdNamePair[] expectedResponse = new[] { testStatus };
			_mockRestService
				.Setup(restService => restService.GetAsync<ArtifactIdNamePair[]>(_getAllAsyncUrl, null))
				.Returns(Task.FromResult(expectedResponse));

			ArtifactIdNamePair[] result = await _sut.GetAllAsync().ConfigureAwait(false);

			result.Should().BeEquivalentTo(expectedResponse);
		}
	}
}
