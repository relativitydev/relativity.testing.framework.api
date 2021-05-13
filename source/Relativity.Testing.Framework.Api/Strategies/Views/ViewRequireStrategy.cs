using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ViewRequireStrategy : IRequireWorkspaceEntityStrategy<View>
	{
		private readonly ICreateWorkspaceEntityStrategy<View> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<View> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<View> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<View> _updateWorkspaceEntityStrategy;

		public ViewRequireStrategy(
			ICreateWorkspaceEntityStrategy<View> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<View> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<View> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<View> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
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
