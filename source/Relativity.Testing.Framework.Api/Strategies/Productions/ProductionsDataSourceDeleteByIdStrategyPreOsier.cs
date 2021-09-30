using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ProductionsDataSourceDeleteByIdStrategyPreOsier : DeleteWorkspaceEntityByIdStrategy<ProductionDataSource>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<ProductionDataSource> _existsWorkspaceEntityByIdStrategy;
		private readonly IWaitDeleteWorkspaceEntityStrategy _waitDeleteWorkspaceEntityStrategy;

		public ProductionsDataSourceDeleteByIdStrategyPreOsier(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<ProductionDataSource> existsWorkspaceEntityByIdStrategy,
			IWaitDeleteWorkspaceEntityStrategy waitDeleteWorkspaceEntityStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
			_waitDeleteWorkspaceEntityStrategy = waitDeleteWorkspaceEntityStrategy;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				dataSourceArtifactID = entityId
			};

			_restService.Post("Relativity.Productions.Services.IProductionModule/Production%20Data%20Source%20Manager/DeleteSingleAsync", dto);

			_waitDeleteWorkspaceEntityStrategy.Wait<ProductionDataSource>(workspaceId, entityId);
		}
	}
}
