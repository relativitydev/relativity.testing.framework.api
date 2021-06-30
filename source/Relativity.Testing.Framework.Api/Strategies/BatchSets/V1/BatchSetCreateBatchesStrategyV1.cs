using System;
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
		private readonly IExistsBatchSetByIdStrategy _existsBatchSetByIdStrategy;

		public BatchSetCreateBatchesStrategyV1(
			IRestService restService,
			IExistsBatchSetByIdStrategy existsBatchSetByIdStrategy)
		{
			_restService = restService;
			_existsBatchSetByIdStrategy = existsBatchSetByIdStrategy;
		}

		public BatchProcessResult CreateBatches(int workspaceId, int entityId, UserCredentials userCredentials = null)
		{
			if (workspaceId < -1 || workspaceId == 0)
			{
				throw new ArgumentException("Workspace ID should be greater than zero or equal to -1 for admin context.");
			}

			if (entityId < 1)
			{
				throw new ArgumentException("Batch Set ID should be greater than zero.");
			}

			if (!_existsBatchSetByIdStrategy.Exists(workspaceId, entityId, userCredentials: userCredentials))
			{
				throw new ArgumentException($"The batch set with ID: {entityId} does not exists.");
			}

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
