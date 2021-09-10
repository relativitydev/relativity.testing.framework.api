using System;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingJobUpdatePriorityStrategy))]
	internal class ImagingJobUpdatePriorityStrategyFixture : ImagingStrategyAbstractFixture<IImagingJobUpdatePriorityStrategy>
	{
		[Test]
		public void UpdatePriority_ShouldNotThrow()
		{
			int imagingSetId = CreateImagingSet().ArtifactID;
			long imagingJobId = Facade.Resolve<IImagingJobRunStrategy>().Run(DefaultWorkspace.ArtifactID, imagingSetId);
			var imagingJobPriorityRequest = ArrangeImagingJobPriorityRequest();

			Assert.DoesNotThrow(() => Sut.UpdatePriority(DefaultWorkspace.ArtifactID, imagingJobId, imagingJobPriorityRequest));
		}

		private ImagingJobPriorityRequest ArrangeImagingJobPriorityRequest()
		{
			return new ImagingJobPriorityRequest
			{
				OriginationID = Guid.NewGuid(),
				Priority = 99
			};
		}
	}
}
