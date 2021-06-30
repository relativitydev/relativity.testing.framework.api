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

		public BatchSetCreateStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public BatchSet Create(int workspaceId, BatchSet entity, UserCredentials userCredentials = null)
		{
			if (workspaceId < -1 || workspaceId == 0)
			{
				throw new ArgumentException("Workspace ID should be greater than zero or equal to -1 for admin context.");
			}

			var dto = new BatchSetDTOV1(entity);
			var request = new BatchSetRequestV1(dto);

			var createdBatchSet = _restService.Post<BatchSetDetailedDTOV1>($"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets", request, userCredentials: userCredentials);
			var result = createdBatchSet.Map();
			return result;
		}
	}
}
