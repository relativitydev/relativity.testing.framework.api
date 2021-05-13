using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class SearchProviderDeleteStrategy : DeleteWorkspaceEntityByIdStrategy<SearchProvider>
	{
		private readonly IRestService _restService;

		public SearchProviderDeleteStrategy(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_restService.Delete($"Relativity.SearchProviders/workspace/{workspaceId}/searchproviders/{entityId}");
		}
	}
}
