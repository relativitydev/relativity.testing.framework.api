using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class EntityGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<Entity>
	{
		private readonly IObjectService _objectService;
		private readonly IExistsWorkspaceEntityByIdStrategy<Entity> _existsWorkspaceEntityByIdStrategy;

		public EntityGetByIdStrategy(
			IObjectService objectService,
			IExistsWorkspaceEntityByIdStrategy<Entity> existsWorkspaceEntityByIdStrategy)
		{
			_objectService = objectService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		public Entity Get(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
			{
				return null;
			}

			return _objectService.Query<Entity>().
				For(workspaceId).
				Where(x => x.ArtifactID, entityId).
				FirstOrDefault();
		}
	}
}
