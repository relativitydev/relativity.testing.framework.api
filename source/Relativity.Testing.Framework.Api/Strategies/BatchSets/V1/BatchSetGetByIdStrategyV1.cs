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
		private readonly IBatchSetDTOMapStrategyV1 _batchSetDTOMapper;

		public BatchSetGetByIdStrategyV1(
			IRestService restService,
			IExistsBatchSetByIdStrategy existsBatchSetByIdStrategy,
			IBatchSetDTOMapStrategyV1 batchSetDTOMapper)
		{
			_restService = restService;
			_existsBatchSetByIdStrategy = existsBatchSetByIdStrategy;
			_batchSetDTOMapper = batchSetDTOMapper;
		}

		public BatchSet Get(int workspaceId, int batchSetId, UserCredentials userCredentials = null)
		{
			if (!_existsBatchSetByIdStrategy.Exists(workspaceId, batchSetId, userCredentials: userCredentials))
			{
				return null;
			}

			var batchSetDTO = _restService.Get<BatchSetDetailedDTOV1>(
				$"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets/{batchSetId}", userCredentials: userCredentials);
			var result = _batchSetDTOMapper.Map(batchSetDTO);
			return result;
		}
	}
}
