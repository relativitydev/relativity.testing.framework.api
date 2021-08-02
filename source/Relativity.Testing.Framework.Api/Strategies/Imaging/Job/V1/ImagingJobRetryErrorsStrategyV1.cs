using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingJobRetryErrorsStrategyV1 : IImagingJobRetryErrorsStrategy
	{
		private readonly IRestService _restService;
		private readonly IImagingSetValidatorV1 _imagingSetValidator;

		public ImagingJobRetryErrorsStrategyV1(
			IRestService restService,
			IImagingSetValidatorV1 imagingSetValidator)
		{
			_restService = restService;
			_imagingSetValidator = imagingSetValidator;
		}

		public long RetryErrors(int workspaceId, int imagingSetId, ImagingSetJobRequest retryErrorsRequest = null)
		{
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);

			var dto = BuildDto(retryErrorsRequest);
			var url = BuildUrl(workspaceId, imagingSetId);

			var result = _restService.Post<JObject>(url, dto);
			return (long)result["ImagingJobID"];
		}

		public async Task<long> RetryErrorsAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest retryErrorsRequest = null)
		{
			_imagingSetValidator.ValidateIds(workspaceId, imagingSetId);

			var dto = BuildDto(retryErrorsRequest);
			var url = BuildUrl(workspaceId, imagingSetId);

			var result = await _restService.PostAsync<JObject>(url, dto).ConfigureAwait(false);
			return (long)result["ImagingJobID"];
		}

		private object BuildDto(ImagingSetJobRequest retryErrorsRequest)
		{
			return new
			{
				ImagingSetRequest = retryErrorsRequest ?? new ImagingSetJobRequest()
			};
		}

		private string BuildUrl(int workspaceId, int imagingSetId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/imaging-sets/{imagingSetId}/retry-errors";
		}
	}
}
