using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<Production>))]
	internal class ProductionsGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<Production>>
	{
		public ProductionsGetByIdStrategyFixture()
		{
		}

		public ProductionsGetByIdStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Production existingProductionSet = null;

			Arrange(() =>
			{
				existingProductionSet = Facade.Resolve<ICreateWorkspaceEntityStrategy<Production>>()
					.Create(DefaultWorkspace.ArtifactID, new Production());
			});

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingProductionSet.ArtifactID);

			result.Should().BeEquivalentTo(existingProductionSet);
		}
	}
}
