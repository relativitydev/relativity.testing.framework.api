using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class CancelImagingJobStrategyV1 : ICancelImagingJobStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public CancelImagingJobStrategyV1(IRestService restService, IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public ImagingJobActionResponse Cancel(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest = null)
		{
			_workspaceIdValidator.Validate(workspaceId);

			var dto = BuildDto(cancelImagingJobRequest);
			var url = BuildUrl(workspaceId, imagingJobId);

			return _restService.Post<ImagingJobActionResponse>(url, dto);
		}

		public async Task<ImagingJobActionResponse> CancelAsync(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest = null)
		{
			_workspaceIdValidator.Validate(workspaceId);

			var dto = BuildDto(cancelImagingJobRequest);
			var url = BuildUrl(workspaceId, imagingJobId);

			return await _restService.PostAsync<ImagingJobActionResponse>(url, dto).ConfigureAwait(false);
		}

		private object BuildDto(ImagingJobRequest cancellImagingJobRequest)
		{
			return new
			{
				StopImagingJobRequest = cancellImagingJobRequest ?? new ImagingJobRequest()
			};
		}

		private string BuildUrl(int workspaceId, long imagingJobId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/jobs/{imagingJobId}/stop";
		}
	}
}
