using System;
using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class BatchSetGetByIdStrategyV1 : IGetBatchSetByIdStrategy
	{
		private readonly IRestService _restService;
		private readonly IExistsBatchSetByIdStrategy _existsBatchSetByIdStrategy;

		public BatchSetGetByIdStrategyV1(
			IRestService restService,
			IExistsBatchSetByIdStrategy existsBatchSetByIdStrategy)
		{
			_restService = restService;
			_existsBatchSetByIdStrategy = existsBatchSetByIdStrategy;
		}

		public BatchSet Get(int workspaceId, int batchSetId, UserCredentials userCredentials = null)
		{
			if (workspaceId < -1 || workspaceId == 0)
			{
				throw new ArgumentException("Workspace ID should be greater than zero or equal to -1 for admin context.");
			}

			if (batchSetId < 1)
			{
				throw new ArgumentException("Batch Set ID should be greater than zero.");
			}

			if (!_existsBatchSetByIdStrategy.Exists(workspaceId, batchSetId, userCredentials: userCredentials))
			{
				throw new ArgumentException($"The batch set with ID: {batchSetId} does not exists.");
			}

			var batchSetDTO = _restService.Get<BatchSetDetailedDTOV1>(
				$"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets/{batchSetId}", userCredentials: userCredentials);
			var result = batchSetDTO.Map();
			return result;
		}
	}
}
