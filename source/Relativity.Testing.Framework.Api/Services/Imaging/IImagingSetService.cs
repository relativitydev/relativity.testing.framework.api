using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the Imaging Set API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _imagingSetService = relativityFacade.Resolve&lt;IImagingSetService&gt;();
	/// </code>
	/// </example>
	public interface IImagingSetService
	{
		/// <summary>
		/// Creates imaging set based on provided <see cref="ImagingSetRequest"/> properties.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetRequest">The Imaging Set Request that included basing information about the Imaging Set that should be created.</param>
		/// <returns>The created <see cref="ImagingSet"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// KeywordSearch keywordSearch = Facade.Resolve&lt;ICreateWorkspaceEntityStrategy&lt;KeywordSearch&gt;&gt;()
		/// 	.Create(workspaceId, new KeywordSearch());
		/// var imagingProfileDto = new CreateBasicImagingProfileDTO
		/// {
		/// 	Name = Randomizer.GetString(),
		/// 	Notes = string.Empty,
		/// 	Keywords = string.Empty,
		/// 	BasicOptions = new BasicImagingEngineOptions
		/// 	{
		/// 		ImageOutputDpi = 300,
		/// 		BasicImageFormat = ImageFormatType.Jpeg,
		/// 		ImageSize = ImageSizeType.Custom,
		/// 		MaximumImageHeight = 6.0m,
		/// 		MaximumImageWidth = 6.0m
		/// 	}
		/// };
		/// ImagingProfile imagingProfile = Facade.Resolve&lt;IImagingProfileCreateBasicStrategy&gt;()
		/// 	.Create(workspaceId, imagingProfileDto);
		///
		/// var imagingSetCreateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = keywordSearch.ArtifactID,
		/// 	ImagingProfileID = imagingProfile.ArtifactID,
		/// 	Name = "Test Imaging Set"
		/// };
		/// ImagingSet createdImagingSet = _imagingSetService.Create(workspaceId, imagingSetCreateRequest);
		/// </code>
		/// </example>
		ImagingSet Create(int workspaceId, ImagingSetRequest imagingSetRequest);

		/// <summary>
		/// Gets the imaging set by specified <paramref name="imagingSetId"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <returns>The <see cref="ImagingSet"/> with specified <paramref name="imagingSetId"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imagingSetId = 2;
		/// ImagingSet imagingSet = _imagingSetService.Get(workspaceId, imagingSetId);
		/// </code>
		/// </example>
		ImagingSet Get(int workspaceId, int imagingSetId);

		/// <summary>
		/// Updates Imaging Set by given <paramref name="imagingSetRequest"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <param name="imagingSetRequest">The Imaging Set Request that included basing information about the Imaging Set that should be updated.</param>
		/// <returns>The Artifact ID of updated Imaging Set.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int existingImagingSetId = 1213;
		/// int imagingSetId = 2;
		/// int existingDatSourceId = 3;
		/// int existingImagingProfileID = 4;
		/// var imagingSetUpdateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = existingDatSourceId,
		/// 	ImagingProfileID = existingImagingProfileID,
		/// 	Name = "Updated Name"
		/// };
		/// int updatedImagingSetId = _imagingSetService.Update(workspaceId, existingImagingSetId, imagingSetUpdateRequest);
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int existingImagingSetId = 1213;
		/// ImagingSet existingImagingSet = _imagingSetService.Get(workspaceId, existingImagingSetId);
		/// var imagingSetUpdateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = existingImagingSet.DataSourceId,
		/// 	ImagingProfileID = existingImagingSet.ImagingProfile.ArtifactID,
		/// 	Name = "Updated Name"
		/// };
		/// int updatedImagingSetId = _imagingSetService.Update(workspaceId, existingImagingSetId, imagingSetUpdateRequest);
		/// </code>
		/// </example>
		int Update(int workspaceId, int imagingSetId, ImagingSetRequest imagingSetRequest);

		/// <summary>
		/// Gets the status of an imaging set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <returns>The <see cref="ImagingSetDetailedStatus"/> repesenting the status of imaging set.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imagingSetId = 2;
		/// ImagingSetDetailedStatus imagingSetStatus = _imagingSetService.GetStatus(workspaceId, imagingSetId);
		/// </code>
		/// </example>
		ImagingSetDetailedStatus GetStatus(int workspaceId, int imagingSetId);

		/// <summary>
		/// Deletes Imaging Set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imagingSetId = 2;
		/// _imagingSetService.Delete(workspaceId, imagingSetId);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int imagingSetId);

		/// <summary>
		/// Hides Imaging Set.
		/// Can be used to prevent users from viewing images that need to undergo a quality control review.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imagingSetId = 2;
		/// _imagingSetService.Hide(workspaceId, imagingSetId);
		/// </code>
		/// </example>
		void Hide(int workspaceId, int imagingSetId);

		/// <summary>
		/// Releases hidden images.
		/// Used to make images available to reviewers after a quality control review has been completed on hidden images.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int hiddenImagingSetId = 2;
		/// _imagingSetService.Release(workspaceId, imagingSetId);
		/// </code>
		/// </example>
		void Release(int workspaceId, int imagingSetId);
	}
}
