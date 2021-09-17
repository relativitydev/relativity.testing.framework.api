using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingProfileUpdateStrategy))]
	internal class ImagingProfileUpdateStrategyFixture : ApiServiceTestFixture<IImagingProfileUpdateStrategy>
	{
		private IImagingProfileCreateBasicStrategy _basicImagingProfileCreateStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_basicImagingProfileCreateStrategy = Facade.Resolve<IImagingProfileCreateBasicStrategy>();
		}

		[Test]
		public void Update_BasicImagingProfile_ShouldBeSuccessful()
		{
			var dto = PrepareData();

			var imagingProfile = _basicImagingProfileCreateStrategy.Create(DefaultWorkspace.ArtifactID, dto);
			imagingProfile.Name = Randomizer.GetString();

			var updatedImagingProfile = new ImagingProfile();
			Assert.DoesNotThrow(() => updatedImagingProfile = Sut.Update(DefaultWorkspace.ArtifactID, imagingProfile));

			imagingProfile.Name.Should().Be(updatedImagingProfile.Name);
		}

		private CreateBasicImagingProfileDTO PrepareData()
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
