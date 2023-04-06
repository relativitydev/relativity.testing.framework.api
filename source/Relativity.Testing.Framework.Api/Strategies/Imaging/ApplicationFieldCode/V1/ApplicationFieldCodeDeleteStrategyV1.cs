using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ApplicationFieldCodeDeleteStrategyV1 : IApplicationFieldCodeDeleteStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;
		private readonly IArtifactIdValidator _artifactIdValidator;

		public ApplicationFieldCodeDeleteStrategyV1(
			IRestService restService,
			IWorkspaceIdValidator workspaceIdValidator,
			IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
			_artifactIdValidator = artifactIdValidator;
		}

		public void Delete(int workspaceId, int applicationFieldCodeId)
		{
			ValidateInput(workspaceId, applicationFieldCodeId);

			var url = BuildUrl(workspaceId, applicationFieldCodeId);

			_restService.Delete<bool>(url);
		}

		private void ValidateInput(int workspaceId, int applicationFieldCodeId)
		{
			_workspaceIdValidator.Validate(workspaceId);
			_artifactIdValidator.Validate(applicationFieldCodeId, "ApplicationFieldCode");
		}

		private string BuildUrl(int workspaceId, int applicationFieldCodeId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/application-field-codes/{applicationFieldCodeId}";
		}
	}
}
