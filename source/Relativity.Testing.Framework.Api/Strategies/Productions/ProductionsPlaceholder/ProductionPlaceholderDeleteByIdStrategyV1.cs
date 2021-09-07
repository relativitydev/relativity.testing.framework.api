using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ProductionPlaceholderDeleteByIdStrategyV1 : DeleteWorkspaceEntityByIdStrategy<ProductionPlaceholder>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<ProductionPlaceholder> _existsWorkspaceEntityByIdStrategy;

		public ProductionPlaceholderDeleteByIdStrategyV1(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<ProductionPlaceholder> existsWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

			_restService.Delete($"relativity-productions/v1/workspaces/{workspaceId}/production-placeholders/{entityId}");
		}
	}
}
