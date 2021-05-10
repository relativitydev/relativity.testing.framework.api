using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[NonParallelizable]
	[TestOf(typeof(IWorkspaceAddToGroupsStrategy))]
	internal class WorkspaceAddToGroupsStrategyFixture : ApiServiceTestFixture<IWorkspaceAddToGroupsStrategy>
	{
		public WorkspaceAddToGroupsStrategyFixture()
		{
		}

		public WorkspaceAddToGroupsStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void AddWorkspaceToGroups_ById()
		{
			Workspace workspace = null;
			Group[] groups = null;

			Arrange(x => x
				.Create(2, out groups)
				.Create(out workspace));

			Sut.AddWorkspaceToGroups(workspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());

			GroupSelector groupSelector = Facade.Resolve<IGetByWorkspaceIdStrategy<GroupSelector>>().
				Get(workspace.ArtifactID);

			groupSelector.EnabledGroups.Should().
				BeEquivalentTo(groups, o => o.Including(x => x.Name));
		}

		[Test]
		public void AddWorkspaceToGroups_ByName()
		{
			Workspace workspace = null;
			Group[] groups = null;

			Arrange(x => x
				.Create(2, out groups)
				.Create(out workspace));

			Sut.AddWorkspaceToGroups(workspace.ArtifactID, groups.Select(x => x.Name).ToArray());

			GroupSelector groupSelector = Facade.Resolve<IGetByWorkspaceIdStrategy<GroupSelector>>().
				Get(workspace.ArtifactID);

			groupSelector.EnabledGroups.Should().
				BeEquivalentTo(groups, o => o.Including(x => x.Name));
		}
	}
}
