using System;
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

		public Client Update(Client entity)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_clientStatusEnsureArtifactIdIsFilledStrategy.Ensure(entity);
			var dto = new ClientDTOV1(entity);

			return _restService.Put<Client>(
				$"Relativity.rest/api/relativity-identity/v1/workspaces/-1/clients/{entity.ArtifactID}",
				dto);
		}
	}
}
