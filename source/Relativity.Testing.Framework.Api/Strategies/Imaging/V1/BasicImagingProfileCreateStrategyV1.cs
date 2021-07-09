using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class BasicImagingProfileCreateStrategyV1 : IBasicImagingProfileCreateStrategy
	{
		private readonly IRestService _restService;
		private readonly IImagingProfileGetStrategy _imagingProfileGetStrategy;

		public BasicImagingProfileCreateStrategyV1(IRestService restService, IImagingProfileGetStrategy imagingProfileGetStrategy)
		{
			_restService = restService;
			_imagingProfileGetStrategy = imagingProfileGetStrategy;
		}

		public ImagingProfile Create(int workspaceId, CreateBasicImagingProfileDTO dto)
		{
			var url = BuildUrl(workspaceId);

			var imagingProfileId = _restService.Post<int>(url, dto);

			return _imagingProfileGetStrategy.Get(workspaceId, imagingProfileId);
		}

		public async Task<ImagingProfile> CreateAsync(int workspaceId, CreateBasicImagingProfileDTO dto)
		{
			var url = BuildUrl(workspaceId);

			var imagingProfileId = await _restService.PostAsync<int>(url, dto).ConfigureAwait(false);

			return await _imagingProfileGetStrategy.GetAsync(workspaceId, imagingProfileId).ConfigureAwait(false);
		}

		private string BuildUrl(int workspaceId)
		{
			return $"relativity-imaging/1/workspaces/{workspaceId}/imaging-profiles/basic";
		}
	}
}
