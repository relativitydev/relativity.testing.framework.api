using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class KeywordSearchRequireStrategy : IRequireWorkspaceEntityStrategy<KeywordSearch>
	{
		private readonly ICreateWorkspaceEntityStrategy<KeywordSearch> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<KeywordSearch> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<KeywordSearch> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<KeywordSearch> _updateWorkspaceEntityStrategy;

		public KeywordSearchRequireStrategy(
			ICreateWorkspaceEntityStrategy<KeywordSearch> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<KeywordSearch> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<KeywordSearch> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<KeywordSearch> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public KeywordSearch Require(int workspaceId, KeywordSearch entity)
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
