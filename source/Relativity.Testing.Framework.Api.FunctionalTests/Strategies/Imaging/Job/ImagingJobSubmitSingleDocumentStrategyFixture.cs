using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingJobSubmitSingleDocumentStrategy))]
	internal class ImagingJobSubmitSingleDocumentStrategyFixture : ApiServiceTestFixture<IImagingJobSubmitSingleDocumentStrategy>
	{
		[Test]
		public void SubmitSingleDocument_ReturnsImagingJobId()
		{
			var document = ArrangeDocument();
			var imagingProfile = ArrangeImagingProfile();
			var imagingJobSubmitSingleDocumentRequest = ArrangeRequest(imagingProfile);

			var imagingJobId = Sut.SubmitSingleDocument(DefaultWorkspace.ArtifactID, document.ArtifactID, imagingJobSubmitSingleDocumentRequest);

			Assert.That(imagingJobId > 0);
		}

		[Test]
		public async Task SubmitSingleDocumentAsync_ReturnsImagingJobId()
		{
			var document = ArrangeDocument();
			var imagingProfile = ArrangeImagingProfile();
			var imagingJobSubmitSingleDocumentRequest = ArrangeRequest(imagingProfile);

			var imagingJobId = await Sut.SubmitSingleDocumentAsync(DefaultWorkspace.ArtifactID, document.ArtifactID, imagingJobSubmitSingleDocumentRequest).ConfigureAwait(false);

			Assert.That(imagingJobId > 0);
		}

		private Document ArrangeDocument()
		{
			const string fileName = "single_image.jpg";
			Facade.Resolve<IDocumentSingleImageImportStrategy>().Import(DefaultWorkspace.ArtifactID, $@"{AppDomain.CurrentDomain.BaseDirectory}\files\{fileName}");

			var documents = Facade.Resolve<IGetAllWorkspaceEntitiesStrategy<Document>>().GetAll(DefaultWorkspace.ArtifactID);
			return documents.First();
		}

		private ImagingProfile ArrangeImagingProfile()
		{
			var createImagingProfileDTO = new CreateBasicImagingProfileDTO
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
				}
			};

			return Facade.Resolve<IImagingProfileCreateBasicStrategy>().Create(DefaultWorkspace.ArtifactID, createImagingProfileDTO);
		}

		private static ImagingJobSubmitSingleDocumentRequest ArrangeRequest(ImagingProfile imagingProfile)
		{
			return new ImagingJobSubmitSingleDocumentRequest
			{
				OriginationID = Guid.NewGuid(),
				ProfileID = imagingProfile.ArtifactID,
				AlternateNativeLocation = null,
				RemoveAlternateNativeAfterImaging = false
			};
		}
	}
}
