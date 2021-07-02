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
		private readonly IBatchSetValidatorV1 _validator;

		public BatchSetPurgeBatchesStrategyV1(
			IRestService restService,
			IBatchSetValidatorV1 validator)
		{
			_restService = restService;
			_validator = validator;
		}

		public BatchProcessResult PurgeBatches(int workspaceId, int entityId, UserCredentials userCredentials = null)
		{
			_validator.ValidateWorkspaceIdAndExistingBatchSetId(workspaceId, entityId, userCredentials);

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
