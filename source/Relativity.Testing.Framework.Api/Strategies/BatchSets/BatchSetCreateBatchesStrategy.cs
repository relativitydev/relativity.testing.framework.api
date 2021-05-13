using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetCreateBatchesStrategy : ICreateBatchesStrategy
	{
		private readonly IRestService _restService;

		public BatchSetCreateBatchesStrategy(
			IRestService restService)
		{
			_restService = restService;
		}

		public BatchProcessResult CreateBatches(int workspaceId, int entityId)
		{
			var dto = new
			{
				workspaceID = workspaceId,
				batchSetArtifactID = entityId
			};

			return _restService.Post<BatchProcessResult>("Relativity.Services.Review.Batching.IBatchingModule/BatchSetManager/CreateBatchesAsync", dto);
		}
	}
}
