using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ProductionsDataSourceCreateStrategyV1 : ICreateWorkspaceEntityStrategy<ProductionDataSource>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<ProductionDataSource> _getWorkspaceEntityByIdStrategy;
		private readonly IWaitCreateWorkspaceEntityStrategy _waitCreateWorkspaceEntityStrategy;

		public ProductionsDataSourceCreateStrategyV1(
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
			ValidateEntityNotEmpty(entity);

			var dto = CreateDTO(workspaceId, entity);

			var artifactId = _restService.Post<int>($"relativity-productions/V1/workspaces/{workspaceId}/productions/{entity.ProductionId}/production-data-sources", dto);

			_waitCreateWorkspaceEntityStrategy.Wait<ProductionDataSource>(workspaceId, artifactId);

			return GetCreatedEntity(workspaceId, artifactId, entity.ProductionId);
		}

		private ProductionDataSource GetCreatedEntity(int workspaceId, int artifactId, int productionId)
		{
			var createdEntity = _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifactId);
			createdEntity.ProductionId = productionId;
			return createdEntity;
		}

		private void ValidateEntityNotEmpty(ProductionDataSource entity)
		{
			if (entity == null || entity.ProductionId == 0)
			{
				throw new ArgumentNullException(nameof(entity));
			}
		}

		private object CreateDTO(int workspaceId, ProductionDataSource entity)
		{
			return new
			{
				workspaceArtifactID = workspaceId,
				productionID = entity.ProductionId,
				dataSource = entity
			};
		}
	}
}
