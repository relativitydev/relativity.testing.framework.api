using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the Imaging Job API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _imagingJobService = relativityFacade.Resolve&lt;IImagingJobService&gt;();
	/// </code>
	/// </example>
	public interface IImagingJobService
	{
		/// <summary>
		/// Schedules an imaging job.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging job.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <param name="imagingSetJobRequest">
		/// Request for imaging job on imaging set can set QcEnabled property and OriginationID.
		/// Can be skipped to use default false value for QcEnabled and null for OriginationID.
		/// </param>
		/// <returns>The Artifact ID for the job.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imaginingSetId = 2;
		/// ImagingSetJobRequest imagingSetJobRequest = new ImagingSetJobRequest();
		/// imagingSetJobRequest.QcEnabled = true;
		/// int imagingJobId = _imagingJobService.Run(workspaceId, imagingSetId, imagingSetJobRequest);
		/// </code>
		/// </example>
		int Run(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null);

		/// <summary>
		/// Schedules an imaging job.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging job.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <param name="imagingSetJobRequest">
		/// Request for imaging job on imaging set can set QcEnabled property and OriginationID.
		/// Can be skipped to use default false value for QcEnabled and null for OriginationID.
		/// </param>
		/// <returns>The Task with Artifact ID for the job.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imaginingSetId = 2;
		/// int imagingJobId = await _imagingJobService.RunAsync(workspaceId, imagingSetId)
		/// 	.ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<int> RunAsync(int workspaceId, int imagingSetId, ImagingSetJobRequest imagingSetJobRequest = null);

		/// <summary>
		/// Waits for the job to be in 'Completed' or 'Completed with Erorrs' status.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging job.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set connected with imaging job.</param>
		/// <param name="timeout">The maximum time in minutes to wait, default is 2.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imagingSetWithRunJobId = 2;
		/// _imagingJobService.WaitForTheJobToComplete(workspaceId, imagingSetWithRunJobId, 3);
		/// </code>
		/// </example>
		void WaitForTheJobToComplete(int workspaceId, int imagingSetId, double timeout = 2.0);

		/// <summary>
		/// Waits for the job to be in 'Completed' or 'Completed with Erorrs' status.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging job.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set connected with imaging job.</param>
		/// <param name="timeout">The maximum time in minutes to wait, default is 2.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation of waiting for the job to complete.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imagingSetWithRunJobId = 2;
		/// await _imagingJobService.WaitForTheJobToCompleteAsync(workspaceId, imagingSetWithRunJobId)
		/// 	.ConfigureAwait(false);
		/// </code>
		/// </example>
		Task WaitForTheJobToCompleteAsync(int workspaceId, int imagingSetId, double timeout = 2.0);
	}
}
