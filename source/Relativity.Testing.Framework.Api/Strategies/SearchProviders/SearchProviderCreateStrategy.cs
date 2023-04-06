using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class SearchProviderCreateStrategy : CreateWorkspaceEntityStrategy<SearchProvider>
	{
		private readonly IRestService _restService;

		public SearchProviderCreateStrategy(IRestService restService)
		{
			_restService = restService;
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

			entity.ArtifactID = _restService.Post<int>($"Relativity.SearchProviders/workspace/{workspaceId}/searchproviders", dto);

			return entity;
		}
	}
}
