using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IImagingSetReleaseStrategy))]
	internal class ImagingSetReleaseStrategyFixture : ImagingStrategyAbstractFixture<IImagingSetReleaseStrategy>
	{
		private const string _RELEASE_ON_NOT_RUN_JOB_EXCEPTION = "Unable to update QC status, the imaging set must be Completed or Completed With Errors";

		[Test]
		public void Release_ValidIdsRunJob_DoesNotThrowException()
		{
			int imagingSetId = CreateImagingSetAndRunJob();
			WaitUntilImagingSetStatusIsCompleted(imagingSetId);

			Assert.DoesNotThrow(() => Sut.Release(DefaultWorkspace.ArtifactID, imagingSetId));
		}

		[Test]
		public void Release_ValidIdsNotRunJob_ThrowsException()
		{
			int imagingSetId = CreateImagingSet().ArtifactID;

			HttpRequestException result = Assert.Throws<HttpRequestException>(() =>
				Sut.Release(DefaultWorkspace.ArtifactID, imagingSetId));
			result.Message.Should().Contain(_RELEASE_ON_NOT_RUN_JOB_EXCEPTION);
		}
	}
}
