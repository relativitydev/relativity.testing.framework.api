using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class SearchProviderCreateStrategy : CreateWorkspaceEntityStrategy<SearchProvider>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<SearchProvider> _getWorkspaceEntityByIdStrategy;

		public SearchProviderCreateStrategy(IRestService restService, IGetWorkspaceEntityByIdStrategy<SearchProvider> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		protected override SearchProvider DoCreate(int workspaceId, SearchProvider entity)
		{
			entity.FillRequiredProperties();

			var searchProvider = JObject.FromObject(entity);

			searchProvider["ArtifactID"].Parent.Remove();

			var dto = new
			{
				searchProvider
			};

			var artifactID = _restService.Post<int>($"Relativity.SearchProviders/workspace/{workspaceId}/searchproviders", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifactID);
		}
	}
}
