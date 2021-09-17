using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateWorkspaceEntityStrategy<View>))]
	internal class ViewUpdateStrategyFixture : ApiServiceTestFixture<IUpdateWorkspaceEntityStrategy<View>>
	{
		private IGetWorkspaceEntityByNameStrategy<FixedLengthTextField> _getWorkspaceEntityByNameStrategy;
		private IGetAllWorkspaceViewOwnersStrategy<NamedArtifact> _getViewOwnersStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_getWorkspaceEntityByNameStrategy = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<FixedLengthTextField>>();
			_getViewOwnersStrategy = Facade.Resolve<IGetAllWorkspaceViewOwnersStrategy<NamedArtifact>>();
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(DefaultWorkspace.ArtifactID, null));
		}

		[Test]
		public void Update_WithFilledEntity()
		{
			var field = _getWorkspaceEntityByNameStrategy.Get(DefaultWorkspace.ArtifactID, "Control Number");

			View existingView = null;

			var eligibleOwners = _getViewOwnersStrategy.GetViewOwners(DefaultWorkspace.ArtifactID);
			NamedArtifact owner = eligibleOwners.Length > 1 ? eligibleOwners[1] : new NamedArtifact { Name = string.Empty };

			ArrangeWorkingWorkspace(x => x
				.Create(new View
				{
					Owner = owner,
					Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
					QueryHint = string.Empty,
					RelativityApplications = new List<NamedArtifact>(),
					Sorts = new List<Sort>(),
					IsVisible = false,
					SearchCriteria = new CriteriaCollection(),
					VisibleInDropdown = true
				})
				.Pick(out existingView));

			var toUpdate = existingView.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Order = Randomizer.GetInt(101, 99999);

			var result = Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			result.Fields.Should().NotBeNullOrEmpty();

			result.Should().BeEquivalentTo(toUpdate, o => o
				.Excluding(x => x.IsVisible)
				.Excluding(x => x.SystemCreatedBy)
				.Excluding(x => x.SystemCreatedOn)
				.Excluding(x => x.SystemLastModifiedBy)
				.Excluding(x => x.SystemLastModifiedOn));
		}
	}
}
