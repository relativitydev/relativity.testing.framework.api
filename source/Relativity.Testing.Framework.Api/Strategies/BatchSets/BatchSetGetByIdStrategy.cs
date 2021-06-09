using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies.BatchSets;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetGetByIdStrategy : IGetBatchSetByIdStrategy
	{
		private readonly IRestService _restService;
		private readonly IExistsBatchSetByIdStrategy _existsBatchSetByIdStrategy;

		public BatchSetGetByIdStrategy(
			IRestService restService,
			IExistsBatchSetByIdStrategy existsBatchSetByIdStrategy)
		{
			_restService = restService;
			_existsBatchSetByIdStrategy = existsBatchSetByIdStrategy;
		}

		public BatchSet Get(int workspaceId, int batchSetId, UserCredentials userCredentials = null)
		{
			if (!_existsBatchSetByIdStrategy.Exists(workspaceId, batchSetId, userCredentials: userCredentials))
			{
				return null;
			}

			return _restService.Get<BatchSet>($"Relativity.Services.Review.Batching.IBatchingModule/workspaces/{workspaceId}/batching/ReadFull/{batchSetId}", userCredentials: userCredentials);
		}
	}
}
