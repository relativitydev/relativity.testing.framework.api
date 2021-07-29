using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingJobUpdatePriorityStrategyV1 : IImagingJobUpdatePriorityStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public ImagingJobUpdatePriorityStrategyV1(
			IRestService restService,
			IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public ImagingJobActionResponse UpdatePriority(int workspaceId, long imagingJobId, ImagingJobPriorityRequest updateJobPriorityRequest)
		{
			_workspaceIdValidator.Validate(workspaceId);

			var dto = BuildDto(updateJobPriorityRequest);
			var url = BuildUrl(workspaceId, imagingJobId);

			return _restService.Post<ImagingJobActionResponse>(url, dto);
		}

		public async Task<ImagingJobActionResponse> UpdatePriorityAsync(int workspaceId, long imagingJobId, ImagingJobPriorityRequest updateJobPriorityRequest)
		{
			_workspaceIdValidator.Validate(workspaceId);

			var dto = BuildDto(updateJobPriorityRequest);
			var url = BuildUrl(workspaceId, imagingJobId);

			return await _restService.PostAsync<ImagingJobActionResponse>(url, dto).ConfigureAwait(false);
		}

		private object BuildDto(ImagingJobPriorityRequest updateJobPriorityRequest)
		{
			return new
			{
				UpdateJobPriorityRequest = updateJobPriorityRequest
			};
		}

		private string BuildUrl(int workspaceId, long imagingJobId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/jobs/{imagingJobId}/priority";
		}
	}
}
