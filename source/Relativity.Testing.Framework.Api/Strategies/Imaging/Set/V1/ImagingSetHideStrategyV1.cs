using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingSetHideStrategyV1 : IImagingSetHideStrategy
	{
		private const string _BASE_HIDE_URL_FORMAT = "relativity-imaging/v1/workspaces/{0}/imaging-sets/{1}/hide-images";
		private readonly IRestService _restService;
		private readonly IImagingSetValidatorV1 _imagingSetValidator;

		public ImagingSetHideStrategyV1(
			IRestService restService,
			IImagingSetValidatorV1 imagingSetValidator)
		{
			_restService = restService;
			_imagingSetValidator = imagingSetValidator;
		}

		public void Hide(int workspaceId, int imagingSetId)
		{
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);
			string url = GetUrlWithParameters(workspaceId, imagingSetId);

			_restService.Post(url);
		}

		public async Task HideAsync(int workspaceId, int imagingSetId)
		{
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);
			string url = GetUrlWithParameters(workspaceId, imagingSetId);

			await _restService.PostAsync(url).ConfigureAwait(false);
		}

		private static string GetUrlWithParameters(int workspaceId, int imagingSetId)
		{
			return string.Format(_BASE_HIDE_URL_FORMAT, workspaceId, imagingSetId);
		}
	}
}
