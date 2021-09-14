using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ClientDomainRequestKeyStrategyV1 : IClientDomainRequestKeyStrategy
	{
		private readonly IRestService _restService;

		private readonly IArtifactIdValidator _artifactIDValidator;

		public ClientDomainRequestKeyStrategyV1(
			IRestService restService,
			IArtifactIdValidator artifactIDValidator)
		{
			_restService = restService;
			_artifactIDValidator = artifactIDValidator;
		}

		public string Request(int clientArtifactID)
		{
			_artifactIDValidator.Validate(clientArtifactID, "Client");

			return _restService.Post<string>($"relativity-identity/v1/workspaces/-1/clients/{clientArtifactID}/client-domain/request-key");
		}
	}
}
