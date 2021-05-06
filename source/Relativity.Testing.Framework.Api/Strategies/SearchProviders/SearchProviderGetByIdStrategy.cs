using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class SearchProviderGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<SearchProvider>
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public SearchProviderGetByIdStrategy(IRestService restService, IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public SearchProvider Get(int workspaceId, int entityId)
		{
			bool isExist = _objectService.Query<SearchProvider>().
				For(workspaceId).
				FetchOnlyArtifactID().
				Where(x => x.ArtifactID, entityId).
				Any();

			if (!isExist)
			{
				return null;
			}

			return _restService.Get<SearchProvider>($"Relativity.SearchProviders/workspace/{workspaceId}/searchproviders/{entityId}");
		}
	}
}
