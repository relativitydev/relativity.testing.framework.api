using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsDataSourceGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<ProductionDataSource>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<ProductionDataSource> _existsWorkspaceEntityByIdStrategy;

		public ProductionsDataSourceGetByIdStrategy(
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
