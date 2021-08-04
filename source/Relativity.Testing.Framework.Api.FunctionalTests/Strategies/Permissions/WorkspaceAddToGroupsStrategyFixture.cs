using System.Linq;
using System.Threading.Tasks;
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
		[Test]
		public async Task AddWorkspaceToGroupsAsync_ById()
		{
			Workspace workspace = null;
			Group[] groups = null;

			Arrange(x => x
				.Create(2, out groups)
				.Create(out workspace));

			await Sut.AddWorkspaceToGroupsAsync(workspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray()).ConfigureAwait(false);

			GroupSelector groupSelector = Facade.Resolve<IGetByWorkspaceIdStrategy<GroupSelector>>().
				Get(workspace.ArtifactID);

			groupSelector.EnabledGroups.Should().
				BeEquivalentTo(groups, o => o.Including(x => x.Name));
		}

		[Test]
		public async Task AddWorkspaceToGroupsAsync_ByName()
		{
			Workspace workspace = null;
			Group[] groups = null;

			Arrange(x => x
				.Create(2, out groups)
				.Create(out workspace));

			await Sut.AddWorkspaceToGroupsAsync(workspace.ArtifactID, groups.Select(x => x.Name).ToArray()).ConfigureAwait(false);

			GroupSelector groupSelector = Facade.Resolve<IGetByWorkspaceIdStrategy<GroupSelector>>().
				Get(workspace.ArtifactID);

			groupSelector.EnabledGroups.Should().
				BeEquivalentTo(groups, o => o.Including(x => x.Name));
		}
	}
}
