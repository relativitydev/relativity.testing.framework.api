using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IProductionPlaceholderGetDefaultFieldValuesStrategy))]
	[VersionRange(">=12.1")]
	internal class ProductionPlaceholderGetDefaultFieldValuesStrategyFixture : ApiServiceTestFixture<IProductionPlaceholderGetDefaultFieldValuesStrategy>
	{
		[Test]
		public void Get_ShouldNotThrow()
		{
			Assert.DoesNotThrow(() => Sut.Get(DefaultWorkspace.ArtifactID));
		}
	}
}
