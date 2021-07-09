using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingProfileGetStrategyV1 : IImagingProfileGetStrategy
	{
		private readonly IRestService _restService;

		public ImagingProfileGetStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public ImagingProfile Get(int workspaceId, int imagingProfileId)
		{
			var url = BuildUrl(workspaceId, imagingProfileId);

			return _restService.Get<ImagingProfile>(url);
		}

		public async Task<ImagingProfile> GetAsync(int workspaceId, int imagingProfileId)
		{
			var url = BuildUrl(workspaceId, imagingProfileId);

			return await _restService.GetAsync<ImagingProfile>(url).ConfigureAwait(false);
		}

		private string BuildUrl(int workspaceId, int imagingProfileId)
		{
			return $"relativity-imaging/1/workspaces/{workspaceId}/imaging-profiles/{imagingProfileId}";
		}
	}
}
