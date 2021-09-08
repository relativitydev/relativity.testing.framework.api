using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ProductionPlaceholderService : IProductionPlaceholderService
	{
		private readonly ICreateWorkspaceEntityStrategy<ProductionPlaceholder> _createWorkspaceEntityStrategy;

		private readonly IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder> _getWorkspaceEntityByIdStrategy;

		private readonly IExistsWorkspaceEntityByIdStrategy<ProductionPlaceholder> _existsWorkspaceEntityByIdStrategy;

		private readonly IUpdateWorkspaceEntityStrategy<ProductionPlaceholder> _updateWorkspaceEntityStrategy;

		private readonly IDeleteWorkspaceEntityByIdStrategy<ProductionDataSource> _deleteWorkspaceEntityByIdStrategy;

		private readonly IProductionPlaceholderGetDefaultFieldValuesStrategy _getDefaultFieldValuesStrategy;

		public ProductionPlaceholderService(
			ICreateWorkspaceEntityStrategy<ProductionPlaceholder> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder> getWorkspaceEntityByIdStrategy,
			IExistsWorkspaceEntityByIdStrategy<ProductionPlaceholder> existsWorkspaceEntityByIdStrategy,
			IUpdateWorkspaceEntityStrategy<ProductionPlaceholder> updateWorkspaceEntityStrategy,
			IDeleteWorkspaceEntityByIdStrategy<ProductionDataSource> deleteWorkspaceEntityByIdStrategy,
			IProductionPlaceholderGetDefaultFieldValuesStrategy getDefaultFieldValuesStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_getDefaultFieldValuesStrategy = getDefaultFieldValuesStrategy;
		}

		public ProductionPlaceholder Create(int workspaceId, ProductionPlaceholder entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public ProductionPlaceholder Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public bool Exists(int workspaceId, int entityId)
			=> _existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

		public void Update(int workspaceId, ProductionPlaceholder entity)
			=> _updateWorkspaceEntityStrategy.Update(workspaceId, entity);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public DefaultFieldValue<NamedArtifact> GetDefaultFieldValues(int workspaceId)
			=> _getDefaultFieldValuesStrategy.Get(workspaceId);
	}
}
