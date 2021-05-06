using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetPurgeBatchesStrategy : IPurgeBatchesStrategy
	{
		private readonly IRestService _restService;

		public BatchSetPurgeBatchesStrategy(
			IRestService restService)
		{
			_restService = restService;
		}

		public BatchProcessResult PurgeBatches(int workspaceId, int entityId)
		{
		var dto = new
		{
			workspaceID = workspaceId,
			batchSetArtifactID = entityId
		};

		return _restService.Post<BatchProcessResult>("Relativity.Services.Review.Batching.IBatchingModule/BatchSetManager/PurgeBatchesAsync", dto);
		}
	}
}
