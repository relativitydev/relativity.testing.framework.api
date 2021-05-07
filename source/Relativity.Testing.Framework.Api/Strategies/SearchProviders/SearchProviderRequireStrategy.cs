using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class SearchProviderRequireStrategy : IRequireWorkspaceEntityStrategy<SearchProvider>
	{
		private readonly ICreateWorkspaceEntityStrategy<SearchProvider> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<SearchProvider> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<SearchProvider> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<SearchProvider> _updateWorkspaceEntityStrategy;

		public SearchProviderRequireStrategy(
			ICreateWorkspaceEntityStrategy<SearchProvider> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<SearchProvider> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<SearchProvider> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<SearchProvider> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public SearchProvider Require(int workspaceId, SearchProvider entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID != 0)
			{
				_updateWorkspaceEntityStrategy.Update(workspaceId, entity);
				return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
			}

			if (entity.Name != null)
			{
				var existedEntity = _getWorkspaceEntityByNameStrategy.Get(workspaceId, entity.Name);
				if (existedEntity == null)
				{
					return _createWorkspaceEntityStrategy.Create(workspaceId, entity);
				}
				else
				{
					entity.ArtifactID = existedEntity.ArtifactID;
					_updateWorkspaceEntityStrategy.Update(workspaceId, entity);
					return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
				}
			}

			return _createWorkspaceEntityStrategy.Create(workspaceId, entity);
		}
	}
}
