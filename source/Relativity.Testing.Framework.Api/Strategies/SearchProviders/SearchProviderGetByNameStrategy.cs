using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class SearchProviderGetByNameStrategy : IGetWorkspaceEntityByNameStrategy<SearchProvider>
	{
		private readonly IObjectService _objectService;
		private readonly IGetWorkspaceEntityByIdStrategy<SearchProvider> _getWorkspaceEntityByIdStrategy;

		public SearchProviderGetByNameStrategy(
			IObjectService objectService,
			IGetWorkspaceEntityByIdStrategy<SearchProvider> getWorkspaceEntityByIdStrategy)
		{
			_objectService = objectService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public SearchProvider Get(int workspaceId, string entityName)
		{
			var searchProvider = _objectService.Query<SearchProvider>().
				For(workspaceId).
				FetchOnlyArtifactID().
				Where(x => x.Name, entityName).
				FirstOrDefault();

			if (searchProvider == null)
			{
				return null;
			}

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, searchProvider.ArtifactID);
		}
	}
}
