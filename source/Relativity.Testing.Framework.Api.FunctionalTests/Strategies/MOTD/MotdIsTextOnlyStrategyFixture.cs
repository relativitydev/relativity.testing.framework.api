using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IMotdIsTextOnlyStrategy))]
	[NonParallelizable]
	internal class MotdIsTextOnlyStrategyFixture : ApiServiceTestFixture<IMotdIsTextOnlyStrategy>
	{
		[Test]
		public void IsTextOnly_NotThrowException()
		{
			Assert.DoesNotThrow(() => Sut.IsTextOnly());
		}
	}
}
