using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateWorkspaceEntityStrategy<KeywordSearch>))]
	internal class KeywordSearchUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<KeywordSearch>>
	{
		private IGetWorkspaceEntityByIdStrategy<KeywordSearch> _getWorkspaceEntityByIdStrategy;
		private ICreateWorkspaceEntityStrategy<KeywordSearch> _createWorkspaceEntityStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_getWorkspaceEntityByIdStrategy = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<KeywordSearch>>();
			_createWorkspaceEntityStrategy = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>();
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Update()
		{
			KeywordSearch existingKeywordSearch = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out existingKeywordSearch));

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
									Condition = new CriteriaCondition(new NamedArtifact { Name = "Control Number" }, ConditionOperator.Is, "NAT_IMG_002_2pg"),
								},
							}
						}
					}
			};

			Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			var result = _getWorkspaceEntityByIdStrategy.Get(DefaultWorkspace.ArtifactID, toUpdate.ArtifactID);
			result.Should().BeEquivalentTo(toUpdate, y => y.Excluding(x => x.SystemLastModifiedOn));
		}
	}
}
