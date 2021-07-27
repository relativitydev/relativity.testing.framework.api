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
		/// Submit a job to image an individual document.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging job.</param>
		/// <param name="documentArtifactId">The Artifact ID of the document that will be imaged.</param>
		/// <param name="singleDocumentImagingJobRequest">The <see cref="SingleDocumentImagingJobRequest"/> indicating which document to image.</param>
		/// <returns>The Artifact ID for the job.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int documentArtifactId = 2;
		/// var singleDocumentImagingJobRequest = new SingleDocumentImagingJobRequest
		/// {
		/// 	OriginationID = Guid.NewGuid(),
		/// 	ProfileID = imagingProfile.ArtifactID,
		/// 	AlternateNativeLocation = null,
		/// 	RemoveAlternateNativeAfterImaging = false
		/// };
		/// long imagingJobId = _imagingJobService.SubmitSingleDocument(workspaceId, documentArtifactId, singleDocumentImagingJobRequest);
		/// </code>
		/// </example>
		long SubmitSingleDocument(int workspaceId, int documentArtifactId, SingleDocumentImagingJobRequest singleDocumentImagingJobRequest);

		/// <summary>
		/// Submit a job to image an individual document.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging job.</param>
		/// <param name="documentArtifactId">The Artifact ID of the document that will be imaged.</param>
		/// <param name="singleDocumentImagingJobRequest">The <see cref="SingleDocumentImagingJobRequest"/> indicating which document to image.</param>
		/// <returns>The Task with Artifact ID for the job.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int documentArtifactId = 2;
		/// var singleDocumentImagingJobRequest = new SingleDocumentImagingJobRequest
		/// {
		/// 	OriginationID = Guid.NewGuid(),
		/// 	ProfileID = imagingProfile.ArtifactID,
		/// 	AlternateNativeLocation = null,
		/// 	RemoveAlternateNativeAfterImaging = false
		/// };
		/// long imagingJobId = await _imagingJobService.SubmitSingleDocumentAsync(workspaceId, documentArtifactId, singleDocumentImagingJobRequest).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<long> SubmitSingleDocumentAsync(int workspaceId, int documentArtifactId, SingleDocumentImagingJobRequest singleDocumentImagingJobRequest);

		/// <summary>
		/// Submit a mass image job.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging job.</param>
		/// <param name="imagingMassJobRequest">The <see cref="ImagingMassJobRequest"/> indicating which documents to image.</param>
		/// <returns>The Artifact ID for the job.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int massProcessId = 5;
		/// var imagingMassJobRequest = new ImagingMassJobRequest
		/// {
		/// 	ProfileID = imagingProfile.ArtifactID,
		/// 	MassProcessID = massProcessId.ToString(),
		/// 	SourceType = ImagingSourceType.Native
		/// };
		/// long imagingJobId = _imagingJobService.SubmitMassDocument(workspaceId, imagingMassJobRequest);
		/// </code>
		/// </example>
		long SubmitMassDocument(int workspaceId, ImagingMassJobRequest imagingMassJobRequest);

		/// <summary>
		/// Submit a mass image job.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging job.</param>
		/// <param name="imagingMassJobRequest">The <see cref="ImagingMassJobRequest"/> indicating which documents to image.</param>
		/// <returns>The Task with Artifact ID for the job.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int massProcessId = 5;
		/// var imagingMassJobRequest = new ImagingMassJobRequest
		/// {
		/// 	ProfileID = imagingProfile.ArtifactID,
		/// 	MassProcessID = massProcessId.ToString(),
		/// 	SourceType = ImagingSourceType.Native
		/// };
		/// long imagingJobId = await _imagingJobService.SubmitMassDocumentAsync(workspaceId, imagingMassJobRequest).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<long> SubmitMassDocumentAsync(int workspaceId, ImagingMassJobRequest imagingMassJobRequest);

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
