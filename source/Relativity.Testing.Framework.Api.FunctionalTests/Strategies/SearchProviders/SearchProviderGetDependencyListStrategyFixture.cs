using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetDependencyListForWorkspaceEntityStrategy<SearchProvider>))]
	internal class SearchProviderGetDependencyListStrategyFixture : ApiServiceTestFixture<IGetDependencyListForWorkspaceEntityStrategy<SearchProvider>>
	{
		public SearchProviderGetDependencyListStrategyFixture()
		{
		}

		public SearchProviderGetDependencyListStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void GetDependencies()
		{
			SearchProvider existingSearchProvider = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out existingSearchProvider));

			var result = Sut.GetDependencies(DefaultWorkspace.ArtifactID, existingSearchProvider.ArtifactID);

			result.Should().NotBeNullOrEmpty();
		}
	}
}
