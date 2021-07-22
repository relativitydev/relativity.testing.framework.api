using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ApplicationFieldCodeGetStrategyV1 : IApplicationFieldCodeGetStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;
		private readonly IArtifactIdValidator _artifactIdValidator;

		public ApplicationFieldCodeGetStrategyV1(IRestService restService, IWorkspaceIdValidator workspaceIdValidator, IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
			_artifactIdValidator = artifactIdValidator;
		}

		public ApplicationFieldCode Get(int workspaceId, int applicationFieldCodeId)
		{
			ValidateInput(workspaceId, applicationFieldCodeId);

			var url = BuildUrl(workspaceId, applicationFieldCodeId);

			return _restService.Get<ApplicationFieldCode>(url);
		}

		public async Task<ApplicationFieldCode> GetAsync(int workspaceId, int applicationFieldCodeId)
		{
			ValidateInput(workspaceId, applicationFieldCodeId);

			var url = BuildUrl(workspaceId, applicationFieldCodeId);

			return await _restService.GetAsync<ApplicationFieldCode>(url).ConfigureAwait(false);
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
