using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabDeleteByIdStrategy : DeleteWorkspaceEntityByIdStrategy<Tab>
	{
		private readonly IRestService _restService;

		public TabDeleteByIdStrategy(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_restService.Delete($"Relativity.Tabs/workspace/{workspaceId}/tabs/{entityId}");
		}
	}
}
