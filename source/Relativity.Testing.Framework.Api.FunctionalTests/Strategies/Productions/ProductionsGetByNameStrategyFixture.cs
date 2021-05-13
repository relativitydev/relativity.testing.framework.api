using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByNameStrategy<Production>))]
	internal class ProductionsGetByNameStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByNameStrategy<Production>>
	{
		public ProductionsGetByNameStrategyFixture()
		{
		}

		public ProductionsGetByNameStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, Guid.NewGuid().ToString());

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

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingProductionSet.Name);

			result.Should().NotBeNull();
			result.ArtifactID.Should().BePositive();
			result.Name.Should().BeEquivalentTo(existingProductionSet.Name);
			result.Should().BeEquivalentTo(existingProductionSet);
		}
	}
}
