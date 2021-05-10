using System;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class SearchProviderUpdateStrategy : IUpdateWorkspaceEntityStrategy<SearchProvider>
	{
		private readonly IRestService _restService;

		public SearchProviderUpdateStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, SearchProvider entity)
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
		}
	}
}
