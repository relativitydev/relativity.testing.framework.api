using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<SearchProvider>))]
	internal class SearchProviderGetByIdStrategyFIxture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<SearchProvider>>
	{
		public SearchProviderGetByIdStrategyFIxture()
		{
		}

		public SearchProviderGetByIdStrategyFIxture(string relativityInstanceAlias)
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
			SearchProvider existingSearchProvider = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out existingSearchProvider));

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingSearchProvider.ArtifactID);

			result.Should().NotBeNull();

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(existingSearchProvider, o => o.Excluding(x => x.ArtifactID));
		}
	}
}
