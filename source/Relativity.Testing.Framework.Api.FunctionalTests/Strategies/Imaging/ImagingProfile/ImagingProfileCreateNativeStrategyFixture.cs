using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingProfileCreateNativeStrategy))]
	internal class ImagingProfileCreateNativeStrategyFixture : ApiServiceTestFixture<IImagingProfileCreateNativeStrategy>
	{
		[Test]
		public void Create_WithNativeImagingProfile_ShouldBeSuccessful()
		{
			var dto = PrepareTestData();

			var result = Sut.Create(DefaultWorkspace.ArtifactID, dto);

			result.Should().NotBeNull();
			result.ArtifactID.Should().BePositive();
		}

		private CreateNativeImagingProfileDTO PrepareTestData()
		{
			return new CreateNativeImagingProfileDTO
			{
				Name = Randomizer.GetString(),
				Notes = string.Empty,
				Keywords = string.Empty,
				BasicOptions = new BasicImagingEngineOptions
				{
					ImageOutputDpi = 300,
					BasicImageFormat = ImageFormatType.Jpeg,
					ImageSize = ImageSizeType.Custom,
					MaximumImageHeight = 6.0m,
					MaximumImageWidth = 6.0m
				},
				NativeOptions = new NativeImagingEngineOptions
				{
					DitheringAlgorithm = NativeImagingDitheringAlgorithm.Threshold,
					DitheringThreshold = 128,
					ImageOutputDpi = 300,
					MaxPagesPerDoc = null,
					NativeImageFormat = ImageFormatType.Tiff,
					RenderColorPagesToJpeg = false,
					TimeZoneFieldOnDocument = null,
					LastModifiedDateOnDocument = null
				},
				HtmlOptions = new ImagingHtmlOptions
				{
					RemoveNonBreakingSpaceCodes = true
				},
				PresentationOptions = new ImagingPresentationOptions
				{
					ShowSpeakerNotes = true,
					SlideOrientation = ImagingElementOrientation.OriginalSetting
				},
				SpreadsheetOptions = new ImagingSpreadsheetOptions
				{
					FitToPagesTall = null,
					FitToPagesWide = null,
					Formatting = new HashSet<ImagingSpreadsheetFormatting>
					{
						ImagingSpreadsheetFormatting.AutoFitColumns,
						ImagingSpreadsheetFormatting.AutoFitRows,
						ImagingSpreadsheetFormatting.ClearFormattingInEmptyColumns,
						ImagingSpreadsheetFormatting.ClearFormattingInEmptyRows
					},
					HideAndPageBreakAfterConsecutiveBlankRowCol = 10,
					IncludeBorders = true,
					IncludeComments = true,
					IncludeGridlines = ImagingIncludeElement.OriginalSetting,
					IncludeHeadersAndFooters = ImagingIncludeElement.OriginalSetting,
					IncludeRowAndColumnHeadings = ImagingIncludeElement.OriginalSetting,
					LimitToPages = null,
					PageOrder = ImagingSpreadsheetPageOrder.OriginalSetting,
					PaperSizeOrientation = ImagingSpreadsheetPaperSizeOrientation.OriginalSetting,
					PrintArea = ImagingSpreadsheetPrintArea.OriginalSetting,
					ShowTrackChanges = true,
					TextVisibility = new HashSet<ImagingSpreadsheetTextVisibility>
					{
						ImagingSpreadsheetTextVisibility.RemoveBackgroundFillColors,
						ImagingSpreadsheetTextVisibility.SetTextColorToBlack
					},
					UnhideHiddenWorksheets = true,
					ZoomLevelPercentage = null,
				},
				WordProcessingOptions = new ImagingWordOptions
				{
					Include = new HashSet<ImagingWordInclude>
					{
						ImagingWordInclude.Comments,
						ImagingWordInclude.FieldCodes,
						ImagingWordInclude.HiddenText
					},
					PageOrientation = ImagingElementOrientation.OriginalSetting,
					ShowTrackChanges = true
				},
				EmailOptions = new ImagingEmailOptions
				{
					ClearIndentations = true,
					DetectCharacterEncoding = true,
					DisplaySmtpAddresses = true,
					DownloadImagesFromInternet = true,
					Orientation = ImagingEmailOrientation.Landscape,
					ResizeImagesToFitPage = true,
					ResizeTablesToFitPage = true,
					ShowMessageTypeInHeader = true,
					SplitTablesToFitPageWidth = true
				}
			};
		}
	}
}
