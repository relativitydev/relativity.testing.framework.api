using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingSetUpdateStrategyV1 : IImagingSetUpdateStrategy
	{
		private const string _BASE_UPDATE_URL_FORMAT = "relativity-imaging/v1/workspaces/{0}/imaging-sets/{1}";
		private readonly IRestService _restService;
		private readonly IImagingSetValidatorV1 _imagingSetValidator;

		public ImagingSetUpdateStrategyV1(
			IRestService restService,
			IImagingSetValidatorV1 imagingSetValidator)
		{
			_restService = restService;
			_imagingSetValidator = imagingSetValidator;
		}

		public int Update(int workspaceId, int imagingSetId, ImagingSetRequest imagingSetRequest)
		{
			_imagingSetValidator.ValidateImagingSetUpdateRequest(workspaceId, imagingSetId, imagingSetRequest);

			string url = GetUrlWithParameters(workspaceId, imagingSetId);
			var dto = new ImagingSetRequestDTOV1(imagingSetRequest);

			var updatedImagingSetId = _restService.Post<int>(url, dto);

			return updatedImagingSetId;
		}

		public async Task<int> UpdateAsync(int workspaceId, int imagingSetId, ImagingSetRequest imagingSetRequest)
		{
			_imagingSetValidator.ValidateImagingSetUpdateRequest(workspaceId, imagingSetId, imagingSetRequest);

			string url = GetUrlWithParameters(workspaceId, imagingSetId);
			var dto = new ImagingSetRequestDTOV1(imagingSetRequest);

			var updatedImagingSetId = await _restService.PostAsync<int>(url, dto).ConfigureAwait(false);

			return updatedImagingSetId;
		}

		private static string GetUrlWithParameters(int workspaceId, int imagingSetId)
		{
			return string.Format(_BASE_UPDATE_URL_FORMAT, workspaceId, imagingSetId);
		}
	}
}
