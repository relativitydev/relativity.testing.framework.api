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
		private IGetWorkspaceEntityByIdStrategy<View> _getWorkspaceEntityByIdStrategy;
		private IGetWorkspaceEntityByNameStrategy<FixedLengthTextField> _getWorkspaceEntityByNameStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_getWorkspaceEntityByIdStrategy = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<View>>();
			_getWorkspaceEntityByNameStrategy = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<FixedLengthTextField>>();
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

			ArrangeWorkingWorkspace(x => x
				.Create(new View
				{
					Owner = new NamedArtifact { Name = string.Empty },
					Fields = new[] { new NamedArtifact { Name = field.Name, ArtifactID = field.ArtifactID } },
					QueryHint = string.Empty,
					RelativityApplications = new List<NamedArtifact>(),
					Sorts = new List<Sort>(),
					IsVisible = false,
					SearchCriteria = new CriteriaCollection()
				})
				.Pick(out existingView));

			var toUpdate = existingView.Copy();
			toUpdate.Name = Randomizer.GetString();
			toUpdate.Order = Randomizer.GetInt(101, 99999);

			Sut.Update(DefaultWorkspace.ArtifactID, toUpdate);

			var result = _getWorkspaceEntityByIdStrategy.Get(DefaultWorkspace.ArtifactID, toUpdate.ArtifactID);
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
