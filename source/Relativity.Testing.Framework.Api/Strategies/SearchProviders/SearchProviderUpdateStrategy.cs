using System;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class SearchProviderUpdateStrategy : IUpdateWorkspaceEntityStrategy<SearchProvider>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<SearchProvider> _getWorkspaceEntityByIdStrategy;

		public SearchProviderUpdateStrategy(IRestService restService, IGetWorkspaceEntityByIdStrategy<SearchProvider> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public SearchProvider Update(int workspaceId, SearchProvider entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var searchProvider = JObject.FromObject(entity);

			searchProvider["ArtifactID"].Parent.Remove();

			var dto = new
			{
				searchProvider
			};

			_restService.Put($"Relativity.SearchProviders/workspace/{workspaceId}/searchproviders/{entity.ArtifactID}", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
		}
	}
}
