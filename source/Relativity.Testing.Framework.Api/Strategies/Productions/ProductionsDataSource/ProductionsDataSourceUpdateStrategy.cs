using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsDataSourceUpdateStrategy : IUpdateProductionsDataSourceStrategy
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<ProductionDataSource> _getWorkspaceEntityByIdStrategy;

		public ProductionsDataSourceUpdateStrategy(IRestService restService, IGetWorkspaceEntityByIdStrategy<ProductionDataSource> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public ProductionDataSource Update(int workspaceId, int productionId, ProductionDataSource entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				productionID = productionId,
				dataSource = entity
			};

			_restService.Post("Relativity.Productions.Services.IProductionModule/Production%20Data%20Source%20Manager/UpdateSingleAsync", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
		}
	}
}
