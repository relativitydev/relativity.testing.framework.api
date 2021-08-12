using System.Threading.Tasks;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IMatterGetEligibleClientsStrategy))]
	internal class MatterGetEligibleClientsStrategyFixture : ApiServiceTestFixture<IMatterGetEligibleClientsStrategy>
	{
		[Test]
		public void GetAll_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() =>
				Sut.GetAll());
		}

		[Test]
		public void GetAll_ReturnsNotEmptyClientsList()
		{
			ArtifactIdNamePair[] result = Sut.GetAll();

			TestIfResultListIsValid(result);
		}

		[Test]
		public void GetAllAsync_DoesNotThrowException()
		{
			Assert.DoesNotThrowAsync(() =>
				Sut.GetAllAsync());
		}

		[Test]
		public async Task GetAllAsync_ReturnsNotEmptyClientsList()
		{
			ArtifactIdNamePair[] result = await Sut.GetAllAsync().ConfigureAwait(false);

			TestIfResultListIsValid(result);
		}

		private static void TestIfResultListIsValid(ArtifactIdNamePair[] result)
		{
			Assert.NotNull(result);
			Assert.IsNotEmpty(result);
			Assert.IsFalse(result[0].ArtifactID < 1 || string.IsNullOrWhiteSpace(result[0].Name));
		}
	}
}
