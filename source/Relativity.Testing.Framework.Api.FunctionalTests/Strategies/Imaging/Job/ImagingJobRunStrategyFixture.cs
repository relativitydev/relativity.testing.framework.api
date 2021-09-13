using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingJobRunStrategy))]
	internal class ImagingJobRunStrategyFixture : ImagingStrategyAbstractFixture<IImagingJobRunStrategy>
	{
		[Test]
		public void Run_ValidIds_ReturnsImagingJobId()
		{
			ImagingSet imagingSet = CreateImagingSet();

			long imagingJobId = Sut.Run(DefaultWorkspace.ArtifactID, imagingSet.ArtifactID);

			Assert.That(imagingJobId > 0);
		}

		[Test]
		public void Run_ValidIds_ChangesImagingSetStatus()
		{
			ImagingSet imagingSet = CreateImagingSet();

			Sut.Run(DefaultWorkspace.ArtifactID, imagingSet.ArtifactID);

			ImagingSetDetailedStatus imagingSetStatusAfterRun = Facade.Resolve<IImagingSetStatusGetStrategy>()
				.Get(DefaultWorkspace.ArtifactID, imagingSet.ArtifactID);

			Assert.That(!imagingSet.Status.Status.Equals(imagingSetStatusAfterRun.Status));
		}
	}
}
