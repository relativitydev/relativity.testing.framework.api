using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingProfileDeleteStrategyV1 : IImagingProfileDeleteStrategy
	{
		private readonly IRestService _restService;

		public ImagingProfileDeleteStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public void Delete(int workspaceId, int imagingProfileId)
		{
			var url = BuildUrl(workspaceId, imagingProfileId);

			_restService.Delete(url);
		}

		public async Task DeleteAsync(int workspaceId, int imagingProfileId)
		{
			var url = BuildUrl(workspaceId, imagingProfileId);

			await _restService.DeleteAsync(url).ConfigureAwait(false);
		}

		private string BuildUrl(int workspaceId, int imagingProfileId)
		{
			return $"relativity-imaging/1/workspaces/{workspaceId}/imaging-profiles/{imagingProfileId}";
		}
	}
}
