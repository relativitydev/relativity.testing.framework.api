using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class BatchSetDeleteStrategyV1 : IDeleteBatchSetStrategy
	{
		private readonly IRestService _restService;
		private readonly IBatchSetValidatorV1 _validator;

		public BatchSetDeleteStrategyV1(IRestService restService, IBatchSetValidatorV1 validator)
		{
			_restService = restService;
			_validator = validator;
		}

		public void Delete(int workspaceId, int entityId, UserCredentials userCredentials = null)
		{
			_validator.ValidateWorkspaceIdAndExistingBatchSetId(workspaceId, entityId, userCredentials);

			_restService.Delete(
				$"/Relativity.REST/api/relativity-review/v1/workspaces/{workspaceId}/batch-sets/{entityId}", userCredentials: userCredentials);
		}
	}
}
