using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ClientCreateStrategyV1 : CreateStrategy<Client>
	{
		private readonly IRestService _restService;

		private readonly IClientStatusEnsureArtifactIdIsFilledStrategy _clientStatusEnsureArtifactIdIsFilledStrategy;

		public ClientCreateStrategyV1(IRestService restService, IClientStatusEnsureArtifactIdIsFilledStrategy clientStatusEnsureArtifactIdIsFilledStrategy)
		{
			_restService = restService;
			_clientStatusEnsureArtifactIdIsFilledStrategy = clientStatusEnsureArtifactIdIsFilledStrategy;
		}

		protected override Client DoCreate(Client entity)
		{
			_clientStatusEnsureArtifactIdIsFilledStrategy.Ensure(entity);
			var dto = new ClientDTOV2(entity);

			var createdClient = _restService.Post<Client>(
				"/Relativity.rest/api/relativity-identity/v1/workspaces/-1/clients/",
				dto);

			return createdClient;
		}
	}
}
