using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IImagingSetReleaseStrategy))]
	internal class ImagingSetReleaseStrategyFixture : ImagingStrategyAbstractFixture<IImagingSetReleaseStrategy>
	{
		private const string _RELEASE_ON_NOT_RUN_JOB_EXCEPTION = "Unable to update QC status, the imaging set must be Completed or Completed With Errors";

		[Test]
		[VersionRange(">=12.1")]
		public async Task ReleaseAsync_ValidIdsRunJob_DoesNotThrowException()
		{
			int imagingSetId = await CreateImagingSetAndRunJobAsync().ConfigureAwait(false);
			WaitUntilImagingSetStatusIsCompleted(imagingSetId);

			Assert.DoesNotThrowAsync(() => Sut.ReleaseAsync(DefaultWorkspace.ArtifactID, imagingSetId));
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Release_ValidIdsRunJob_DoesNotThrowException()
		{
			int imagingSetId = CreateImagingSetAndRunJob();
			WaitUntilImagingSetStatusIsCompleted(imagingSetId);

			Assert.DoesNotThrow(() => Sut.Release(DefaultWorkspace.ArtifactID, imagingSetId));
		}

		[Test]
		[VersionRange(">=12.1")]
		public async Task ReleaseAsync_ValidIdsNotRunJob_ThrowsException()
		{
			int imagingSetId = (await CreateImagingSetAsync().ConfigureAwait(false)).ArtifactID;

			HttpRequestException result = Assert.ThrowsAsync<HttpRequestException>(()
				=> Sut.ReleaseAsync(DefaultWorkspace.ArtifactID, imagingSetId));
			result.Message.Should().Contain(_RELEASE_ON_NOT_RUN_JOB_EXCEPTION);
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Release_ValidIdsNotRunJob_ThrowsException()
		{
			int imagingSetId = CreateImagingSet().ArtifactID;

			HttpRequestException result = Assert.Throws<HttpRequestException>(() =>
				Sut.Release(DefaultWorkspace.ArtifactID, imagingSetId));
			result.Message.Should().Contain(_RELEASE_ON_NOT_RUN_JOB_EXCEPTION);
		}
	}
}
