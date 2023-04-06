using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal abstract class WorkspaceDeleteByIdAbstractStrategy : DeleteByIdStrategy<Workspace>
	{
		private readonly IEnsureExistsByIdStrategy<Workspace> _ensureExistsByIdStrategy;
		private readonly IWaitDeleteWorkspaceStrategy _waitDeleteWorkspaceStrategy;

		protected WorkspaceDeleteByIdAbstractStrategy(
			IEnsureExistsByIdStrategy<Workspace> ensureExistsByIdStrategy,
			IWaitDeleteWorkspaceStrategy waitDeleteWorkspaceStrategy)
		{
			_ensureExistsByIdStrategy = ensureExistsByIdStrategy;
			_waitDeleteWorkspaceStrategy = waitDeleteWorkspaceStrategy;
		}

		protected override void DoDelete(int id)
		{
			_ensureExistsByIdStrategy.EnsureExists(id);

			DeleteWorkspace(id);

			_waitDeleteWorkspaceStrategy.Wait(id);
		}

		protected abstract void DeleteWorkspace(int id);
	}
}
