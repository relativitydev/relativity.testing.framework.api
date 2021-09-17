using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class SearchProviderService : ISearchProviderService
	{
		private readonly ICreateWorkspaceEntityStrategy<SearchProvider> _createWorkspaceEntityStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<SearchProvider> _deleteWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<SearchProvider> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<SearchProvider> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<SearchProvider> _updateWorkspaceEntityStrategy;
		private readonly IRequireWorkspaceEntityStrategy<SearchProvider> _requireWorkspaceEntityStrategy;
		private readonly IGetDependencyListForWorkspaceEntityStrategy<SearchProvider> _getDependencyListForWorkspaceEntityStrategy;

		public SearchProviderService(
			ICreateWorkspaceEntityStrategy<SearchProvider> createWorkspaceEntityStrategy,
			IDeleteWorkspaceEntityByIdStrategy<SearchProvider> deleteWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByIdStrategy<SearchProvider> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<SearchProvider> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<SearchProvider> updateWorkspaceEntityStrategy,
			IRequireWorkspaceEntityStrategy<SearchProvider> requireWorkspaceEntityStrategy,
			IGetDependencyListForWorkspaceEntityStrategy<SearchProvider> getDependencyListForWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
			_requireWorkspaceEntityStrategy = requireWorkspaceEntityStrategy;
			_getDependencyListForWorkspaceEntityStrategy = getDependencyListForWorkspaceEntityStrategy;
		}

		public SearchProvider Create(int workspaceId, SearchProvider entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public SearchProvider Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public SearchProvider Get(int workspaceId, string entityName)
			=> _getWorkspaceEntityByNameStrategy.Get(workspaceId, entityName);

		public SearchProvider Update(int workspaceId, SearchProvider entity)
			=> _updateWorkspaceEntityStrategy.Update(workspaceId, entity);

		public SearchProvider Require(int workspaceId, SearchProvider entity)
			=> _requireWorkspaceEntityStrategy.Require(workspaceId, entity);

		public List<Dependency> GetDependencies(int workspaceId, int entityId)
			=> _getDependencyListForWorkspaceEntityStrategy.GetDependencies(workspaceId, entityId);
	}
}
