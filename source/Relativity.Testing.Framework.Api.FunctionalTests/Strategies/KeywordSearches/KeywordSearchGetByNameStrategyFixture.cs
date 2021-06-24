using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<KeywordSearch>))]
	internal class KeywordSearchGetByNameStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByNameStrategy<KeywordSearch>>
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
			KeywordSearch existingKeywordSearch = null;

			Arrange(() =>
			{
				existingKeywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
					.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());
			});

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingKeywordSearch.Name);

			result.Should().NotBeNull();

			result.ArtifactID.Should().BePositive();
			result.Name.Should().BeEquivalentTo(existingKeywordSearch.Name);
			result.SearchCriteria.Should().BeEquivalentTo(existingKeywordSearch.SearchCriteria);
			result.Fields.Select(x => x.Name).Should().BeEquivalentTo(existingKeywordSearch.Fields.Select(x => x.Name));
			result.Sorts.Should().BeEquivalentTo(existingKeywordSearch.Sorts, o => o.Excluding(x => x.FieldIdentifier.ArtifactID));
		}
	}
}
