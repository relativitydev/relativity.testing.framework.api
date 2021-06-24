using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetProductionStatusStrategy))]
	internal class ProductionsGetStatusStrategyFixture : ApiServiceTestFixture<IGetProductionStatusStrategy>
	{
		[Test]
		public void GetStatus_New()
		{
			Production production = null;

			Arrange(() =>
			{
				production = Facade.Resolve<ICreateWorkspaceEntityStrategy<Production>>()
					.Create(DefaultWorkspace.ArtifactID, new Production());
			});

			Sut.GetStatus(DefaultWorkspace.ArtifactID, production.ArtifactID).Should().Be(ProductionStatus.New);
		}
	}
}
