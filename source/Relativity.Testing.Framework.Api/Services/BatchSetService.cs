using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class BatchSetService : IBatchSetService
	{
		private readonly ICreateWorkspaceEntityStrategy<BatchSet> _createWorkspaceEntityStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<BatchSet> _getWorkspaceEntityByIdStrategy;
		private readonly IExistsWorkspaceEntityByIdStrategy<BatchSet> _existsWorkspaceEntityByIdStrategy;
		private readonly ICreateBatchesStrategy _createBatchesStrategy;
		private readonly IPurgeBatchesStrategy _purgeBatchesStrategy;

		public BatchSetService(
			ICreateWorkspaceEntityStrategy<BatchSet> createWorkspaceEntityStrategy,
			IGetWorkspaceEntityByIdStrategy<BatchSet> getWorkspaceEntityByIdStrategy,
			IExistsWorkspaceEntityByIdStrategy<BatchSet> existsWorkspaceEntityByIdStrategy,
			ICreateBatchesStrategy createBatchesStrategy,
			IPurgeBatchesStrategy purgeBatchesStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
			_createBatchesStrategy = createBatchesStrategy;
			_purgeBatchesStrategy = purgeBatchesStrategy;
		}

		public BatchSet Create(int workspaceId, BatchSet entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public BatchSet Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public bool Exists(int workspaceId, int entityId)
			=> _existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

		public BatchProcessResult CreateBatches(int workspaceId, int entityId)
			=> _createBatchesStrategy.CreateBatches(workspaceId, entityId);

		public BatchProcessResult PurgeBatches(int workspaceId, int entityId)
			=> _purgeBatchesStrategy.PurgeBatches(workspaceId, entityId);
	}
}
