using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	internal abstract class ImagingStrategyAbstractFixture<TStrategy> : ApiServiceTestFixture<TStrategy>
	{
		private readonly TimeSpan _changeStatusTimeout = TimeSpan.FromMinutes(2);

		public void WaitUntilImagingSetStatusIsCompleted(int imagingSetId)
		{
			bool completed = false;
			var watch = Stopwatch.StartNew();

			while (!completed)
			{
				if (watch.Elapsed > _changeStatusTimeout)
				{
					throw new InvalidOperationException(
						$"Imaging Job for Imaging Set with id={imagingSetId} was not completed within the 2 minutes time limit." +
						"Please check the error log in Relativity, or confirm that the job took longer than expected.");
				}

				string status = Facade.Resolve<IImagingSetStatusGetStrategy>().Get(DefaultWorkspace.ArtifactID, imagingSetId).Status;
				completed = status.Equals("Completed") || status.Equals("Completed with Errors");
				Thread.Sleep(1000);
			}
		}

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

		public async Task<int> CreateImagingSetAndRunJobAsync()
		{
			int imagingSetId = (await CreateImagingSetAsync().ConfigureAwait(false)).ArtifactID;
			await Facade.Resolve<IImagingJobRunStrategy>()
				.RunAsync(DefaultWorkspace.ArtifactID, imagingSetId).ConfigureAwait(false);
			return imagingSetId;
		}

		public async Task<ImagingSet> CreateImagingSetAsync()
		{
			ImagingSetRequest imagingSetCreateRequest = await ArrangeImagingSetRequestWithImagingProfileAsync()
				.ConfigureAwait(false);

			ImagingSet imagingSet = await Facade.Resolve<IImagingSetCreateStrategy>()
				.CreateAsync(DefaultWorkspace.ArtifactID, imagingSetCreateRequest).ConfigureAwait(false);
			return imagingSet;
		}

		public async Task<ImagingSetRequest> ArrangeImagingSetRequestWithImagingProfileAsync()
		{
			KeywordSearch keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
				.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());
			ImagingProfile imagingProfile = await ArrangeImagingProfileAsync().ConfigureAwait(false);

			ImagingSetRequest imagingSetCreateRequest = PrepareImagingSetRequest(keywordSearch, imagingProfile);
			return imagingSetCreateRequest;
		}

		public async Task<ImagingProfile> ArrangeImagingProfileAsync()
		{
			CreateBasicImagingProfileDTO imagingProfileDto = PrepareBasicImagingProfileDto();
			ImagingProfile imagingProfile = await Facade.Resolve<IImagingProfileCreateBasicStrategy>()
				.CreateAsync(DefaultWorkspace.ArtifactID, imagingProfileDto).ConfigureAwait(false);
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
