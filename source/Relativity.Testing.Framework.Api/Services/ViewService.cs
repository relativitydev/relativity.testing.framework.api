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

		public View Create(int workspaceArtifactID, View view)
			=> _createWorkspaceEntityStrategy.Create(workspaceArtifactID, view);

		public View[] GetAll(int workspaceArtifactID)
			=> _getAllWorkspaceEntitiesStrategy.GetAll(workspaceArtifactID);

		public View Get(int workspaceArtifactID, int viewArtifactID)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceArtifactID, viewArtifactID);

		public View Get(int workspaceArtifactID, string viewName)
			=> _getWorkspaceEntityByNameStrategy.Get(workspaceArtifactID, viewName);

		public View Require(int workspaceArtifactID, View view)
			=> _requireWorkspaceEntityStrategy.Require(workspaceArtifactID, view);

		public bool Exists(int workspaceArtifactID, int viewArtifactID)
			=> _existsWorkspaceEntityByIdStrategy.Exists(workspaceArtifactID, viewArtifactID);

		public void Update(int workspaceArtifactID, View view)
			=> _updateWorkspaceEntityStrategy.Update(workspaceArtifactID, view);
	}
}
