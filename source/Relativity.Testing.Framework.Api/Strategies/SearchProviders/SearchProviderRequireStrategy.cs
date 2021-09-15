using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class SearchProviderRequireStrategy : IRequireWorkspaceEntityStrategy<SearchProvider>
	{
		private readonly ICreateWorkspaceEntityStrategy<SearchProvider> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<SearchProvider> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<SearchProvider> _updateWorkspaceEntityStrategy;

		public SearchProviderRequireStrategy(
			ICreateWorkspaceEntityStrategy<SearchProvider> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByNameStrategy<SearchProvider> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<SearchProvider> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
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
				return _updateWorkspaceEntityStrategy.Update(workspaceId, entity);
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
					return _updateWorkspaceEntityStrategy.Update(workspaceId, entity);
				}
			}

			return _createWorkspaceEntityStrategy.Create(workspaceId, entity);
		}
	}
}
