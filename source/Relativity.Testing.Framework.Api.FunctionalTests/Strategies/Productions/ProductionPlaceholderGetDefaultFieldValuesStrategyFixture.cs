using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IProductionPlaceholderGetDefaultFieldValuesStrategy))]
	internal class ProductionPlaceholderGetDefaultFieldValuesStrategyFixture : ApiServiceTestFixture<IProductionPlaceholderGetDefaultFieldValuesStrategy>
	{
		[Test]
		public void Get_ShouldNotThrow()
		{
			Assert.DoesNotThrow(() => Sut.Get(DefaultWorkspace.ArtifactID));
		}
	}
}
