using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetAllWorkspaceEntitiesStrategy<View>))]
	internal class ViewGetAllStrategyFixture : ApiServiceTestFixture<IGetAllWorkspaceEntitiesStrategy<View>>
	{
		private IGetWorkspaceEntityByNameStrategy<FixedLengthTextField> _getWorkspaceEntityByNameStrategy;

		public ViewGetAllStrategyFixture()
		{
		}

		public ViewGetAllStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			_getWorkspaceEntityByNameStrategy = Facade.Resolve<IGetWorkspaceEntityByNameStrategy<FixedLengthTextField>>();
		}

		[Test]
		public void GetAll()
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

			var result = Sut.GetAll(DefaultWorkspace.ArtifactID);

			result.Length.Should().BeGreaterThan(0);
		}
	}
}
