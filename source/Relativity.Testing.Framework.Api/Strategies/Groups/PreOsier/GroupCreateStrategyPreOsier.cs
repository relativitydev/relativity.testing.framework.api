using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class GroupCreateStrategyPreOsier : CreateStrategy<Group>
	{
		private readonly IRestService _restService;

		private readonly IRequireStrategy<Client> _clientRequireStrategy;

		public GroupCreateStrategyPreOsier(
			IRestService restService,
			IRequireStrategy<Client> clientRequireStrategy)
		{
			_restService = restService;
			_clientRequireStrategy = clientRequireStrategy;
		}

		protected override Group DoCreate(Group entity)
		{
			entity.Client = _clientRequireStrategy.Require(entity.Client);

			var dto = new GroupDTO(entity);

			var response = _restService.Post<GroupResponse>("relativity.groups/workspace/-1/groups", dto);
			Group mappedGroup = response.MapToGroup();

			return mappedGroup;
		}
	}
}
