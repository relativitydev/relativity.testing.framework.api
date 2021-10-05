using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the ImagingProfile API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _imagingProfileService = relativityFacade.Resolve&lt;IImagingProfileService&gt;();
	/// </code>
	/// </example>
	public interface IImagingProfileService
	{
		/// <summary>
		/// Creates a new Imaging Profile instance, specifying only Basic Profile options. Native Profile options will be set to
		/// default values, as occurs when creating a Basic Profile in the UI.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dto">A <see cref="CreateBasicImagingProfileDTO"/> object corresponding to the desired Imaging Profile object.</param>
		/// <returns>Returns an [ImagingProfile](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ImagingProfile.html) instance.</returns>
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
		/// Creates a new Imaging Profile instance, specifying both Basic and Native Profile options.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dto">A <see cref="CreateNativeImagingProfileDTO"/> object corresponding to the desired Imaging Profile object.</param>
		/// <returns>Returns an [ImagingProfile](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ImagingProfile.html) instance.</returns>
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
		/// var imagingProfile = _imagingProfileService.CreateNative(workspaceId, dto);
		/// </code>
		/// </example>
		ImagingProfile CreateNative(int workspaceId, CreateNativeImagingProfileDTO dto);

		/// <summary>
		/// Updates an existing Imaging Profile instance.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfile">An [ImagingProfile](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ImagingProfile.html) object with updated fields, reflecting the desired final state of the Imaging Profile.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// someImagingProfile.Name = Randomizer.GetString();
		/// var updatedImagingProfile = _imagingProfileService.Update(workspaceId, someImagingProfile);
		/// </code>
		/// </example>
		void Update(int workspaceId, ImagingProfile imagingProfile);

		/// <summary>
		/// Retrieves the [ImagingProfile](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ImagingProfile.html) with the specified Artifact ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfileId">The Artifact ID of the [ImagingProfile](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ImagingProfile.html) instance to read.</param>
		/// <returns>Returns an [ImagingProfile](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ImagingProfile.html) instance.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var imagingProfileId = 1018877;
		/// var imagingProfile = _imagingProfileService.Get(workspaceId, imagingProfileId);
		/// </code>
		/// </example>
		ImagingProfile Get(int workspaceId, int imagingProfileId);

		/// <summary>
		/// Deletes an Imaging Profile instance.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfileId">The Artifact ID of the [ImagingProfile](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ImagingProfile.html) instance to delete.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var imagingProfileId = 1018877;
		/// _imagingProfileService.Delete(workspaceId, imagingProfileId);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int imagingProfileId);
	}
}
