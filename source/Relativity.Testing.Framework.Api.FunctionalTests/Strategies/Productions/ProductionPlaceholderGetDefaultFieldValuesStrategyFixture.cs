using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IProductionPlaceholderGetDefaultFieldValuesStrategy))]
	internal class ProductionPlaceholderGetDefaultFieldValuesStrategyFixture : ApiServiceTestFixture<IProductionPlaceholderGetDefaultFieldValuesStrategy>
	{
		[Test]
		public void Get_ShouldNotThrown()
		{
			Assert.DoesNotThrow(() => Sut.Get(DefaultWorkspace.ArtifactID));
		}
	}
}
