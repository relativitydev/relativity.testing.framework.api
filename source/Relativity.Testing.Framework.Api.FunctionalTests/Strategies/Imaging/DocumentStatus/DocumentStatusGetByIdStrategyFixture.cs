using System;
using System.Linq;
using System.Net.Http;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingDocumentStatusGetByIdStrategy))]
	internal class DocumentStatusGetByIdStrategyFixture : ApiServiceTestFixture<IImagingDocumentStatusGetByIdStrategy>
	{
		[Test]
		public void Get_Missing()
		{
			Assert.Throws<HttpRequestException>(() => Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue));
		}

		[Test]
		public void Get_Existing_DoesNotThrowException()
		{
			int documentId = ArrangeDocumentId();

			Assert.DoesNotThrow(() => Sut.Get(DefaultWorkspace.ArtifactID, documentId));
		}

		private int ArrangeDocumentId()
		{
			var document = ArrangeDocument();
			var imagingProfile = ArrangeImagingProfile();
			RunImagingjob(imagingProfile, document.ArtifactID);

			return document.ArtifactID;
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

		private void RunImagingjob(ImagingProfile imagingProfile, int documentId)
		{
			var imagingJobSubmitSingleDocumentRequest = new SingleDocumentImagingJobRequest
			{
				OriginationID = Guid.NewGuid(),
				ProfileID = imagingProfile.ArtifactID,
				AlternateNativeLocation = null,
				RemoveAlternateNativeAfterImaging = false
			};

			Facade.Resolve<IImagingJobSubmitSingleDocumentStrategy>().SubmitSingleDocument(DefaultWorkspace.ArtifactID, documentId, imagingJobSubmitSingleDocumentRequest);
		}
	}
}
