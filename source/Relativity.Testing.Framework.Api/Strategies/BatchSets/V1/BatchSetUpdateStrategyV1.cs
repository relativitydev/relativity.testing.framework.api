using System;
using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class BatchSetUpdateStrategyV1 : IUpdateBatchSetStrategy
	{
		private readonly IRestService _restService;

		public BatchSetUpdateStrategyV1(
			IRestService restService)
		{
			_restService = restService;
		}

		public BatchSet Update(int workspaceId, BatchSet entity, UserCredentials userCredentials = null)
		{
			if (workspaceId < -1 || workspaceId == 0)
			{
				throw new ArgumentException("Workspace ID should be greater than zero or equal to -1 for admin context.");
			}

			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID < 1)
			{
				throw new ArgumentException("Batch Set ID should be greater than zero.");
			}

			var currentBatchSet = _restService.Get<BatchSetDetailedDTOV1>(
				$"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets/{entity.ArtifactID}", userCredentials: userCredentials);

			var dto = new BatchSetDetailedDTOV1(entity, currentBatchSet);
			var request = new BatchSetRequestV1(dto);

			var updatedBatchSet = _restService.Put<BatchSetDetailedDTOV1>($"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets", request, userCredentials: userCredentials);
			var result = updatedBatchSet.Map();
			return result;
		}
	}
}
