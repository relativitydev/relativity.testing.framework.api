using System.Threading.Tasks;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IMatterGetEligibleStatusesStrategy))]
	internal class MatterGetEligibleStatusesStrategyFixture : ApiServiceTestFixture<IMatterGetEligibleStatusesStrategy>
	{
		[Test]
		public void GetAllAsync_DoesNotThrowException()
		{
			Assert.DoesNotThrowAsync(() =>
				Sut.GetAllAsync());
		}

		[Test]
		public async Task GetAllAsync_ReturnsNotNull()
		{
			ArtifactIdNamePair[] result = await Sut.GetAllAsync().ConfigureAwait(false);

			Assert.NotNull(result);
		}
	}
}
