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
		private readonly IBatchSetValidatorV1 _validator;

		public BatchSetCreateStrategyV1(IRestService restService, IBatchSetValidatorV1 validator)
		{
			_restService = restService;
			_validator = validator;
		}

		public BatchSet Create(int workspaceId, BatchSet entity, UserCredentials userCredentials = null)
		{
			_validator.ValidateWorkspaceId(workspaceId);

			var dto = new BatchSetDTOV1(entity);
			var request = new BatchSetRequestV1(dto);

			var createdBatchSet = _restService.Post<BatchSetDetailedDTOV1>($"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets", request, userCredentials: userCredentials);
			var result = createdBatchSet.Map();
			return result;
		}
	}
}
