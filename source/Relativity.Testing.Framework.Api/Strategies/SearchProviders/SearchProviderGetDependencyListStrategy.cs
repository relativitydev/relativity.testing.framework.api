using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class SearchProviderGetDependencyListStrategy : IGetDependencyListForWorkspaceEntityStrategy<SearchProvider>
	{
		private readonly IRestService _restService;

		public SearchProviderGetDependencyListStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public List<Dependency> GetDependencies(int workspaceId, int entityId)
		{
			return _restService.Get<List<Dependency>>($"Relativity.SearchProviders/workspace/{workspaceId}/searchproviders/{entityId}/dependencyList");
		}
	}
}
