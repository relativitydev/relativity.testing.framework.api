using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class EntityDeleteByIdStrategy : DeleteWorkspaceEntityByIdStrategy<Entity>
	{
		private readonly IObjectService _objectService;
		private readonly IExistsWorkspaceEntityByIdStrategy<Entity> _existsWorkspaceEntityByIdStrategy;

		public EntityDeleteByIdStrategy(
			IObjectService objectService,
			IExistsWorkspaceEntityByIdStrategy<Entity> existsWorkspaceEntityByIdStrategy)
		{
			_objectService = objectService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
				throw new ObjectNotFoundException();

			_objectService.Delete(workspaceId, entityId);
		}
	}
}
