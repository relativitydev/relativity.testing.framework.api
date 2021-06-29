using System;
using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class BatchSetDeleteStrategyV1 : IDeleteBatchSetStrategy
	{
		private readonly IRestService _restService;
		private readonly IExistsBatchSetByIdStrategy _existsBatchSetByIdStrategy;

		public BatchSetDeleteStrategyV1(IRestService restService, IExistsBatchSetByIdStrategy existsBatchSetByIdStrategy)
		{
			_restService = restService;
			_existsBatchSetByIdStrategy = existsBatchSetByIdStrategy;
		}

		public void Delete(int workspaceId, int entityId, UserCredentials userCredentials = null)
		{
			if (!_existsBatchSetByIdStrategy.Exists(workspaceId, entityId, userCredentials: userCredentials))
			{
				throw new ArgumentException($"The batch set with {entityId} does not exists.");
			}

			_restService.Delete(
				$"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets/{entityId}", userCredentials: userCredentials);
		}
	}
}
