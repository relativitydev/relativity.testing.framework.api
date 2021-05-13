using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupCreateStrategy : CreateStrategy<Group>
	{
		private readonly IRestService _restService;

		private readonly IRequireStrategy<Client> _clientRequireStrategy;

		private readonly IGetByIdStrategy<Group> _getByIdStrategy;

		public GroupCreateStrategy(
			IRestService restService,
			IRequireStrategy<Client> clientRequireStrategy,
			IGetByIdStrategy<Group> getByIdStrategy)
		{
			_restService = restService;
			_clientRequireStrategy = clientRequireStrategy;
			_getByIdStrategy = getByIdStrategy;
		}

		protected override Group DoCreate(Group entity)
		{
			entity.Client = _clientRequireStrategy.Require(entity.Client);

			var dto = new
			{
				groupRequest = new
				{
					Client = new
					{
						Secured = false,
						Value = new
						{
							entity.Client.ArtifactID
						}
					},
					entity.Name,
					entity.Keywords,
					entity.Notes
				}
			};

			var response = _restService.Post<JObject>("relativity.groups/workspace/-1/groups", dto);

			var artifactId = (int)response["ArtifactID"];

			return _getByIdStrategy.Get(artifactId);
		}
	}
}
