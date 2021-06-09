using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Strategies.BatchSets;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class BatchSetService : IBatchSetService
	{
		private readonly ICreateBatchSetStrategy _createBatchSetStrategy;
		private readonly IGetBatchSetByIdStrategy _getBatchSetByIdStrategy;
		private readonly IExistsBatchSetByIdStrategy _existsBatchSetByIdStrategy;
		private readonly ICreateBatchesStrategy _createBatchesStrategy;
		private readonly IPurgeBatchesStrategy _purgeBatchesStrategy;

		public BatchSetService(
			ICreateBatchSetStrategy createBatchSetStrategy,
			IGetBatchSetByIdStrategy getBatchSetByIdStrategy,
			IExistsBatchSetByIdStrategy existsBatchSetByIdStrategy,
			ICreateBatchesStrategy createBatchesStrategy,
			IPurgeBatchesStrategy purgeBatchesStrategy)
		{
			_createBatchSetStrategy = createBatchSetStrategy;
			_getBatchSetByIdStrategy = getBatchSetByIdStrategy;
			_existsBatchSetByIdStrategy = existsBatchSetByIdStrategy;
			_createBatchesStrategy = createBatchesStrategy;
			_purgeBatchesStrategy = purgeBatchesStrategy;
		}

		public BatchSet Create(int workspaceId, BatchSet entity)
			=> Create(workspaceId, entity, null);

		public BatchSet Create(int workspaceId, BatchSet entity, UserCredentials userCredentials)
			=> _createBatchSetStrategy.Create(workspaceId, entity, userCredentials);

		public BatchSet Get(int workspaceId, int entityId)
			=> Get(workspaceId, entityId, null);

		public BatchSet Get(int workspaceId, int entityId, UserCredentials userCredentials)
			=> _getBatchSetByIdStrategy.Get(workspaceId, entityId, userCredentials);

		public bool Exists(int workspaceId, int entityId)
			=> Exists(workspaceId, entityId, null);

		public bool Exists(int workspaceId, int entityId, UserCredentials userCredentials)
			=> _existsBatchSetByIdStrategy.Exists(workspaceId, entityId, userCredentials);

		public BatchProcessResult CreateBatches(int workspaceId, int entityId)
			=> CreateBatches(workspaceId, entityId, null);

		public BatchProcessResult CreateBatches(int workspaceId, int entityId, UserCredentials userCredentials)
			=> _createBatchesStrategy.CreateBatches(workspaceId, entityId, userCredentials);

		public BatchProcessResult PurgeBatches(int workspaceId, int entityId)
			=> PurgeBatches(workspaceId, entityId, null);

		public BatchProcessResult PurgeBatches(int workspaceId, int entityId, UserCredentials userCredentials)
			=> _purgeBatchesStrategy.PurgeBatches(workspaceId, entityId, userCredentials);
	}
}
