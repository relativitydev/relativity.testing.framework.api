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
		private readonly IBatchSetValidatorV1 _validator;

		public BatchSetGetByIdStrategyV1(
			IRestService restService,
			IBatchSetValidatorV1 validator)
		{
			_restService = restService;
			_validator = validator;
		}

		public BatchSet Get(int workspaceId, int batchSetId, UserCredentials userCredentials = null)
		{
			_validator.ValidateWorkspaceIdAndExistingBatchSetId(workspaceId, batchSetId, userCredentials);

			var batchSetDTO = _restService.Get<BatchSetDetailedDTOV1>(
				$"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets/{batchSetId}", userCredentials: userCredentials);
			var result = batchSetDTO.Map();
			return result;
		}
	}
}
