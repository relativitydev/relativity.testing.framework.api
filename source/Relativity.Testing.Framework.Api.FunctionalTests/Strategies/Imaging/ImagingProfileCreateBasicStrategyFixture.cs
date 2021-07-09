using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingProfileCreateBasicStrategy))]
	internal class ImagingProfileCreateBasicStrategyFixture : ApiServiceTestFixture<IImagingProfileCreateBasicStrategy>
	{
		[Test]
		public void Create_WithBasicImagingProfile_ShouldBeSuccessful()
		{
			var dto = GetTestData();

			var result = Sut.Create(DefaultWorkspace.ArtifactID, dto);

			result.Should().NotBeNull();
			result.ArtifactID.Should().BePositive();
		}

		[Test]
		public async Task CreateAsync_WithBasicImagingProfile_ShouldBeSuccessful()
		{
			var dto = GetTestData();

			var result = await Sut.CreateAsync(DefaultWorkspace.ArtifactID, dto).ConfigureAwait(false);

			result.Should().NotBeNull();
			result.ArtifactID.Should().BePositive();
		}

		private CreateBasicImagingProfileDTO GetTestData()
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
