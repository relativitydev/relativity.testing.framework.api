using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingJobSubmitMassDocumentStrategy))]
	internal class ImagingJobSubmitMassDocumentStrategyFixture : ApiServiceTestFixture<IImagingJobSubmitMassDocumentStrategy>
	{
		private const int _DOCUMENT_ARTIFACT_TYPE_ID = 10;

		[Test]
		public void SubmitMassDocument_ReturnsImagingJobId()
		{
			var imagingMassJobRequest = ArrangeRequest();

			var imagingJobId = Sut.SubmitMassDocument(DefaultWorkspace.ArtifactID, imagingMassJobRequest);

			Assert.That(imagingJobId > 0);
		}

		[Test]
		public async Task SubmitMassDocumentAsync_ReturnsImagingJobId()
		{
			var imagingMassJobRequest = ArrangeRequest();

			var imagingJobId = await Sut.SubmitMassDocumentAsync(DefaultWorkspace.ArtifactID, imagingMassJobRequest).ConfigureAwait(false);

			Assert.That(imagingJobId > 0);
		}

		private ImagingMassJobRequest ArrangeRequest()
		{
			var documents = ArrangeDocuments();
			var imagingProfile = ArrangeImagingProfile();
			var massProcessId = ArrangeMassProcessId(documents);

			return new ImagingMassJobRequest
			{
				ProfileID = imagingProfile.ArtifactID,
				MassProcessID = massProcessId.ToString(),
				SourceType = ImagingSourceType.Native
			};
		}

		private IList<Document> ArrangeDocuments()
		{
			const string fileName = "single_image.jpg";
			Facade.Resolve<IDocumentSingleImageImportStrategy>().Import(DefaultWorkspace.ArtifactID, $@"{AppDomain.CurrentDomain.BaseDirectory}\files\{fileName}");

			return Facade.Resolve<IGetAllWorkspaceEntitiesStrategy<Document>>().GetAll(DefaultWorkspace.ArtifactID);
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

		private int ArrangeMassProcessId(IList<Document> documents)
		{
			var joinedArtifactIds = string.Join(",", documents.Select(x => x.ArtifactID));

			var dto = new
			{
				request = new
				{
					artifactTypeId = _DOCUMENT_ARTIFACT_TYPE_ID,
					databaseTokenRequired = true,
					query = new
					{
						condition = $"(('Artifact ID' IN [{joinedArtifactIds}]))"
					}
				}
			};

			var result = Facade.Resolve<IRestService>().Post<JObject>($"MassOperation/v1/MassOperationManager/workspace/{DefaultWorkspace.ArtifactID}/CreateMassProcessTables", dto);

			return (int)result["ProcessID"];
		}
	}
}
