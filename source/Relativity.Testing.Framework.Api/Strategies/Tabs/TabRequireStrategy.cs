using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabRequireStrategy : IRequireWorkspaceEntityStrategy<Tab>
	{
		private readonly ICreateWorkspaceEntityStrategy<Tab> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<Tab> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Tab> _updateWorkspaceEntityStrategy;

		public TabRequireStrategy(
			ICreateWorkspaceEntityStrategy<Tab> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByNameStrategy<Tab> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<Tab> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
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
