using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IImagingSetHideStrategy))]
	internal class ImagingSetHideStrategyFixture : ImagingStrategyAbstractFixture<IImagingSetHideStrategy>
	{
		private const string _JOB_NOT_COMPLETED_EXCEPTION_MESSAGE = "Unable to update QC status, the imaging set must be Completed or Completed With Errors";

		[Test]
		[VersionRange(">=12.1")]
		public void Hide_ValidIdsAndCompletedJob_DoesNotThrowException()
		{
			int imagingSetId = CreateImagingSetAndRunJob();
			WaitUntilImagingSetStatusIsCompleted(imagingSetId);
			Assert.DoesNotThrow(() => Sut.Hide(DefaultWorkspace.ArtifactID, imagingSetId));
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Hide_ValidIdsAndNotCompletedJob_ThrowsException()
		{
			int imagingSetId = CreateImagingSet().ArtifactID;

			HttpRequestException exception = Assert.Throws<HttpRequestException>(() => Sut.Hide(DefaultWorkspace.ArtifactID, imagingSetId));

			exception.Message.Should().Contain(_JOB_NOT_COMPLETED_EXCEPTION_MESSAGE);
		}
	}
}
