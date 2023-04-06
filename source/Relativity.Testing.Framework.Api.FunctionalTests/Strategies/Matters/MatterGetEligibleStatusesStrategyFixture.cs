using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IMatterGetEligibleStatusesStrategy))]
	internal class MatterGetEligibleStatusesStrategyFixture : ApiServiceTestFixture<IMatterGetEligibleStatusesStrategy>
	{
		[Test]
		public void GetAll_DoesNotThrowException()
		{
			Assert.DoesNotThrow(() =>
				Sut.GetAll());
		}

		[Test]
		public void GetAll_ReturnsNotEmptyStatusesList()
		{
			ArtifactIdNamePair[] result = Sut.GetAll();
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
