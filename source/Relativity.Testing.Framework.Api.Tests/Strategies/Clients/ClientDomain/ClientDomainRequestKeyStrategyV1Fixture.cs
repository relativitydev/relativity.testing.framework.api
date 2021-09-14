using System;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;

namespace Relativity.Testing.Framework.Api.Tests.Strategies.Clients.ClientDomain
{
	[TestFixture]
	[TestOf(typeof(ClientDomainRequestKeyStrategyV1))]
	internal class ClientDomainRequestKeyStrategyV1Fixture
	{
		private const string _INVALID_CLIENT_ARTIFACT_ID_EXCEPTION_MESSAGE = "Client ID should be greater than zero.";
		private const int _VALID_CLIENT_ARTIFACT_ID = 2;

		private readonly string _request_key_url = $"relativity-identity/v1/workspaces/-1/clients/{_VALID_CLIENT_ARTIFACT_ID}/client-domain/request-key";
		private ClientDomainRequestKeyStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IArtifactIdValidator> _mockArtifactIDValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockArtifactIDValidator = new Mock<IArtifactIdValidator>();
			_mockArtifactIDValidator
				.Setup(validator => validator.Validate(It.Is<int>(id => id < 1), "Workspace"))
				.Throws(new ArgumentException(_INVALID_CLIENT_ARTIFACT_ID_EXCEPTION_MESSAGE));
			_sut = new ClientDomainRequestKeyStrategyV1(_mockRestService.Object, _mockArtifactIDValidator.Object);
		}

		[Test]
		public void Request_WithAnyClientArtifactID_CallsArtifactIdValidator()
		{
			_sut.Request(_VALID_CLIENT_ARTIFACT_ID);
			_mockArtifactIDValidator.Verify(validator => validator.Validate(_VALID_CLIENT_ARTIFACT_ID, "Client"), Times.Once);
		}

		[Test]
		public void Request_WithValidClientArtifactID_ShouldCallRestServiceWithExpectedUrl()
		{
			_sut.Request(_VALID_CLIENT_ARTIFACT_ID);
			_mockRestService.Verify(restService => restService.Post<string>(_request_key_url, null, 2, null), Times.Once);
		}
	}
}
