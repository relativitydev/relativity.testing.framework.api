using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<View>))]
	internal class ViewGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<View>>
	{
		private IGetWorkspaceEntityByNameStrategy<FixedLengthTextField> _getWorkspaceEntityByNameStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_getWorkspaceEntityByNameStrategy = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<FixedLengthTextField>>();
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
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

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingView.ArtifactID);

			result.Fields.Should().NotBeNullOrEmpty();
			result.Should().BeEquivalentTo(existingView, o => o
				.Excluding(x => x.SystemCreatedBy)
				.Excluding(x => x.SystemCreatedOn)
				.Excluding(x => x.SystemLastModifiedBy)
				.Excluding(x => x.SystemLastModifiedOn));
		}
	}
}
