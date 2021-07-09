using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the Imaging API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _imagingService = relativityFacade.Resolve&lt;IImagingService&gt;();
	/// </code>
	/// </example>
	public interface IImagingService
	{
		/// <summary>
		/// Creates a new Imaging Profile instance, specifying only Basic Profile options. Native Profile options will be set to
		/// default values, as occurs when creating a Basic Profile in the UI.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dto">A <see cref="CreateBasicImagingProfileDTO"/> object corresponding to the desired Imaging Profile object.</param>
		/// <returns>Returns an <see cref="ImagingProfile"/> instance.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var dto = new CreateBasicImagingProfileDTO
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
		/// var imagingProfile = _imagingService.CreateBasic(workspaceId, dto);
		/// </code>
		/// </example>
		ImagingProfile CreateBasic(int workspaceId, CreateBasicImagingProfileDTO dto);

		/// <summary>
		/// Creates a new Imaging Profile instance, specifying only Basic Profile options. Native Profile options will be set to
		/// default values, as occurs when creating a Basic Profile in the UI.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dto">A <see cref="CreateBasicImagingProfileDTO"/> object corresponding to the desired Imaging Profile object.</param>
		/// <returns>A <see cref="Task"/> with an <see cref="ImagingProfile"/> instance.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var dto = new CreateBasicImagingProfileDTO
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
		/// var imagingProfile = await _imagingService.CreateBasicAsync(workspaceId, dto).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<ImagingProfile> CreateBasicAsync(int workspaceId, CreateBasicImagingProfileDTO dto);

		/// <summary>
		/// Creates a new Imaging Profile instance, specifying both Basic and Native Profile options.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dto">A <see cref="CreateNativeImagingProfileDTO"/> object corresponding to the desired Imaging Profile object.</param>
		/// <returns>Returns an <see cref="ImagingProfile"/> instance.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var dto = new CreateNativeImagingProfileDTO
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
		/// 	},
		/// 	NativeOptions = new NativeImagingEngineOptions
		/// 	{
		/// 		NativeImageFormat = ImageFormatType.Tiff,
		/// 	},
		/// 	HtmlOptions = new ImagingHtmlOptions
		/// 	{
		/// 		RemoveNonBreakingSpaceCodes = true
		/// 	},
		/// 	PresentationOptions = new ImagingPresentationOptions
		/// 	{
		/// 		ShowSpeakerNotes = true,
		/// 		SlideOrientation = ImagingElementOrientation.OriginalSetting
		/// 	},
		/// 	SpreadsheetOptions = new ImagingSpreadsheetOptions
		/// 	{
		/// 		HideAndPageBreakAfterConsecutiveBlankRowCol = 10,
		/// 		IncludeBorders = true,
		/// 		IncludeComments = true,
		/// 		IncludeGridlines = ImagingIncludeElement.OriginalSetting,
		/// 		IncludeHeadersAndFooters = ImagingIncludeElement.OriginalSetting,
		/// 		IncludeRowAndColumnHeadings = ImagingIncludeElement.OriginalSetting,
		/// 		PageOrder = ImagingSpreadsheetPageOrder.OriginalSetting,
		/// 		PaperSizeOrientation = ImagingSpreadsheetPaperSizeOrientation.OriginalSetting,
		/// 		PrintArea = ImagingSpreadsheetPrintArea.OriginalSetting,
		/// 		ShowTrackChanges = true,
		/// 		UnhideHiddenWorksheets = true
		/// 	},
		/// 	WordProcessingOptions = new ImagingWordOptions
		/// 	{
		/// 		PageOrientation = ImagingElementOrientation.OriginalSetting,
		/// 		ShowTrackChanges = true
		/// 	},
		/// 	EmailOptions = new ImagingEmailOptions
		/// 	{
		/// 		ClearIndentations = true,
		/// 		DetectCharacterEncoding = true,
		/// 		DisplaySmtpAddresses = true,
		/// 		DownloadImagesFromInternet = true,
		/// 		Orientation = ImagingEmailOrientation.Landscape,
		/// 		ResizeImagesToFitPage = true,
		/// 		ResizeTablesToFitPage = true,
		/// 		ShowMessageTypeInHeader = true,
		/// 		SplitTablesToFitPageWidth = true
		/// 	}
		/// };
		/// var imagingProfile = _imagingService.CreateNative(workspaceId, dto);
		/// </code>
		/// </example>
		ImagingProfile CreateNative(int workspaceId, CreateNativeImagingProfileDTO dto);

		/// <summary>
		/// Creates a new Imaging Profile instance, specifying both Basic and Native Profile options.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dto">A <see cref="CreateNativeImagingProfileDTO"/> object corresponding to the desired Imaging Profile object.</param>
		/// <returns>A <see cref="Task"/> with an <see cref="ImagingProfile"/> instance.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var dto = new CreateNativeImagingProfileDTO
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
		/// 	},
		/// 	NativeOptions = new NativeImagingEngineOptions
		/// 	{
		/// 		NativeImageFormat = ImageFormatType.Tiff,
		/// 	},
		/// 	HtmlOptions = new ImagingHtmlOptions
		/// 	{
		/// 		RemoveNonBreakingSpaceCodes = true
		/// 	},
		/// 	PresentationOptions = new ImagingPresentationOptions
		/// 	{
		/// 		ShowSpeakerNotes = true,
		/// 		SlideOrientation = ImagingElementOrientation.OriginalSetting
		/// 	},
		/// 	SpreadsheetOptions = new ImagingSpreadsheetOptions
		/// 	{
		/// 		HideAndPageBreakAfterConsecutiveBlankRowCol = 10,
		/// 		IncludeBorders = true,
		/// 		IncludeComments = true,
		/// 		IncludeGridlines = ImagingIncludeElement.OriginalSetting,
		/// 		IncludeHeadersAndFooters = ImagingIncludeElement.OriginalSetting,
		/// 		IncludeRowAndColumnHeadings = ImagingIncludeElement.OriginalSetting,
		/// 		PageOrder = ImagingSpreadsheetPageOrder.OriginalSetting,
		/// 		PaperSizeOrientation = ImagingSpreadsheetPaperSizeOrientation.OriginalSetting,
		/// 		PrintArea = ImagingSpreadsheetPrintArea.OriginalSetting,
		/// 		ShowTrackChanges = true,
		/// 		UnhideHiddenWorksheets = true
		/// 	},
		/// 	WordProcessingOptions = new ImagingWordOptions
		/// 	{
		/// 		PageOrientation = ImagingElementOrientation.OriginalSetting,
		/// 		ShowTrackChanges = true
		/// 	},
		/// 	EmailOptions = new ImagingEmailOptions
		/// 	{
		/// 		ClearIndentations = true,
		/// 		DetectCharacterEncoding = true,
		/// 		DisplaySmtpAddresses = true,
		/// 		DownloadImagesFromInternet = true,
		/// 		Orientation = ImagingEmailOrientation.Landscape,
		/// 		ResizeImagesToFitPage = true,
		/// 		ResizeTablesToFitPage = true,
		/// 		ShowMessageTypeInHeader = true,
		/// 		SplitTablesToFitPageWidth = true
		/// 	}
		/// };
		/// var imagingProfile = await _imagingService.CreateNativeAsync(workspaceId, dto).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<ImagingProfile> CreateNativeAsync(int workspaceId, CreateNativeImagingProfileDTO dto);

		/// <summary>
		/// Updates an existing Imaging Profile instance.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfile">An <see cref="ImagingProfile"/> object with updated fields, reflecting the desired final state of the Imaging Profile.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// someImagingProfile.Name = Randomizer.GetString();
		/// var updatedImagingProfile = _imagingService.Update(workspaceId, someImagingProfile);
		/// </code>
		/// </example>
		void Update(int workspaceId, ImagingProfile imagingProfile);

		/// <summary>
		/// Updates an existing Imaging Profile instance.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfile">An <see cref="ImagingProfile"/> object with updated fields, reflecting the desired final state of the Imaging Profile.</param>
		/// <returns>A <see cref="Task"/> with an <see cref="ImagingProfile"/> instance.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// someImagingProfile.Name = Randomizer.GetString();
		/// var updatedImagingProfile = await _imagingService.UpdateAsync(workspaceId, someImagingProfile).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task UpdateAsync(int workspaceId, ImagingProfile imagingProfile);

		/// <summary>
		/// Retrieves the <see cref="ImagingProfile"/> with the specified Artifact ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfileId">The Artifact ID of the <see cref="ImagingProfile"/> instance to read.</param>
		/// <returns>Returns an <see cref="ImagingProfile"/> instance.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var imagingProfileId = 1018877;
		/// var imagingProfile = _imagingService.Get(workspaceId, imagingProfileId);
		/// </code>
		/// </example>
		ImagingProfile Get(int workspaceId, int imagingProfileId);

		/// <summary>
		/// Retrieves the <see cref="ImagingProfile"/> with the specified Artifact ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfileId">The Artifact ID of the <see cref="ImagingProfile"/> instance to read.</param>
		/// <returns>A <see cref="Task"/> with an <see cref="ImagingProfile"/> instance.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var imagingProfileId = 1018877;
		/// var imagingProfile = await _imagingService.GetAsync(workspaceId, imagingProfileId).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<ImagingProfile> GetAsync(int workspaceId, int imagingProfileId);

		/// <summary>
		/// Deletes an Imaging Profile instance.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfileId">The Artifact ID of the <see cref="ImagingProfile"/> instance to delete.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var imagingProfileId = 1018877;
		/// _imagingService.Delete(workspaceId, imagingProfileId);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int imagingProfileId);

		/// <summary>
		/// Deletes an Imaging Profile instance.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfileId">The Artifact ID of the <see cref="ImagingProfile"/> instance to delete.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var imagingProfileId = 1018877;
		/// await _imagingService.DeleteAsync(workspaceId, imagingProfileId).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task DeleteAsync(int workspaceId, int imagingProfileId);
	}
}
