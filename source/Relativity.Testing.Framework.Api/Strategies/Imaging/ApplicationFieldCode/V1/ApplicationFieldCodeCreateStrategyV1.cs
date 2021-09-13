using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ApplicationFieldCodeCreateStrategyV1 : IApplicationFieldCodeCreateStrategy
	{
		private readonly IRestService _restService;
		private readonly IApplicationFieldCodeGetStrategy _applicationFieldCodeGetStrategy;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public ApplicationFieldCodeCreateStrategyV1(
			IRestService restService,
			IApplicationFieldCodeGetStrategy applicationFieldCodeGetStrategy,
			IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_applicationFieldCodeGetStrategy = applicationFieldCodeGetStrategy;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public ApplicationFieldCode Create(int workspaceId, ApplicationFieldCode applicationFieldCode)
		{
			ValidateInput(workspaceId, applicationFieldCode);

			var url = BuildUrl(workspaceId);

			var dto = BuildRequest(applicationFieldCode);

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

		private string BuildUrl(int workspaceId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/application-field-codes";
		}

		private object BuildRequest(ApplicationFieldCode applicationFieldCode)
		{
			return new
			{
				applicationFieldCode = applicationFieldCode.MapToCreateRequest()
			};
		}
	}
}
