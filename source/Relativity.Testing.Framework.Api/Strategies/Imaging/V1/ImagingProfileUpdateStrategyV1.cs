using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingProfileUpdateStrategyV1 : IImagingProfileUpdateStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public ImagingProfileUpdateStrategyV1(IRestService restService, IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public void Update(int workspaceId, ImagingProfile imagingProfile)
		{
			ValidateInput(workspaceId, imagingProfile);

			var url = BuildUrl(workspaceId, imagingProfile.ArtifactID);
			var request = BuildRequest(imagingProfile);

			_restService.Post(url, request);
		}

		public async Task UpdateAsync(int workspaceId, ImagingProfile imagingProfile)
		{
			ValidateInput(workspaceId, imagingProfile);

			var url = BuildUrl(workspaceId, imagingProfile.ArtifactID);
			var request = BuildRequest(imagingProfile);

			await _restService.PostAsync(url, request).ConfigureAwait(false);
		}

		private void ValidateInput(int workspaceId, ImagingProfile input)
		{
			if (input is null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			_workspaceIdValidator.Validate(workspaceId);
		}

		private string BuildUrl(int workspaceId, int imagingProfileId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/imaging-profiles/{imagingProfileId}";
		}

		private object BuildRequest(ImagingProfile imagingProfile)
		{
			return new
			{
				request = imagingProfile.MapToUpdateDTO()
			};
		}
	}
}
