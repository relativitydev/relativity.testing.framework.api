using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingSetReleaseStrategyV1 : IImagingSetReleaseStrategy
	{
		private const string _BASE_RELEASE_URL_FORMAT = "relativity-imaging/v1/workspaces/{0}/imaging-sets/{1}/release-images";
		private readonly IRestService _restService;
		private readonly IImagingSetValidatorV1 _imagingSetValidator;

		public ImagingSetReleaseStrategyV1(
			IRestService restService,
			IImagingSetValidatorV1 imagingSetValidator)
		{
			_restService = restService;
			_imagingSetValidator = imagingSetValidator;
		}

		public void Release(int workspaceId, int imagingSetId)
		{
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);
			string url = GetUrlWithParameters(workspaceId, imagingSetId);

			_restService.Post(url);
		}

		private static string GetUrlWithParameters(int workspaceId, int imagingSetId)
		{
			return string.Format(_BASE_RELEASE_URL_FORMAT, workspaceId, imagingSetId);
		}
	}
}
