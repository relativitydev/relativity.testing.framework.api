using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class LayoutDeleteByIdStrategyV1 : DeleteWorkspaceEntityByIdStrategy<Layout>
	{
		private readonly IRestService _restService;

		public LayoutDeleteByIdStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_restService.Delete($"relativity-data-visualization/v1/workspaces/{workspaceId}/layouts/{entityId}");
		}
	}
}
