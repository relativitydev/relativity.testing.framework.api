using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ProductionsDataSourceCreateStrategyPreOsier : ICreateWorkspaceEntityStrategy<ProductionDataSource>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<ProductionDataSource> _getWorkspaceEntityByIdStrategy;
		private readonly IWaitCreateWorkspaceEntityStrategy _waitCreateWorkspaceEntityStrategy;

		public ProductionsDataSourceCreateStrategyPreOsier(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<ProductionDataSource> getWorkspaceEntityByIdStrategy,
			IWaitCreateWorkspaceEntityStrategy waitCreateWorkspaceEntityStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_waitCreateWorkspaceEntityStrategy = waitCreateWorkspaceEntityStrategy;
		}

		public ProductionDataSource Create(int workspaceId, ProductionDataSource entity)
		{
			if (entity == null || entity.ProductionId == 0)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				productionID = entity.ProductionId,
				dataSource = entity
			};

			var artifactId = _restService.Post<int>("Relativity.Productions.Services.IProductionModule/Production%20Data%20Source%20Manager/CreateSingleAsync", dto);

			_waitCreateWorkspaceEntityStrategy.Wait<ProductionDataSource>(workspaceId, artifactId);

			var createdEntity = _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifactId);
			createdEntity.ProductionId = entity.ProductionId;

			return createdEntity;
		}
	}
}
