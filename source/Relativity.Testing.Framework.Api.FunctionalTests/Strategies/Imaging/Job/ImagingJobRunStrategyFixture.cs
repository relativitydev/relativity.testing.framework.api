using System.Threading.Tasks;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IImagingJobRunStrategy))]
	internal class ImagingJobRunStrategyFixture : ImagingStrategyAbstractFixture<IImagingJobRunStrategy>
	{
		[Test]
		[VersionRange(">=12.1")]
		public async Task RunAsync_ValidIds_ReturnsImagingJobId()
		{
			ImagingSet imagingSet = CreateImagingSet();

			int imagingJobId = await Sut.RunAsync(DefaultWorkspace.ArtifactID, imagingSet.ArtifactID)
				.ConfigureAwait(false);

			Assert.That(imagingJobId > 0);
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Run_ValidIds_ReturnsImagingJobId()
		{
			ImagingSet imagingSet = CreateImagingSet();

			int imagingJobId = Sut.Run(DefaultWorkspace.ArtifactID, imagingSet.ArtifactID);

			Assert.That(imagingJobId > 0);
		}

		[Test]
		[VersionRange(">=12.1")]
		public async Task RunAsync_ValidIds_ChangesImagingSetStatus()
		{
			ImagingSet imagingSet = CreateImagingSet();

			await Sut.RunAsync(DefaultWorkspace.ArtifactID, imagingSet.ArtifactID).ConfigureAwait(false);

			ImagingSetDetailedStatus imagingSetStatusAfterRun = await Facade.Resolve<IImagingSetStatusGetStrategy>()
				.GetAsync(DefaultWorkspace.ArtifactID, imagingSet.ArtifactID).ConfigureAwait(false);

			Assert.That(!imagingSet.Status.Status.Equals(imagingSetStatusAfterRun.Status));
		}

		[Test]
		[VersionRange(">=12.1")]
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
