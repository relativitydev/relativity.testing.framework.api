using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class WorkspaceDeleteByIdStrategyV1 : WorkspaceDeleteByIdAbstractStrategy
	{
		private readonly IRestService _restService;

		public WorkspaceDeleteByIdStrategyV1(
				IRestService restService,
				IEnsureExistsByIdStrategy<Workspace> ensureExistsByIdStrategy,
				IWaitDeleteWorkspaceStrategy waitDeleteWorkspaceStrategy)
			: base(ensureExistsByIdStrategy, waitDeleteWorkspaceStrategy)
		{
			_restService = restService;
		}

		protected override void DeleteWorkspace(int id)
		{
			_restService.Delete($"relativity-environment/V1/workspace/{id}");
		}
	}
}
