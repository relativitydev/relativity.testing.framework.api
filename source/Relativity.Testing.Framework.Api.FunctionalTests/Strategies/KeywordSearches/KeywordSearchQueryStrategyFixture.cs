using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IKeywordSearchQueryStrategy))]
	internal class KeywordSearchQueryStrategyFixture : ApiServiceTestFixture<IKeywordSearchQueryStrategy>
	{
		[Test]
		public void Query_Missing()
		{
			var result = Sut.Query(DefaultWorkspace.ArtifactID, $"'Artifact ID' == {int.MaxValue}");

			result.Should().BeEmpty();
		}

		[Test]
		public void Query_Existing()
		{
			KeywordSearch existingKeywordSearch = null;

			Arrange(() =>
			{
				const string controlNumber = "Control Number";

				var keywordSearch = new KeywordSearch
				{
					SearchCriteria = new CriteriaCollection
					{
						Conditions = new System.Collections.Generic.List<BaseCriteria>
					{
						new CriteriaCollection
						{
							Conditions = new System.Collections.Generic.List<BaseCriteria>
							{
								new Criteria
								{
									Condition = new CriteriaCondition(new NamedArtifact { Name = controlNumber }, ConditionOperator.Is, "NAT_IMG_002_2pg"),
									BooleanOperator = BooleanOperator.Or
								},
								new Criteria
								{
									Condition = new CriteriaCondition(new NamedArtifact { Name = controlNumber }, ConditionOperator.Is, "NAT_IMG_003_2pg"),
								}
							},
							BooleanOperator = BooleanOperator.Or
						},
						new Criteria
						{
							Condition = new CriteriaCondition(new NamedArtifact { Name = controlNumber }, ConditionOperator.Is, "NAT_IMG_00132_1pg")
						}
					}
					},
					Fields = new[] { new NamedArtifact { Name = controlNumber } },
					Sorts = new System.Collections.Generic.List<Sort> { new Sort { FieldIdentifier = new NamedArtifact { Name = controlNumber }, Direction = SortDirection.Descending } }
				};

				existingKeywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
					.Create(DefaultWorkspace.ArtifactID, keywordSearch);
			});

			var results = Sut.Query(DefaultWorkspace.ArtifactID, $"'Artifact ID' == {existingKeywordSearch.ArtifactID}");

			results.Should().NotBeNullOrEmpty();
			var result = results.First();

			result.ArtifactID.Should().BePositive();
			result.Name.Should().BeEquivalentTo(existingKeywordSearch.Name);
			result.SearchCriteria.Should().BeEquivalentTo(existingKeywordSearch.SearchCriteria);
			result.Fields.Select(x => x.Name).Should().BeEquivalentTo(existingKeywordSearch.Fields.Select(x => x.Name));
			result.Sorts.Should().BeEquivalentTo(existingKeywordSearch.Sorts, o => o.Excluding(x => x.FieldIdentifier.ArtifactID));
		}
	}
}
