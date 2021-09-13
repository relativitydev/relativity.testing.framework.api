using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingSetGetStrategyV1 : IImagingSetGetStrategy
	{
		private const string _BASE_GET_URL_FORMAT = "relativity-imaging/v1/workspaces/{0}/imaging-sets/{1}";
		private readonly IRestService _restService;
		private readonly IImagingSetValidatorV1 _imagingSetValidator;

		public ImagingSetGetStrategyV1(
			IRestService restService,
			IImagingSetValidatorV1 imagingSetValidator)
		{
			_restService = restService;
			_imagingSetValidator = imagingSetValidator;
		}

		public ImagingSet Get(int workspaceId, int imagingSetId)
		{
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);
			string url = GetUrlWithParameters(workspaceId, imagingSetId);

			var result = _restService.Get<ImagingSet>(url);

			return result;
		}

		private static string GetUrlWithParameters(int workspaceId, int imagingSetId)
		{
			return string.Format(_BASE_GET_URL_FORMAT, workspaceId, imagingSetId);
		}
	}
}
