using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IImagingSetDeleteStrategy))]
	internal class ImagingSetDeleteStrategyFixture : ImagingStrategyAbstractFixture<IImagingSetDeleteStrategy>
	{
		[Test]
		[VersionRange(">=12.1")]
		public void DeleteAsync_ValidIds_DoesNotThrowException()
		{
			var imagingSetToDelete = CreateImagingSet();

			Assert.DoesNotThrowAsync(() => Sut.DeleteAsync(DefaultWorkspace.ArtifactID, imagingSetToDelete.ArtifactID));
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Delete_ValidIds_DoesNotThrowException()
		{
			var imagingSetToDelete = CreateImagingSet();

			Assert.DoesNotThrow(() => Sut.Delete(DefaultWorkspace.ArtifactID, imagingSetToDelete.ArtifactID));
		}
	}
}
