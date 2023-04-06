using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<KeywordSearch>))]
	internal class KeywordSearchGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<KeywordSearch>>
	{
		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			KeywordSearch existingKeywordSearch = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out existingKeywordSearch));

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingKeywordSearch.ArtifactID);

			result.Should().NotBeNull();

			result.ArtifactID.Should().BePositive();
			result.Name.Should().BeEquivalentTo(existingKeywordSearch.Name);
			result.SearchCriteria.Should().BeEquivalentTo(existingKeywordSearch.SearchCriteria);
			result.Fields.Select(x => x.Name).Should().BeEquivalentTo(existingKeywordSearch.Fields.Select(x => x.Name));
			result.Sorts.Should().BeEquivalentTo(existingKeywordSearch.Sorts, o => o.Excluding(x => x.FieldIdentifier.ArtifactID));
		}
	}
}
