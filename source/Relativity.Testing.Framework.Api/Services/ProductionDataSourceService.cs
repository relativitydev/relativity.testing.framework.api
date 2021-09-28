using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ProductionDataSourceService : IProductionDataSourceService
	{
		private readonly ICreateWorkspaceEntityStrategy<ProductionDataSource> _createWorkspaceEntityStrategy;
		private readonly IExistsWorkspaceEntityByIdStrategy<ProductionDataSource> _existsWorkspaceEntityByIdStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<ProductionDataSource> _deleteWorkspaceEntityByIdStrategy;
		private readonly IUpdateProductionsDataSourceStrategy _updateProductionsDataSourceStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<ProductionDataSource> _getWorkspaceEntityByIdStrategy;
		private readonly IProductionsDataSourceGetDefaultFieldValuesStrategy _getDefaultFieldValuesStrategy;

		public ProductionDataSourceService(
			ICreateWorkspaceEntityStrategy<ProductionDataSource> createWorkspaceEntityStrategy,
			IExistsWorkspaceEntityByIdStrategy<ProductionDataSource> existsWorkspaceEntityByIdStrategy,
			IDeleteWorkspaceEntityByIdStrategy<ProductionDataSource> deleteWorkspaceEntityByIdStrategy,
			IUpdateProductionsDataSourceStrategy updateProductionsDataSourceStrategy,
			IGetWorkspaceEntityByIdStrategy<ProductionDataSource> getWorkspaceEntityByIdStrategy,
			IProductionsDataSourceGetDefaultFieldValuesStrategy getDefaultFieldValuesStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_updateProductionsDataSourceStrategy = updateProductionsDataSourceStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getDefaultFieldValuesStrategy = getDefaultFieldValuesStrategy;
		}

		public ProductionDataSource Create(int workspaceId, int productionId, ProductionDataSource entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public bool Exists(int workspaceId, int entityId)
			=> _existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

		public void Update(int workspaceId, int productionId, ProductionDataSource entity)
			=> _updateProductionsDataSourceStrategy.Update(workspaceId, productionId, entity);

		public ProductionDataSource Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public ProductionDataSourceDefaultValues GetDefaultFieldValues(int workspaceId)
		=> _getDefaultFieldValuesStrategy.Get(workspaceId);
	}
}
