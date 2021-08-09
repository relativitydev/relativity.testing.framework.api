using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[NonParallelizable]
	[TestOf(typeof(IWorkspacePermissionService))]
	internal class WorkspaceAddToGroupsStrategyFixture : ApiServiceTestFixture<IWorkspacePermissionService>
	{
		[Test]
		public void AddWorkspaceToGroupsAsync_ById()
		{
			Workspace workspace = null;
			Group[] groups = null;

			Arrange(x => x
				.Create(2, out groups)
				.Create(out workspace));

			Sut.AddWorkspaceToGroups(workspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());

			GroupSelector groupSelector = Facade.Resolve<IWorkspacePermissionService>().
				GetWorkspaceGroupSelector(workspace.ArtifactID);

			groupSelector.EnabledGroups.Should().
				BeEquivalentTo(groups, o => o.Including(x => x.Name));
		}

		[Test]
		public void AddWorkspaceToGroupsAsync_ByName()
		{
			Workspace workspace = null;
			Group[] groups = null;

			Arrange(x => x
				.Create(2, out groups)
				.Create(out workspace));

			Sut.AddWorkspaceToGroups(workspace.ArtifactID, groups.Select(x => x.Name).ToArray());

			GroupSelector groupSelector = Facade.Resolve<IWorkspacePermissionService>().
				GetWorkspaceGroupSelector(workspace.ArtifactID);

			groupSelector.EnabledGroups.Should().
				BeEquivalentTo(groups, o => o.Including(x => x.Name));
		}
	}
}
