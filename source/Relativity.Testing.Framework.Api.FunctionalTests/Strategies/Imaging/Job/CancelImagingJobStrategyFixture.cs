using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(ICancelImagingJobStrategy))]
	internal class CancelImagingJobStrategyFixture : ImagingStrategyAbstractFixture<ICancelImagingJobStrategy>
	{
		[Test]
		public void Cancel_ShoudNotThrow()
		{
			int imagingSetId = CreateImagingSet().ArtifactID;
			long imagingJobId = Facade.Resolve<IImagingJobRunStrategy>().Run(DefaultWorkspace.ArtifactID, imagingSetId);
			var stopImagingJobRequest = ArrangeImagingJobRequest();

			Assert.DoesNotThrow(() => Sut.Cancel(DefaultWorkspace.ArtifactID, imagingJobId, stopImagingJobRequest));
		}

		[Test]
		public async Task CancelAsync_ShoudNotThrow()
		{
			int imagingSetId = CreateImagingSet().ArtifactID;
			long imagingJobId = await Facade.Resolve<IImagingJobRunStrategy>().RunAsync(DefaultWorkspace.ArtifactID, imagingSetId).ConfigureAwait(false);
			var stopImagingJobRequest = ArrangeImagingJobRequest();

			Assert.DoesNotThrowAsync(() => Sut.CancelAsync(DefaultWorkspace.ArtifactID, imagingJobId, stopImagingJobRequest));
		}

		private ImagingJobRequest ArrangeImagingJobRequest()
		{
			return new ImagingJobRequest
			{
				OriginationID = Guid.NewGuid()
			};
		}
	}
}
