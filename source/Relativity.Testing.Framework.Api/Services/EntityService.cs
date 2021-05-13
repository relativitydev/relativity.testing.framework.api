using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class EntityService : IEntityService
	{
		private readonly ICreateWorkspaceEntityStrategy<Entity> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Entity> _getWorkspaceEntityByIdStrategy;
		private readonly IExistsWorkspaceEntityByIdStrategy<Entity> _existsWorkspaceEntityByIdStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Entity> _updateWorkspaceEntityStrategy;
		private readonly IRequireWorkspaceEntityStrategy<Entity> _requireWorkspaceEntityStrategy;
		private readonly IGetAllWorkspaceEntitiesStrategy<Entity> _getAllWorkspaceEntitiesStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<Entity> _deleteWorkspaceEntityByIdStrategy;

		public EntityService(
			ICreateWorkspaceEntityStrategy<Entity> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<Entity> getWorkspaceEntityByIdStrategy,
			IExistsWorkspaceEntityByIdStrategy<Entity> existsWorkspaceEntityByIdStrategy,
			IUpdateWorkspaceEntityStrategy<Entity> updateWorkspaceEntityStrategy,
			IRequireWorkspaceEntityStrategy<Entity> requireWorkspaceEntityStrategy,
			IGetAllWorkspaceEntitiesStrategy<Entity> getAllWorkspaceEntitiesStrategy,
			IDeleteWorkspaceEntityByIdStrategy<Entity> deleteWorkspaceEntityByIdStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
			_requireWorkspaceEntityStrategy = requireWorkspaceEntityStrategy;
			_getAllWorkspaceEntitiesStrategy = getAllWorkspaceEntitiesStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
		}

		public Entity Create(int workspaceId, Entity entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public Entity Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public bool Exists(int workspaceId, int entityId)
			=> _existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

		public void Update(int workspaceId, Entity entity)
			=> _updateWorkspaceEntityStrategy.Update(workspaceId, entity);

		public Entity Require(int workspaceId, Entity entity)
			=> _requireWorkspaceEntityStrategy.Require(workspaceId, entity);

		public Entity[] GetAll(int workspaceId)
			=> _getAllWorkspaceEntitiesStrategy.GetAll(workspaceId);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);
	}
}
