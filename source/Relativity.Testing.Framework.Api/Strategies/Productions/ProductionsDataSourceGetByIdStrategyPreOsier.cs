using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ProductionsDataSourceGetByIdStrategyPreOsier : IGetWorkspaceEntityByIdStrategy<ProductionDataSource>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<ProductionDataSource> _existsWorkspaceEntityByIdStrategy;

		public ProductionsDataSourceGetByIdStrategyPreOsier(
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

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				dataSourceArtifactID = entityId,
				withPlaceholderImage = true
			};

			return _restService.Post<ProductionDataSource>("Relativity.Productions.Services.IProductionModule/Production%20Data%20Source%20Manager/ReadSingleAsync", dto);
		}
	}
}
