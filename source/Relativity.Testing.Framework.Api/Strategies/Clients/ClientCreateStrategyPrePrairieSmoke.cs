using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ClientCreateStrategyPrePrairieSmoke : CreateStrategy<Client>
	{
		private readonly IRestService _restService;
		private readonly IClientStatusEnsureArtifactIdIsFilledStrategy _clientStatusEnsureArtifactIdIsFilledStrategy;
		private readonly IGetByIdStrategy<Client> _getByIdStrategy;

		public ClientCreateStrategyPrePrairieSmoke(
			IRestService restService,
			IClientStatusEnsureArtifactIdIsFilledStrategy clientStatusEnsureArtifactIdIsFilledStrategy,
			IGetByIdStrategy<Client> getByIdStrategy)
		{
			_restService = restService;
			_clientStatusEnsureArtifactIdIsFilledStrategy = clientStatusEnsureArtifactIdIsFilledStrategy;
			_getByIdStrategy = getByIdStrategy;
		}

		protected override Client DoCreate(Client entity)
		{
			_clientStatusEnsureArtifactIdIsFilledStrategy.Ensure(entity);
			var dto = new ClientDTOPrePrairieSmoke(entity);

			var artifactID = _restService.Post<int>(
				"Relativity.Services.Client.IClientModule/Client%20Manager/CreateSingleAsync",
				dto);

			return _getByIdStrategy.Get(artifactID);
		}
	}
}
