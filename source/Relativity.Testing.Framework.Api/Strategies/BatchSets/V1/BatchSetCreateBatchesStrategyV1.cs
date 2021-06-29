using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class BatchSetCreateBatchesStrategyV1 : ICreateBatchesStrategy
	{
		private readonly IRestService _restService;

		public BatchSetCreateBatchesStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public BatchProcessResult CreateBatches(int workspaceId, int entityId, UserCredentials userCredentials = null)
		{
			var createdBatchesCount = _restService.Post<int>(
				$"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets/{entityId}/batches/create", userCredentials: userCredentials);

			var result = new BatchProcessResult
			{
				Count = createdBatchesCount,
				Action = BatchProcessAction.Create.ToString()
			};
			return result;
		}
	}
}
