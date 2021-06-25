using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<KeywordSearch>))]
	internal class KeywordSearchCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<KeywordSearch>>
	{
		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Create_WithEmptyEntity()
		{
			var result = Sut.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.ArtifactTypeID.Should().Be(10);
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			const string controlNumber = "Control Number";

			var entity = new KeywordSearch
			{
				Name = Randomizer.GetString(),
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

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().BeEquivalentTo(entity.Name);
			result.SearchCriteria.Should().BeEquivalentTo(entity.SearchCriteria);
			result.Fields.Select(x => x.Name).Should().BeEquivalentTo(entity.Fields.Select(x => x.Name));
			result.Sorts.Should().BeEquivalentTo(entity.Sorts, o => o.Excluding(x => x.FieldIdentifier.ArtifactID));
		}
	}
}
