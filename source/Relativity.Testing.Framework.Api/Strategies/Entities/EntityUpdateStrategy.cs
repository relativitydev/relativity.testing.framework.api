using System;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class EntityUpdateStrategy : IUpdateWorkspaceEntityStrategy<Entity>
	{
		private readonly IObjectService _objectService;

		public EntityUpdateStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		void IUpdateWorkspaceEntityStrategy<Entity>.Update(int workspaceId, Entity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_objectService.Update(workspaceId, entity);
		}
	}
}
