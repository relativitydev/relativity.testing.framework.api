using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ObjectTypeRequireStrategy : IRequireWorkspaceEntityStrategy<ObjectType>
	{
		private readonly ICreateWorkspaceEntityStrategy<ObjectType> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<ObjectType> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<ObjectType> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<ObjectType> _updateWorkspaceEntityStrategy;

		public ObjectTypeRequireStrategy(
			ICreateWorkspaceEntityStrategy<ObjectType> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<ObjectType> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<ObjectType> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<ObjectType> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
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
