using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ClientGetEligibleStatusesStrategyV1))]
	internal class ClientGetEligibleStatusesStrategyV1Fixture
	{
		private const string _GET_URL = "relativity-identity/v1/workspaces/-1/clients/eligible-statuses";
		private readonly IEnumerable<NamedArtifact> _expectedStatusesResponse = new List<NamedArtifact>
		{
			new NamedArtifact
			{
				ArtifactID = 1,
				Name = "Test Status Name"
			}
		};

		private Mock<IRestService> _mockRestService;
		private ClientGetEligibleStatusesStrategyV1 _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_sut = new ClientGetEligibleStatusesStrategyV1(_mockRestService.Object);
		}

		[Test]
		public void Get_ShouldCallRestServiceWithExpectedUrl()
		{
			_sut.Get();
			_mockRestService.Verify(restService => restService.Get<IEnumerable<NamedArtifact>>(_GET_URL, null), Times.Once);
		}

		[Test]
		public void Get_ShouldReturnRestServiceResponse()
		{
			SetupMockRestService();

			IEnumerable<NamedArtifact> result = _sut.Get();

			result.Should().BeEquivalentTo(_expectedStatusesResponse);
		}

		private void SetupMockRestService()
		{
			_mockRestService
				.Setup(restService => restService.Get<IEnumerable<NamedArtifact>>(_GET_URL, null))
				.Returns(_expectedStatusesResponse);
		}
	}
}
