using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IRemoveInactiveImagingJobsStrategy))]
	internal class RemoveInactiveImagingJobsStrategyFixture : ApiServiceTestFixture<IRemoveInactiveImagingJobsStrategy>
	{
		[Test]
		public void Remove_ShouldNotThrow()
		{
			Assert.DoesNotThrow(() => Sut.Remove());
		}
	}
}
