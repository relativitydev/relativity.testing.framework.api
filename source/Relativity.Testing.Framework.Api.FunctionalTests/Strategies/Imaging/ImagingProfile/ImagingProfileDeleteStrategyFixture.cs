using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingProfileDeleteStrategy))]
	internal class ImagingProfileDeleteStrategyFixture : ApiServiceTestFixture<IImagingProfileDeleteStrategy>
	{
		private IImagingProfileCreateBasicStrategy _basicImagingProfileCreateStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_basicImagingProfileCreateStrategy = Facade.Resolve<IImagingProfileCreateBasicStrategy>();
		}

		[Test]
		public void Delete_NotExitingImagingProfile_ShouldThrowNotFoundException()
		{
			var exception = Assert.Throws<HttpRequestException>(() => Sut.Delete(DefaultWorkspace.ArtifactID, int.MaxValue));

			exception.Message.Should().StartWith("StatusCode: 404, ReasonPhrase: 'Not Found'");
		}

		[Test]
		public void Delete_ExistingBasicImagingProfile_ShouldBeSuccessful()
		{
			var dto = PrepareTestData();

			var imagingProfile = _basicImagingProfileCreateStrategy.Create(DefaultWorkspace.ArtifactID, dto);

			Assert.DoesNotThrow(() => Sut.Delete(DefaultWorkspace.ArtifactID, imagingProfile.ArtifactID));
		}

		private CreateBasicImagingProfileDTO PrepareTestData()
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
