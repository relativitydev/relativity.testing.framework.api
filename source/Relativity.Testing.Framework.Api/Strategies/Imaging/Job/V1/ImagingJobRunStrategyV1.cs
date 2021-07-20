using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingJobRunStrategyV1 : IImagingJobRunStrategy
	{
		private const string _BASE_RUN_URL_FORMAT = "relativity-imaging/v1/workspaces/{0}/imaging-sets/{1}/run";
		private readonly IRestService _restService;
		private readonly IImagingSetValidatorV1 _imagingSetValidator;

		public ImagingJobRunStrategyV1(
			IRestService restService,
			IImagingSetValidatorV1 imagingSetValidator)
		{
			_restService = restService;
			_imagingSetValidator = imagingSetValidator;
		}

		public int Run(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
		{
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);
			string url = GetUrlWithParameters(workspaceId, imagingSetId);
			var imagingSetRequest = new ImagingJobRequestDtoV1(imagingSetJobRequest);

			ImagingJobIdResponseDtoV1 result = _restService.Post<ImagingJobIdResponseDtoV1>(url, imagingSetRequest);

			return result.ImagingJobID;
		}

		public async Task<int> RunAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null)
		{
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);
			string url = GetUrlWithParameters(workspaceId, imagingSetId);
			ImagingJobRequestDtoV1 imagingSetRequest = new ImagingJobRequestDtoV1(imagingSetJobRequest);

			ImagingJobIdResponseDtoV1 result = await _restService.PostAsync<ImagingJobIdResponseDtoV1>(url, imagingSetRequest)
				.ConfigureAwait(false);

			return result.ImagingJobID;
		}

		private static string GetUrlWithParameters(int workspaceId, int imagingSetId)
		{
			return string.Format(_BASE_RUN_URL_FORMAT, workspaceId, imagingSetId);
		}
	}
}
