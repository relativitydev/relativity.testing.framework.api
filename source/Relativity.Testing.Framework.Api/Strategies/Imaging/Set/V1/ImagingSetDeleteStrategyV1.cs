using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingSetDeleteStrategyV1 : IImagingSetDeleteStrategy
	{
		private const string _BASE_DELETE_URL_FORMAT = "relativity-imaging/v1/workspaces/{0}/imaging-sets/{1}";
		private readonly IRestService _restService;
		private readonly IImagingSetValidatorV1 _imagingSetValidator;

		public ImagingSetDeleteStrategyV1(
			IRestService restService,
			IImagingSetValidatorV1 imagingSetValidator)
		{
			_restService = restService;
			_imagingSetValidator = imagingSetValidator;
		}

		public void Delete(int workspaceId, int imagingSetId)
		{
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);
			string url = GetUrlWithParameters(workspaceId, imagingSetId);

			_restService.Delete(url);
		}

		public async Task DeleteAsync(int workspaceId, int imagingSetId)
		{
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);
			string url = GetUrlWithParameters(workspaceId, imagingSetId);

			await _restService.DeleteAsync(url).ConfigureAwait(false);
		}

		private static string GetUrlWithParameters(int workspaceId, int imagingSetId)
		{
			return string.Format(_BASE_DELETE_URL_FORMAT, workspaceId, imagingSetId);
		}
	}
}
