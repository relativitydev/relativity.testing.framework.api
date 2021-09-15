using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeRequireStrategy : IRequireWorkspaceEntityStrategy<ObjectType>
	{
		private readonly ICreateWorkspaceEntityStrategy<ObjectType> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<ObjectType> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<ObjectType> _updateWorkspaceEntityStrategy;

		public ObjectTypeRequireStrategy(
			ICreateWorkspaceEntityStrategy<ObjectType> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByNameStrategy<ObjectType> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<ObjectType> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public ObjectType Require(int workspaceId, ObjectType entity)
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
