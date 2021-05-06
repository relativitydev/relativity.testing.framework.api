using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class TabRequireStrategy : IRequireWorkspaceEntityStrategy<Tab>
	{
		private readonly ICreateWorkspaceEntityStrategy<Tab> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Tab> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<Tab> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Tab> _updateWorkspaceEntityStrategy;

		public TabRequireStrategy(
			ICreateWorkspaceEntityStrategy<Tab> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<Tab> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<Tab> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<Tab> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public Tab Require(int workspaceId, Tab entity)
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
