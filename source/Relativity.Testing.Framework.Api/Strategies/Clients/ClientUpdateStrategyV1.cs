using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies.Clients
{
	[VersionRange(">=12.1")]
	internal class ClientUpdateStrategyV1 : IUpdateStrategy<Client>
	{
		private readonly IRestService _restService;

		private readonly IClientStatusEnsureArtifactIdIsFilledStrategy _clientStatusEnsureArtifactIdIsFilledStrategy;

		public ClientUpdateStrategyV1(
			IRestService restService,
			IClientStatusEnsureArtifactIdIsFilledStrategy clientStatusEnsureArtifactIdIsFilledStrategy)
		{
			_restService = restService;
			_clientStatusEnsureArtifactIdIsFilledStrategy = clientStatusEnsureArtifactIdIsFilledStrategy;
		}

		public void Update(Client entity)
		{
			_clientStatusEnsureArtifactIdIsFilledStrategy.Ensure(entity);
			var dto = new ClientDTOV2(entity);

			_restService.Put<Client>(
				$"Relativity.rest/api/relativity-identity/v1/workspaces/-1/clients/{entity.ArtifactID}",
				dto);
		}
	}
}
