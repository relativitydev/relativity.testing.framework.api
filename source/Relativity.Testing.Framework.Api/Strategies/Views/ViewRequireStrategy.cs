using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ViewRequireStrategy : IRequireWorkspaceEntityStrategy<View>
	{
		private readonly ICreateWorkspaceEntityStrategy<View> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<View> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<View> _updateWorkspaceEntityStrategy;

		public ViewRequireStrategy(
			ICreateWorkspaceEntityStrategy<View> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByNameStrategy<View> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<View> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public View Require(int workspaceId, View entity)
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
