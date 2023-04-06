using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	internal abstract class ImagingStrategyAbstractFixture<TStrategy> : ApiServiceTestFixture<TStrategy>
	{
		public static ImagingSet GetExpectedImageSetFromImagingSetRequest(ImagingSetRequest imagingSetCreateRequest)
		{
			return new ImagingSet
			{
				DataSourceId = imagingSetCreateRequest.DataSourceID,
				ImagingProfile = new ImagingProfile
				{
					ArtifactID = imagingSetCreateRequest.ImagingProfileID
				},
				Name = imagingSetCreateRequest.Name
			};
		}

		public static ImagingSetRequest PrepareImagingSetRequestFromImagingSet(ImagingSet imagingSet)
		{
			return new ImagingSetRequest
			{
				DataSourceID = imagingSet.DataSourceId,
				ImagingProfileID = imagingSet.ImagingProfile.ArtifactID,
				Name = imagingSet.Name
			};
		}

		public void WaitUntilImagingSetStatusIsCompleted(int imagingSetId)
		{
			Facade.Resolve<IWaitForImagingJobToCompleteStrategy>().Wait(DefaultWorkspace.ArtifactID, imagingSetId);
		}

		public int CreateImagingSetAndRunJob()
		{
			int imagingSetId = CreateImagingSet().ArtifactID;
			Facade.Resolve<IImagingJobRunStrategy>().Run(DefaultWorkspace.ArtifactID, imagingSetId);
			return imagingSetId;
		}

		public ImagingSet CreateImagingSet()
		{
			ImagingSetRequest imagingSetCreateRequest = ArrangeImagingSetRequestWithImagingProfile();

			ImagingSet imagingSet = Facade.Resolve<IImagingSetCreateStrategy>()
				.Create(DefaultWorkspace.ArtifactID, imagingSetCreateRequest);
			return imagingSet;
		}

		public ImagingSetRequest ArrangeImagingSetRequestWithImagingProfile()
		{
			KeywordSearch keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
				.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());
			ImagingProfile imagingProfile = ArrangeImagingProfile();

			ImagingSetRequest imagingSetCreateRequest = PrepareImagingSetRequest(keywordSearch, imagingProfile);
			return imagingSetCreateRequest;
		}

		public ImagingProfile ArrangeImagingProfile()
		{
			CreateBasicImagingProfileDTO imagingProfileDto = PrepareBasicImagingProfileDto();
			ImagingProfile imagingProfile = Facade.Resolve<IImagingProfileCreateBasicStrategy>()
				.Create(DefaultWorkspace.ArtifactID, imagingProfileDto);
			return imagingProfile;
		}

		private static ImagingSetRequest PrepareImagingSetRequest(KeywordSearch keywordSearch, ImagingProfile imagingProfile)
		{
			return new ImagingSetRequest
			{
				DataSourceID = keywordSearch.ArtifactID,
				ImagingProfileID = imagingProfile.ArtifactID,
				Name = Randomizer.GetString("Test Imaging Set {0}")
			};
		}

		private static CreateBasicImagingProfileDTO PrepareBasicImagingProfileDto()
		{
			return new CreateBasicImagingProfileDTO
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
		}
	}
}
