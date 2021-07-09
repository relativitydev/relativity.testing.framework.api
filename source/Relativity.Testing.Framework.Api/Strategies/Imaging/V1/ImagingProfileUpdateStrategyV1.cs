using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingProfileUpdateStrategyV1 : IImagingProfileUpdateStrategy
	{
		private readonly IRestService _restService;

		public ImagingProfileUpdateStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, ImagingProfile imagingProfile)
		{
			var url = BuildUrl(workspaceId, imagingProfile.ArtifactID);

			var dto = imagingProfile.MapToUpdateDTO();

			_restService.Post(url, dto);
		}

		public async Task UpdateAsync(int workspaceId, ImagingProfile imagingProfile)
		{
			var url = BuildUrl(workspaceId, imagingProfile.ArtifactID);

			var dto = imagingProfile.MapToUpdateDTO();

			await _restService.PostAsync(url, dto).ConfigureAwait(false);
		}

		private string BuildUrl(int workspaceId, int imagingProfileId)
		{
			return $"relativity-imaging/1/workspaces/{workspaceId}/imaging-profiles/{imagingProfileId}";
		}
	}
}
