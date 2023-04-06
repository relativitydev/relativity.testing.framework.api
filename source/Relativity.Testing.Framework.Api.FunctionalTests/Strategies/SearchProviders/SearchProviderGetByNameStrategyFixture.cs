using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<SearchProvider>))]
	internal class SearchProviderGetByNameStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByNameStrategy<SearchProvider>>
	{
		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, Guid.NewGuid().ToString());

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			SearchProvider existingSearchProvider = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out existingSearchProvider));

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingSearchProvider.Name);

			result.Should().NotBeNull();

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(existingSearchProvider, o => o.Excluding(x => x.ArtifactID));
		}
	}
}
