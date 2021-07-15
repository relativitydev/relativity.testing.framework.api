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
		public void Get_ValidIds_ReturnsExpectedImagingSet()
		{
			var expectedImagingSet = CreateImagingSet();

			var imagingSet = Sut.Get(DefaultWorkspace.ArtifactID, expectedImagingSet.ArtifactID);

			imagingSet.Should().BeEquivalentTo(expectedImagingSet);
		}
	}
}
