using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class TabDeleteByIdStrategyPrePrairieSmoke : DeleteWorkspaceEntityByIdStrategy<Tab>
	{
		private readonly IRestService _restService;

		public TabDeleteByIdStrategyPrePrairieSmoke(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_restService.Delete($"Relativity.Tabs/workspace/{workspaceId}/tabs/{entityId}");
		}
	}
}
