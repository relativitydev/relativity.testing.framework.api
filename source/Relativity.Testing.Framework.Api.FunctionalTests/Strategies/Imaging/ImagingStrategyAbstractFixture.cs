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

		public static ImagingSetRequest CreateImagingSetRequestFromImagingSet(ImagingSet imagingSet)
		{
			return new ImagingSetRequest
			{
				DataSourceID = imagingSet.DataSourceId,
				ImagingProfileID = imagingSet.ImagingProfile.ArtifactID,
				Name = imagingSet.Name
			};
		}

		public ImagingSetRequest ArrangeImagingSetRequest()
		{
			KeywordSearch keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
							.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());
			ImagingProfile imagingProfile = ArrangeImagingProfile();

			var imagingSetCreateRequest = new ImagingSetRequest
			{
				DataSourceID = keywordSearch.ArtifactID,
				ImagingProfileID = imagingProfile.ArtifactID,
				Name = "Test Imaging Set"
			};
			return imagingSetCreateRequest;
		}

		public ImagingProfile ArrangeImagingProfile()
		{
			var imagingProfileDto = new CreateBasicImagingProfileDTO
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
			ImagingProfile imagingProfile = Facade.Resolve<IImagingProfileCreateBasicStrategy>()
				.Create(DefaultWorkspace.ArtifactID, imagingProfileDto);
			return imagingProfile;
		}

		public ImagingSet CreateImagingSet()
		{
			ImagingSetRequest imagingSetCreateRequest = ArrangeImagingSetRequest();

			ImagingSet imagingSet = Facade.Resolve<IImagingSetCreateStrategy>()
				.Create(DefaultWorkspace.ArtifactID, imagingSetCreateRequest);
			return imagingSet;
		}
	}
}
