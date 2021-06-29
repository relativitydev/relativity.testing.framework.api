using System;
using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class BatchSetCreateStrategyV1 : ICreateBatchSetStrategy
	{
		private readonly IRestService _restService;
		private readonly IBatchSetDTOMapStrategyV1 _batchSetDTOMapper;

		public BatchSetCreateStrategyV1(IRestService restService, IBatchSetDTOMapStrategyV1 batchSetDTOMapper)
		{
			_restService = restService;
			_batchSetDTOMapper = batchSetDTOMapper;
		}

		public BatchSet Create(int workspaceId, BatchSet entity, UserCredentials userCredentials = null)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var dto = new BatchSetDTOV1(entity);
			var request = new BatchSetRequestV1(dto);

			var createdBatchSet = _restService.Post<BatchSetDetailedDTOV1>($"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets", request, userCredentials: userCredentials);
			var result = _batchSetDTOMapper.Map(createdBatchSet);
			return result;
		}
	}
}
