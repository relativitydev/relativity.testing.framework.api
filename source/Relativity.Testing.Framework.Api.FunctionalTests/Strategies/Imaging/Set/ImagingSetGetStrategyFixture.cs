using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IImagingSetGetStrategy))]
	internal class ImagingSetGetStrategyFixture : ImagingSetStrategyAbstractFixture<IImagingSetGetStrategy>
	{
		private const string _NOT_SUPPORTED_EXCEPTION_MESSAGE = "The method Get Imaging Set does not support version of Relativity lower than 12.1.";
		private const int _DEFAULT_IMAGING_SET_ID = 10001;

		[Test]
		[VersionRange(">=12.1")]
		public async Task GetAsync_ValidIds_ReturnsExpectedImagingSet()
		{
			var expectedImagingSet = CreateImagingSet();

			var imagingSet = await Sut.GetAsync(DefaultWorkspace.ArtifactID, expectedImagingSet.ArtifactID).ConfigureAwait(false);

			imagingSet.Should().BeEquivalentTo(expectedImagingSet);
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Get_ValidIds__ReturnsExpectedImagingSet()
		{
			var expectedImagingSet = CreateImagingSet();

			var imagingSet = Sut.Get(DefaultWorkspace.ArtifactID, expectedImagingSet.ArtifactID);

			imagingSet.Should().BeEquivalentTo(expectedImagingSet);
		}

		[Test]
		[VersionRange("<12.1")]
		public void Get_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
				Sut.Get(DefaultWorkspace.ArtifactID, _DEFAULT_IMAGING_SET_ID));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}

		[Test]
		[VersionRange("<12.1")]
		public void GetAsync_ForNotSupportedVersion_ThrowsArgumentException()
		{
			var result = Assert.ThrowsAsync<ArgumentException>(async () =>
				await Sut.GetAsync(DefaultWorkspace.ArtifactID, _DEFAULT_IMAGING_SET_ID).ConfigureAwait(false));

			result.Message.Should().Contain(_NOT_SUPPORTED_EXCEPTION_MESSAGE);
		}
	}
}
