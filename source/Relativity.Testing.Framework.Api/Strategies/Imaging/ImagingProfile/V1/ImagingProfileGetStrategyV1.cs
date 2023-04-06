using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingProfileGetStrategyV1 : IImagingProfileGetStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;
		private readonly IArtifactIdValidator _artifactIdValidator;

		public ImagingProfileGetStrategyV1(IRestService restService, IWorkspaceIdValidator workspaceIdValidator, IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
			_artifactIdValidator = artifactIdValidator;
		}

		public ImagingProfile Get(int workspaceId, int imagingProfileId)
		{
			ValidateInput(workspaceId, imagingProfileId);

			var url = BuildUrl(workspaceId, imagingProfileId);

			return _restService.Get<ImagingProfile>(url);
		}

		private void ValidateInput(int workspaceId, int imagingProfileId)
		{
			_workspaceIdValidator.Validate(workspaceId);
			_artifactIdValidator.Validate(imagingProfileId, "ImagingProfile");
		}

		private string BuildUrl(int workspaceId, int imagingProfileId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/imaging-profiles/{imagingProfileId}";
		}
	}
}
