using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ViewService : IViewService
	{
		private readonly ICreateWorkspaceEntityStrategy<View> _createWorkspaceEntityStrategy;
		private readonly IGetAllWorkspaceEntitiesStrategy<View> _getAllWorkspaceEntitiesStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<View> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<View> _getWorkspaceEntityByNameStrategy;
		private readonly IRequireWorkspaceEntityStrategy<View> _requireWorkspaceEntityStrategy;
		private readonly IExistsWorkspaceEntityByIdStrategy<View> _existsWorkspaceEntityByIdStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<View> _updateWorkspaceEntityStrategy;

		public ViewService(
			ICreateWorkspaceEntityStrategy<View> cretCreateWorkspaceEntityStrategy,
			IGetAllWorkspaceEntitiesStrategy<View> getAllWorkspaceEntitiesStrategy,
			IGetWorkspaceEntityByIdStrategy<View> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<View> getWorkspaceEntityByNameStrategy,
			IRequireWorkspaceEntityStrategy<View> requireWorkspaceEntityStrategy,
			IExistsWorkspaceEntityByIdStrategy<View> existsWorkspaceEntityByIdStrategy,
			IUpdateWorkspaceEntityStrategy<View> updateWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = cretCreateWorkspaceEntityStrategy;
			_getAllWorkspaceEntitiesStrategy = getAllWorkspaceEntitiesStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_requireWorkspaceEntityStrategy = requireWorkspaceEntityStrategy;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
		}

		public View Create(int workspaceId, View entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public View[] GetAll(int workspaceId)
			=> _getAllWorkspaceEntitiesStrategy.GetAll(workspaceId);

		public View Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public View Get(int workspaceId, string entityName)
			=> _getWorkspaceEntityByNameStrategy.Get(workspaceId, entityName);

		public View Require(int workspaceId, View entity)
			=> _requireWorkspaceEntityStrategy.Require(workspaceId, entity);

		public bool Exists(int workspaceId, int entityId)
			=> _existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

		public void Update(int workspaceId, View entity)
			=> _updateWorkspaceEntityStrategy.Update(workspaceId, entity);
	}
}
