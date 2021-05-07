using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ProductionService : IProductionService
	{
		private readonly ICreateWorkspaceEntityStrategy<Production> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Production> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<Production> _getWorkspaceEntityByNameStrategy;
		private readonly IGetAllWorkspaceEntitiesStrategy<Production> _getAllWorkspaceEntitiesStrategy;
		private readonly IExistsWorkspaceEntityByIdStrategy<Production> _existsWorkspaceEntityByIdStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<Production> _deleteWorkspaceEntityByIdStrategy;
		private readonly IGetProductionStatusStrategy _getProductionStatusStrategy;
		private readonly IProductionsStageStrategy _productionsStageStrategy;
		private readonly IProductionsRunStrategy _productionsRunStrategy;

		public ProductionService(
			ICreateWorkspaceEntityStrategy<Production> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<Production> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<Production> getWorkspaceEntityByNameStrategy,
			IGetAllWorkspaceEntitiesStrategy<Production> getAllWorkspaceEntitiesStrategy,
			IExistsWorkspaceEntityByIdStrategy<Production> existsWorkspaceEntityByIdStrategy,
			IDeleteWorkspaceEntityByIdStrategy<Production> deleteWorkspaceEntityByIdStrategy,
			IGetProductionStatusStrategy getProductionStatusStrategy,
			IProductionsStageStrategy productionsStageStrategy,
			IProductionsRunStrategy productionsRunStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_getAllWorkspaceEntitiesStrategy = getAllWorkspaceEntitiesStrategy;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_getProductionStatusStrategy = getProductionStatusStrategy;
			_productionsStageStrategy = productionsStageStrategy;
			_productionsRunStrategy = productionsRunStrategy;
		}

		public Production Create(int workspaceId, Production entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public Production Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public Production Get(int workspaceId, string entityName)
			=> _getWorkspaceEntityByNameStrategy.Get(workspaceId, entityName);

		public Production[] GetAll(int workspaceId)
			=> _getAllWorkspaceEntitiesStrategy.GetAll(workspaceId);

		public bool Exists(int workspaceId, int entityId)
			=> _existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

		public void Delete(int workspaceId, int entityId)
		=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public ProductionStatus GetStatus(int workspaceId, int entityId)
			=> _getProductionStatusStrategy.GetStatus(workspaceId, entityId);

		public void Stage(int workspaceId, int entityId, int seconds = 60)
			=> _productionsStageStrategy.Stage(workspaceId, entityId, seconds);

		public void Run(int workspaceId, int entityId, int timeout = 120)
			=> _productionsRunStrategy.Run(workspaceId, entityId, timeout);
	}
}
