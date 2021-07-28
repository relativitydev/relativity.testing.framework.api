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
		/// <param name="singleDocumentImagingJobRequest">The <see cref="SingleDocumentImagingJobRequest"/> which specifies imaging settings.</param>
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
		/// <param name="singleDocumentImagingJobRequest">The <see cref="SingleDocumentImagingJobRequest"/> which specifies imaging settings.</param>
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
		/// <param name="imagingMassJobRequest">The <see cref="ImagingMassJobRequest"/> which specifies imaging settings and indirectly (via <see cref="ImagingMassJobRequest.MassProcessID"/>) indicating which documents to image.</param>
		/// <returns>The Artifact ID for the job.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int documentArtifactTypeId = 10;
		/// List&lt;Document&gt; documents = Facade.Resolve&lt;IGetAllWorkspaceEntitiesStrategy&lt;Document&gt;&gt;().GetAll(workspaceId);
		///
		/// var joinedArtifactIds = string.Join(",", documents.Select(x => x.ArtifactID));
		/// var createMassProcessTablesRequest = new
		/// {
		/// 	request = new
		/// 	{
		/// 		artifactTypeId = documentArtifactTypeId,
		/// 		databaseTokenRequired = true,
		/// 		query = new
		/// 		{
		/// 			condition = $"(('Artifact ID' IN [{joinedArtifactIds}]))"
		/// 		}
		/// 	}
		/// };
		///
		/// var createMassProcessTablesResult = Facade.Resolve&lt;IRestService&gt;()
		/// 											.Post&lt;JObject&gt;($"MassOperation/v1/MassOperationManager/workspace/{workspaceId}/CreateMassProcessTables", createMassProcessTablesRequest);
		/// var massProcessId =  (int)createMassProcessTablesResult["ProcessID"];
		///
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
		/// <param name="imagingMassJobRequest">The <see cref="ImagingMassJobRequest"/> which specifies imaging settings and indirectly (via <see cref="ImagingMassJobRequest.MassProcessID"/>) indicating which documents to image.</param>
		/// <returns>The Task with Artifact ID for the job.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int documentArtifactTypeId = 10;
		/// List&lt;Document&gt; documents = Facade.Resolve&lt;IGetAllWorkspaceEntitiesStrategy&lt;Document&gt;&gt;().GetAll(workspaceId);
		///
		/// var joinedArtifactIds = string.Join(",", documents.Select(x => x.ArtifactID));
		/// var createMassProcessTablesRequest = new
		/// {
		/// 	request = new
		/// 	{
		/// 		artifactTypeId = documentArtifactTypeId,
		/// 		databaseTokenRequired = true,
		/// 		query = new
		/// 		{
		/// 			condition = $"(('Artifact ID' IN [{joinedArtifactIds}]))"
		/// 		}
		/// 	}
		/// };
		///
		/// var createMassProcessTablesResult = Facade.Resolve&lt;IRestService&gt;()
		/// 											.Post&lt;JObject&gt;($"MassOperation/v1/MassOperationManager/workspace/{workspaceId}/CreateMassProcessTables", createMassProcessTablesRequest);
		/// var massProcessId =  (int)createMassProcessTablesResult["ProcessID"];
		///
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
		/// Attempt to cancel imaging job.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging job.</param>
		/// <param name="imagingJobId">The Imaging Job ID.</param>
		/// <param name="cancelImagingJobRequest">The <see cref="ImagingJobRequest"/> which specifies optional parameters for cancellation.</param>
		/// <returns>A <see cref="ImagingJobActionResponse"/> containg the information about attempt to cancel imaging job.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// long imagingJobId = 10;
		/// var cancellationRequest = new ImagingJobRequest
		/// {
		/// 	OriginationID = Guid.NewGuid()
		/// };
		/// ImagingJobActionResponse cancellationResult = _imagingJobService.Cancel(workspaceId, imagingJobId, cancellationRequest);
		/// </code>
		/// </example>
		ImagingJobActionResponse Cancel(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest);

		/// <summary>
		/// Attempt to cancel imaging job.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging job.</param>
		/// <param name="imagingJobId">The Imaging Job ID.</param>
		/// <param name="cancelImagingJobRequest">The <see cref="ImagingJobRequest"/> which specifies optional parameters for cancellation.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation containg a <see cref="ImagingJobActionResponse"/> with the information about attempt to cancel imaging job.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// long imagingJobId = 10;
		/// var cancellationRequest = new ImagingJobRequest
		/// {
		/// 	OriginationID = Guid.NewGuid()
		/// };
		/// ImagingJobActionResponse cancellationResult = await _imagingJobService.CancelAsync(workspaceId, imagingJobId, cancellationRequest).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<ImagingJobActionResponse> CancelAsync(int workspaceId, long imagingJobId, ImagingJobRequest cancelImagingJobRequest);

		/// <summary>
		/// Waits for the job to be in 'Completed' or 'Completed with Errors' status.
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
		/// Waits for the job to be in 'Completed' or 'Completed with Errors' status.
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
