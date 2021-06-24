using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ObjectTypeRequireStrategy))]
	internal class ViewRequireStrategyFixture : ApiServiceTestFixture<IRequireWorkspaceEntityStrategy<View>>
	{
		private IGetWorkspaceEntityByNameStrategy<FixedLengthTextField> _getWorkspaceEntityByNameStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getWorkspaceEntityByNameStrategy = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<FixedLengthTextField>>();
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
			var field = _getWorkspaceEntityByNameStrategy.Get(DefaultWorkspace.ArtifactID, "Control Number");

			View existingView = null;

			ArrangeWorkingWorkspace(x => x
				.Create(new View
				{
					Owner = new NamedArtifact { Name = string.Empty },
					Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
					QueryHint = string.Empty,
					RelativityApplications = new List<NamedArtifact>(),
					Sorts = new List<Sort>(),
				})
				.Pick(out existingView));

			var toUpdate = existingView.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Order = Randomizer.GetInt(101, 99999);

			var result = Sut.Require(DefaultWorkspace.ArtifactID, toUpdate);

			result.Should().BeEquivalentTo(toUpdate, o => o
				.Excluding(x => x.SystemLastModifiedOn));
		}

		[Test]
		public void Require_Missing()
		{
			var field = _getWorkspaceEntityByNameStrategy.Get(DefaultWorkspace.ArtifactID, "Control Number");

			var entity = new View
			{
				Name = Randomizer.GetString(),
				Order = Randomizer.GetInt(101, 99999),
				Owner = new NamedArtifact { Name = string.Empty },
				Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
				QueryHint = string.Empty,
				RelativityApplications = new List<NamedArtifact>(),
				Sorts = new List<Sort>(),
			};

			var result = Sut.Require(DefaultWorkspace.ArtifactID, entity);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o
				.Excluding(x => x.ArtifactID)
				.Excluding(x => x.IsVisible)
				.Excluding(x => x.SearchCriteria)
				.Excluding(x => x.SystemCreatedBy)
				.Excluding(x => x.SystemCreatedOn)
				.Excluding(x => x.SystemLastModifiedBy)
				.Excluding(x => x.SystemLastModifiedOn));
		}
	}
}
