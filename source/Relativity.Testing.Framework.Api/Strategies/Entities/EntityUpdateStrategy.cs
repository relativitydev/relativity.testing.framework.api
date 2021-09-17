using System;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class EntityUpdateStrategy : IUpdateWorkspaceEntityStrategy<Entity>
	{
		private readonly IObjectService _objectService;
		private readonly IGetWorkspaceEntityByIdStrategy<Entity> _getWorkspaceEntityByIdStrategy;

		public EntityUpdateStrategy(IObjectService objectService, IGetWorkspaceEntityByIdStrategy<Entity> getWorkspaceEntityByIdStrategy)
		{
			_objectService = objectService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public Entity Update(int workspaceId, Entity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_objectService.Update(workspaceId, entity);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
		}
	}
}
