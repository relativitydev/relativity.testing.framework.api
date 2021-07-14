using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IImagingSetStatusGetStrategy))]
	internal class ImagingSetStatusGetStrategyFixture : ImagingSetStrategyAbstractFixture<IImagingSetStatusGetStrategy>
	{
		[Test]
		[VersionRange(">=12.1")]
		public void Get_ValidWorkspaceId_ReturnsNotNull()
		{
			var imagingSet = CreateImagingSet();

			var result = Sut.Get(DefaultWorkspace.ArtifactID, imagingSet.ArtifactID);

			result.Should().NotBeNull();
		}

		[Test]
		[VersionRange(">=12.1")]
		public async Task GetAsync_ValidWorkspaceId_ReturnsNotNull()
		{
			var imagingSet = CreateImagingSet();

			var result = await Sut.GetAsync(DefaultWorkspace.ArtifactID, imagingSet.ArtifactID).ConfigureAwait(false);

			result.Should().NotBeNull();
		}
	}
}
