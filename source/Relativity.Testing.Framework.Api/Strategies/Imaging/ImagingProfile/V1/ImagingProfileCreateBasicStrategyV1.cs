using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingProfileCreateBasicStrategyV1 : IImagingProfileCreateBasicStrategy
	{
		private readonly IRestService _restService;
		private readonly IImagingProfileGetStrategy _imagingProfileGetStrategy;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public ImagingProfileCreateBasicStrategyV1(IRestService restService, IImagingProfileGetStrategy imagingProfileGetStrategy, IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_imagingProfileGetStrategy = imagingProfileGetStrategy;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public ImagingProfile Create(int workspaceId, CreateBasicImagingProfileDTO dto)
		{
			ValidateInput(workspaceId, dto);

			var url = BuildUrl(workspaceId);
			var request = BuildRequest(dto);

			var imagingProfileId = _restService.Post<int>(url, request);

			return _imagingProfileGetStrategy.Get(workspaceId, imagingProfileId);
		}

		private void ValidateInput(int workspaceId, CreateBasicImagingProfileDTO input)
		{
			if (input is null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			_workspaceIdValidator.Validate(workspaceId);
		}

		private string BuildUrl(int workspaceId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/imaging-profiles/basic";
		}

		private object BuildRequest(CreateBasicImagingProfileDTO dto)
		{
			return new
			{
				request = dto
			};
		}
	}
}
