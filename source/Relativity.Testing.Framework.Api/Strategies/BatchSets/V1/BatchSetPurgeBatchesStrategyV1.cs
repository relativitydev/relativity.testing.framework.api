using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class BatchSetPurgeBatchesStrategyV1 : IPurgeBatchesStrategy
	{
		private readonly IRestService _restService;

		public BatchSetPurgeBatchesStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public BatchProcessResult PurgeBatches(int workspaceId, int entityId, UserCredentials userCredentials = null)
		{
			var purgedBatchesCount = _restService.Post<int>(
				$"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets/{entityId}/batches/purge", userCredentials: userCredentials);

			var result = new BatchProcessResult
			{
				Count = purgedBatchesCount,
				Action = BatchProcessAction.Purge.ToString()
			};
			return result;
		}
	}
}
