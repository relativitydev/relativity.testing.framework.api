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
	[TestOf(typeof(ClientGetEligibleStatusesStrategyPreOsier))]
	internal class ClientGetEligibleStatusesStrategyPreOsierFixture
	{
		private const string _GET_URL = "/Relativity.Services.Client.IClientModule/Client%20Manager/GetStatusChoicesForClientAsync";
		private readonly List<ArtifactIdNamePair> _expectedStatusesResponse = new List<ArtifactIdNamePair>
		{
			new ArtifactIdNamePair
			{
				ArtifactID = 1,
				Name = "Test Status Name"
			}
		};

		private Mock<IRestService> _mockRestService;
		private ClientGetEligibleStatusesStrategyPreOsier _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_sut = new ClientGetEligibleStatusesStrategyPreOsier(_mockRestService.Object);
		}

		[Test]
		public void Get_ShouldCallRestServiceWithExpectedUrl()
		{
			_sut.Get();
			_mockRestService.Verify(restService => restService.Get<IList<ArtifactIdNamePair>>(_GET_URL, null), Times.Once);
		}

		[Test]
		public void Get_ShouldReturnRestServiceResponse()
		{
			SetupMockRestService();

			IList<ArtifactIdNamePair> result = _sut.Get();

			result.Should().BeEquivalentTo(_expectedStatusesResponse);
		}

		private void SetupMockRestService()
		{
			_mockRestService
				.Setup(restService => restService.Get<IList<ArtifactIdNamePair>>(_GET_URL, null))
				.Returns(_expectedStatusesResponse);
		}
	}
}
