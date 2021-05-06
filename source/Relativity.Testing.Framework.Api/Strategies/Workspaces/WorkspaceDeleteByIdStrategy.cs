using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceDeleteByIdStrategy : DeleteByIdStrategy<Workspace>
	{
		private readonly IRestService _restService;
		private readonly IEnsureExistsByIdStrategy<Workspace> _ensureExistsByIdStrategy;
		private readonly IWaitDeleteWorkspaceStrategy _waitDeleteWorkspaceStrategy;

		public WorkspaceDeleteByIdStrategy(
			IRestService restService,
			IEnsureExistsByIdStrategy<Workspace> ensureExistsByIdStrategy,
			IWaitDeleteWorkspaceStrategy waitDeleteWorkspaceStrategy)
		{
			_restService = restService;
			_ensureExistsByIdStrategy = ensureExistsByIdStrategy;
			_waitDeleteWorkspaceStrategy = waitDeleteWorkspaceStrategy;
		}

		protected override void DoDelete(int id)
		{
			_ensureExistsByIdStrategy.EnsureExists(id);

			var dto = new
			{
				workspace = new
				{
					ArtifactID = id
				}
			};

			_restService.Post(
				"Relativity.Services.Workspace.IWorkspaceModule/Workspace Manager Service/DeleteAsync",
				dto);

			_waitDeleteWorkspaceStrategy.Wait(id);
		}
	}
}
