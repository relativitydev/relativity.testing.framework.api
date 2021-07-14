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
		/// var workspaceId = 1015427;
		/// var keywordSearch = Facade.Resolve&lt;ICreateWorkspaceEntityStrategy&lt;KeywordSearch&gt;&gt;()
		/// 	.Create(workspaceId, new KeywordSearch());
		/// var imagingProfile = var imagingProfileDto = new CreateBasicImagingProfileDTO
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
		/// var imagingProfile = Facade.Resolve&lt;IImagingProfileCreateBasicStrategy&gt;()
		/// 	.Create(workspaceId, imagingProfileDto);
		///
		/// var imagingSetCreateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = keywordSearch.ArtifactID,
		/// 	ImagingProfileID = imagingProfile.ArtifactID,
		/// 	Name = "Test Imaging Set"
		/// };
		/// var createdImagingSet = _imagingSetService.Create(workspaceId, imagingSetCreateRequest);
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
		/// var workspaceId = 1015427;
		/// var keywordSearch = Facade.Resolve&lt;ICreateWorkspaceEntityStrategy&lt;KeywordSearch&gt;&gt;()
		/// 	.Create(workspaceId, new KeywordSearch());
		/// var imagingProfile = var imagingProfileDto = new CreateBasicImagingProfileDTO
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
		/// var imagingProfile = await Facade.Resolve&lt;IImagingProfileCreateBasicStrategy&gt;()
		/// 	.CreateAsync(workspaceId, imagingProfileDto).ConfigureAwait(false);
		///
		/// var imagingSetCreateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = keywordSearch.ArtifactID,
		/// 	ImagingProfileID = imagingProfile.ArtifactID,
		/// 	Name = "Test Imaging Set"
		/// };
		/// var createdImagingSet = await _imagingSetService.CreateAsync(workspaceId, imagingSetCreateRequest).ConfigureAwait(false);;
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
		/// var workspaceId = 1015427;
		/// var imagingSetId = 2;
		/// var imagingSet = _imagingSetService.Get(workspaceId, imagingSetId);
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
		/// var workspaceId = 1015427;
		/// var imagingSetId = 2;
		/// var imagingSet = await _imagingSetService.GetAsync(workspaceId, imagingSetId).ConfigureAwait(false);
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
		/// var workspaceId = 1015427;
		/// var existingImagingSetId = 1213;
		/// var imagingSetId = 2;
		/// var existingDatSourceId = 3;
		/// var existingImagingProfileID = 4;
		/// var imagingSetUpdateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = existingDatSourceId,
		/// 	ImagingProfileID = existingImagingProfileID,
		/// 	Name = "Updated Name"
		/// };
		/// var updatedImagingSetId = _imagingSetService.Update(workspaceId, existingImagingSetId, imagingSetUpdateRequest);
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var existingImagingSetId = 1213;
		/// var existingImagingSet = _imagingSetService.Get(workspaceId, existingImagingSetId);
		/// var imagingSetUpdateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = existingImagingSet.DataSourceId,
		/// 	ImagingProfileID = existingImagingSet.ImagingProfile.ArtifactID,
		/// 	Name = "Updated Name"
		/// };
		/// var updatedImagingSetId = _imagingSetService.Update(workspaceId, existingImagingSetId, imagingSetUpdateRequest);
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
		/// var workspaceId = 1015427;
		/// var existingImagingSetId = 1213;
		/// var imagingSetId = 2;
		/// var existingDatSourceId = 3;
		/// var existingImagingProfileID = 4;
		/// var imagingSetUpdateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = existingDatSourceId,
		/// 	ImagingProfileID = existingImagingProfileID,
		/// 	Name = "Updated Name"
		/// };
		/// var updatedImagingSetId = await _imagingSetService.UpdateAsync(workspaceId, existingImagingSetId, imagingSetUpdateRequest).ConfigureAwait(false);
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var existingImagingSetId = 1213;
		/// var existingImagingSet = await _imagingSetService.GetAsync(1015427, existingImagingSetId).ConfigureAwait(false);
		/// var imagingSetUpdateRequest = new ImagingSetRequest
		/// {
		/// 	DataSourceID = existingImagingSet.DataSourceId,
		/// 	ImagingProfileID = existingImagingSet.ImagingProfile.ArtifactID,
		/// 	Name = "Updated Name"
		/// };
		/// var updatedImagingSetId = await _imagingSetService.Update(workspaceId, existingImagingSetId, imagingSetUpdateRequest).ConfigureAwait(false);
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
		/// var workspaceId = 1015427;
		/// var imagingSetId = 2;
		/// var imagingSetStatus = _imagingSetService.GetStatus(workspaceId, imagingSetId);
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
		/// var workspaceId = 1015427;
		/// var imagingSetId = 2;
		/// var imagingSetStatus = await _imagingSetService.GetStatusAsync(workspaceId, imagingSetId).ConfigureAwait(false);
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
		/// var workspaceId = 1015427;
		/// var imagingSetId = 2;
		/// var imagingSetStatus = _imagingSetService.Delete(workspaceId, imagingSetId);
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
		/// var workspaceId = 1015427;
		/// var imagingSetId = 2;
		/// var imagingSetStatus = await _imagingSetService.DeleteAsync(workspaceId, imagingSetId).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task DeleteAsync(int workspaceId, int imagingSetId);
	}
}
