using System;
using System.Linq;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class EntityRequireStrategy : IRequireWorkspaceEntityStrategy<Entity>
	{
		private readonly ICreateWorkspaceEntityStrategy<Entity> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Entity> _getWorkspaceEntityByIdStrategy;
		private readonly IGetAllWorkspaceEntitiesStrategy<Entity> _getAllWorkspaceEntitiesStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Entity> _updateWorkspaceEntityStrategy;

		public EntityRequireStrategy(
			ICreateWorkspaceEntityStrategy<Entity> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<Entity> getWorkspaceEntityByIdStrategy,
			IGetAllWorkspaceEntitiesStrategy<Entity> getAllWorkspaceEntitiesStrategy,
			IUpdateWorkspaceEntityStrategy<Entity> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getAllWorkspaceEntitiesStrategy = getAllWorkspaceEntitiesStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public Entity Require(int workspaceId, Entity entity)
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

			if (entity.FullName != null)
			{
				var existedEntity = _getAllWorkspaceEntitiesStrategy.GetAll(workspaceId).FirstOrDefault(x => x.FullName == entity.FullName);
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
