using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ProductionsDataSourceGetByIdStrategyV1 : IGetWorkspaceEntityByIdStrategy<ProductionDataSource>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<ProductionDataSource> _existsWorkspaceEntityByIdStrategy;

		public ProductionsDataSourceGetByIdStrategyV1(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<ProductionDataSource> existsWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		public ProductionDataSource Get(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
			{
				return null;
			}

			return _restService.Get<ProductionDataSource>($"relativity-productions/V1/workspaces/{workspaceId}/production-data-sources/{entityId}?withPlaceholderImage=true");
		}
	}
}
