using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class WorkspaceDeleteByIdStrategyPreOsier : WorkspaceDeleteByIdAbstractStrategy
	{
		private readonly IRestService _restService;

		public WorkspaceDeleteByIdStrategyPreOsier(
			IRestService restService,
			IEnsureExistsByIdStrategy<Workspace> ensureExistsByIdStrategy,
			IWaitDeleteWorkspaceStrategy waitDeleteWorkspaceStrategy)
			: base(ensureExistsByIdStrategy, waitDeleteWorkspaceStrategy)
		{
			_restService = restService;
		}

		protected override void DeleteWorkspace(int id)
		{
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
		}
	}
}
