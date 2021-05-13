using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateWorkspaceEntityStrategy<View>))]
	internal class ViewCreateStrategyFixture : ApiServiceTestFixture<ICreateWorkspaceEntityStrategy<View>>
	{
		private IGetWorkspaceEntityByNameStrategy<FixedLengthTextField> _getWorkspaceEntityByNameStrategy;

		public ViewCreateStrategyFixture()
		{
		}

		public ViewCreateStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_getWorkspaceEntityByNameStrategy = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<FixedLengthTextField>>();
		}

		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Create_WithEmptyEntity()
		{
			var field = _getWorkspaceEntityByNameStrategy.Get(DefaultWorkspace.ArtifactID, "Control Number");

			var entity = new View
			{
				Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.Fields.Should().NotBeNullOrEmpty();
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			var field = _getWorkspaceEntityByNameStrategy.Get(DefaultWorkspace.ArtifactID, "Control Number");

			var entity = new View
			{
				Name = Randomizer.GetString("AT_"),
				Order = Randomizer.GetInt(101, 99999),
				Owner = new NamedArtifact { Name = string.Empty },
				Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
				QueryHint = string.Empty,
				RelativityApplications = new List<NamedArtifact>(),
				Sorts = new List<Sort>(),
				VisibleInDropdown = true,
				IsVisible = true,
				SearchCriteria = new CriteriaCollection
				{
					Conditions = new List<BaseCriteria>
					{
						new Criteria
						{
							Condition = new CriteriaCondition
							{
								FieldIdentifier = new NamedArtifact
								{
									Name = "Control Number"
								},
								Operator = ConditionOperator.Is,
								Value = "DOC1"
							}
						}
					},
					BooleanOperator = BooleanOperator.None
				}
			};

			var result = Sut.Create(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Fields.Should().NotBeNullOrEmpty();
			result.Should().BeEquivalentTo(entity, o => o
				.Excluding(x => x.ArtifactID)
				.Excluding(x => x.SystemCreatedBy)
				.Excluding(x => x.SystemCreatedOn)
				.Excluding(x => x.SystemLastModifiedBy)
				.Excluding(x => x.SystemLastModifiedOn));
		}
	}
}
