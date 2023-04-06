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
		private readonly IBatchSetValidatorV1 _validator;

		public BatchSetUpdateStrategyV1(
			IRestService restService,
			IBatchSetValidatorV1 validator)
		{
			_restService = restService;
			_validator = validator;
		}

		public BatchSet Update(int workspaceId, BatchSet entity, UserCredentials userCredentials = null)
		{
			_validator.ValidateUpdateArguments(workspaceId, entity, userCredentials);

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
