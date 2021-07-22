using System.Threading.Tasks;
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
		/// Creates imaging set based on provided <see cref="ImagingSetRequest"/> properties.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetRequest">The Imaging Set Request that included basing information about the Imaging Set that should be created.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation with created <see cref="ImagingSet"/>.</returns>
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
		/// ImagingProfile imagingProfile = await Facade.Resolve&lt;IImagingProfileCreateBasicStrategy&gt;()
		/// 	.CreateAsync(workspaceId, imagingProfileDto).ConfigureAwait(false);
		///
		/// var imagingSetCreateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = keywordSearch.ArtifactID,
		/// 	ImagingProfileID = imagingProfile.ArtifactID,
		/// 	Name = "Test Imaging Set"
		/// };
		/// ImagingSet createdImagingSet = await _imagingSetService.CreateAsync(workspaceId, imagingSetCreateRequest).ConfigureAwait(false);;
		/// </code>
		/// </example>
		Task<ImagingSet> CreateAsync(int workspaceId, ImagingSetRequest imagingSetRequest);

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
		/// Gets the imaging set by specified <paramref name="imagingSetId"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation with <see cref="ImagingSet"/> with specified <paramref name="imagingSetId"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imagingSetId = 2;
		/// ImagingSet imagingSet = await _imagingSetService.GetAsync(workspaceId, imagingSetId).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<ImagingSet> GetAsync(int workspaceId, int imagingSetId);

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
		/// Updates Imaging Set by given <paramref name="imagingSetRequest"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <param name="imagingSetRequest">The Imaging Set Request that included basing information about the Imaging Set that should be updated.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation with the Artifact ID of updated Imaging Set.</returns>
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
		/// int updatedImagingSetId = await _imagingSetService.UpdateAsync(workspaceId, existingImagingSetId, imagingSetUpdateRequest).ConfigureAwait(false);
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int existingImagingSetId = 1213;
		/// ImagingSet existingImagingSet = await _imagingSetService.GetAsync(1015427, existingImagingSetId).ConfigureAwait(false);
		/// var imagingSetUpdateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = existingImagingSet.DataSourceId,
		/// 	ImagingProfileID = existingImagingSet.ImagingProfile.ArtifactID,
		/// 	Name = "Updated Name"
		/// };
		/// int updatedImagingSetId = await _imagingSetService.Update(workspaceId, existingImagingSetId, imagingSetUpdateRequest)
		/// 	.ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<int> UpdateAsync(int workspaceId, int imagingSetId, ImagingSetRequest imagingSetRequest);

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
		/// Gets the status of an imaging set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation with <see cref="ImagingSetDetailedStatus"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imagingSetId = 2;
		/// ImagingSetDetailedStatus imagingSetStatus = await _imagingSetService.GetStatusAsync(workspaceId, imagingSetId)
		/// 	.ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<ImagingSetDetailedStatus> GetStatusAsync(int workspaceId, int imagingSetId);

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
		/// Deletes Imaging Set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <returns>>A <see cref="Task"/> representing the asynchronous delete operation.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imagingSetId = 2;
		/// await _imagingSetService.DeleteAsync(workspaceId, imagingSetId).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task DeleteAsync(int workspaceId, int imagingSetId);

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
		/// Hides Imaging Set.
		/// Can be used to prevent users from viewing images that need to undergo a quality control review.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging set.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous hide operation.</returns>
		/// <example>
		/// <code>
		/// int workspaceId = 1015427;
		/// int imagingSetId = 2;
		/// _imagingSetService.Hide(workspaceId, imagingSetId);
		/// </code>
		/// </example>
		Task HideAsync(int workspaceId, int imagingSetId);

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
		/// await _imagingSetService.ReleaseAsync(workspaceId, imagingSetId).ConfigureAwait(false);
		/// </code>
		/// </example>
		/// <returns>>A <see cref="Task"/> representing the asynchronous release operation.</returns>
		Task ReleaseAsync(int workspaceId, int imagingSetId);
	}
}
