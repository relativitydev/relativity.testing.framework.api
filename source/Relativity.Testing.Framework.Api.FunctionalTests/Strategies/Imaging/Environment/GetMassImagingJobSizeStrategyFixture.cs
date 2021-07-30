using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IGetMassImagingJobSizeStrategy))]
	internal class GetMassImagingJobSizeStrategyFixture : ApiServiceTestFixture<IGetMassImagingJobSizeStrategy>
	{
		[Test]
		public void Get_ShouldNotThrow()
		{
			int size = 0;
			Assert.DoesNotThrow(() => size = Sut.Get());
			Assert.IsTrue(size > 0);
		}

		[Test]
		public void GetAsync_ShouldNotThrow()
		{
			int size = 0;
			Assert.DoesNotThrowAsync(async () => size = await Sut.GetAsync().ConfigureAwait(false));
			Assert.IsTrue(size > 0);
		}
	}
}
