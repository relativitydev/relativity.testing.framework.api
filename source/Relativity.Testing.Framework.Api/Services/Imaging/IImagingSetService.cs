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
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging profile.</param>
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
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging profile.</param>
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
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging profile.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging profile.</param>
		/// <returns>The <see cref="ImagingSet"/> with specified <paramref name="imagingSetId"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var imagingSetId = 2;
		/// var imagingSet = _imagingSetService.Get(1015427, imagingSetId);
		/// </code>
		/// </example>
		ImagingSet Get(int workspaceId, int imagingSetId);

		/// <summary>
		/// Gets the imaging set by specified <paramref name="imagingSetId"/>.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging profile.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging profile.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation with <see cref="ImagingSet"/> with specified <paramref name="imagingSetId"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var imagingSetId = 2;
		/// var imagingSet = await _imagingSetService.GetAsync(1015427, imagingSetId).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<ImagingSet> GetAsync(int workspaceId, int imagingSetId);

		/// <summary>
		/// Gets the status of an imaging set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging profile.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <returns>The <see cref="ImagingSetDetailedStatus"/> repesenting the status of imaging set.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var imagingSetId = 2;
		/// var imagingSetStatus = _imagingSetService.GetStatus(1015427, imagingSetId);
		/// </code>
		/// </example>
		ImagingSetDetailedStatus GetStatus(int workspaceId, int imagingSetId);

		/// <summary>
		/// Gets the status of an imaging set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace that contains the imaging profile.</param>
		/// <param name="imagingSetId">The Artifact ID of a imaging set.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation with <see cref="ImagingSetDetailedStatus"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var imagingSetId = 2;
		/// var imagingSetStatus = await _imagingSetService.GetStatusAsync(1015427, imagingSetId).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<ImagingSetDetailedStatus> GetStatusAsync(int workspaceId, int imagingSetId);
	}
}
