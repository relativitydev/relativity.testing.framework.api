using System;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingJobRetryErrorsStrategy))]
	internal class ImagingJobRetryErrorsStrategyFixture : ImagingStrategyAbstractFixture<IImagingJobRetryErrorsStrategy>
	{
		[Test]
		public void RetryErrors_WithOptionalRequest_ReturnsImagingJobId()
		{
			int imagingSetId = CreateImagingSet().ArtifactID;
			var retryErrorsRequest = ArrangeRetryErrorsRequest();

			var imagingJobId = Sut.RetryErrors(DefaultWorkspace.ArtifactID, imagingSetId, retryErrorsRequest);
			Assert.IsTrue(imagingJobId > 0);
		}

		[Test]
		public void RetryErrors_WithoutOptionalRequest_ReturnsImagingJobId()
		{
			int imagingSetId = CreateImagingSet().ArtifactID;

			var imagingJobId = Sut.RetryErrors(DefaultWorkspace.ArtifactID, imagingSetId);
			Assert.IsTrue(imagingJobId > 0);
		}

		private ImagingSetJobRequest ArrangeRetryErrorsRequest()
		{
			return new ImagingSetJobRequest
			{
				OriginationID = Guid.NewGuid(),
				QcEnabled = false
			};
		}
	}
}
