using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<KeywordSearch>))]
	internal class KeywordSearchRequireStrategyFixture : ApiServiceTestFixture<IRequireWorkspaceEntityStrategy<KeywordSearch>>
	{
		private ICreateWorkspaceEntityStrategy<KeywordSearch> _createStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_createStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>();
		}

		[Test]
		public void Require_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Require(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Require_Existing()
		{
			KeywordSearch existingKeywordSearch = null;

			Arrange(() =>
			{
				existingKeywordSearch = _createStrategy.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());
			});

			var toUpdate = existingKeywordSearch.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Notes = Randomizer.GetString();
			toUpdate.SearchCriteria = new CriteriaCollection
			{
				Conditions = new System.Collections.Generic.List<BaseCriteria>
					{
						new CriteriaCollection
						{
							Conditions = new System.Collections.Generic.List<BaseCriteria>
							{
								new Criteria
								{
									Condition = new CriteriaCondition(new NamedArtifact { Name = "Control Number" }, ConditionOperator.Contains, "NAT_IMG_003_2pg"),
								},
							}
						}
					}
			};

			var result = Sut.Require(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate, y => y.Excluding(x => x.SystemLastModifiedOn));
		}

		[Test]
		public void Require_Missing()
		{
			const string controlNumber = "Control Number";

			var keywordSearch = new KeywordSearch
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
									Condition = new CriteriaCondition(new NamedArtifact { Name = controlNumber }, ConditionOperator.Contains, "NAT_IMG_002_2pg"),
									BooleanOperator = BooleanOperator.Or
								},
								new Criteria
								{
									Condition = new CriteriaCondition(new NamedArtifact { Name = controlNumber }, ConditionOperator.IsLike, "NAT_IMG_003_2pg"),
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
				Fields = new[] { new NamedArtifact { Name = controlNumber } }
			};

			var result = Sut.Require(DefaultWorkspace.ArtifactID, keywordSearch);

			result.Should().BeEquivalentTo(result);
		}
	}
}
