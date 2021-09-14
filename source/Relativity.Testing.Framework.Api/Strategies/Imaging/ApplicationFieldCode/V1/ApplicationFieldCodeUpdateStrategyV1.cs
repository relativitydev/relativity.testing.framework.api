using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ApplicationFieldCodeUpdateStrategyV1 : IApplicationFieldCodeUpdateStrategy
	{
		private readonly IRestService _restService;
		private readonly IApplicationFieldCodeGetStrategy _applicationFieldCodeGetStrategy;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public ApplicationFieldCodeUpdateStrategyV1(
			IRestService restService,
			IApplicationFieldCodeGetStrategy applicationFieldCodeGetStrategy,
			IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_applicationFieldCodeGetStrategy = applicationFieldCodeGetStrategy;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public ApplicationFieldCode Update(int workspaceId, ApplicationFieldCode applicationFieldCode)
		{
			ValidateInput(workspaceId, applicationFieldCode);

			var url = BuildUrl(workspaceId, applicationFieldCode.ArtifactID);

			var dto = new
			{
				applicationFieldCode
			};

			var applicationFieldCodeId = _restService.Post<int>(url, dto);

			return _applicationFieldCodeGetStrategy.Get(workspaceId, applicationFieldCodeId);
		}

		private void ValidateInput(int workspaceId, ApplicationFieldCode applicationFieldCode)
		{
			_workspaceIdValidator.Validate(workspaceId);

			if (applicationFieldCode is null)
			{
				throw new ArgumentNullException(nameof(applicationFieldCode));
			}
		}

		private string BuildUrl(int workspaceId, int applicationFieldCodeID)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/application-field-codes/{applicationFieldCodeID}";
		}
	}
}
