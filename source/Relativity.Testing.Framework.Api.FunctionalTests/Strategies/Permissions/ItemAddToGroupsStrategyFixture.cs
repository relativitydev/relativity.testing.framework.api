using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[Ignore("Refactor when item support will be implemented. By item, we mean ObjectType or Field or FieldCategory.")]
	[TestOf(typeof(IItemAddToGroupsStrategy))]
	internal class ItemAddToGroupsStrategyFixture : ApiServiceTestFixture<IItemAddToGroupsStrategy>
	{
		private IWorkspaceAddToGroupsStrategy _workspaceAddToGroupsStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_workspaceAddToGroupsStrategy = Facade.Resolve<IWorkspaceAddToGroupsStrategy>();
		}

		[Test]
		public async Task AddItemToGroupsAsyncByName()
		{
			Group[] groups = null;
			int itemArtifactId = 0;

			Arrange(async domain =>
			{
				domain.Create(2, out groups);

				await _workspaceAddToGroupsStrategy.AddWorkspaceToGroupsAsync(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray()).ConfigureAwait(false);
				itemArtifactId = 1035255;
			});

			await Sut.AddItemToGroupsAsync(DefaultWorkspace.ArtifactID, itemArtifactId, groups.Select(x => x.Name).ToArray()).ConfigureAwait(false);

			GroupSelector groupSelector = await Facade.Resolve<IItemGroupSelectorGetStrategy>().
				GetAsync(DefaultWorkspace.ArtifactID, itemArtifactId).ConfigureAwait(false);

			groupSelector.EnabledGroups.Select(x => x.Name).
				Should().BeEquivalentTo(groups.Select(x => x.Name));
		}

		[Test]
		public async Task AddItemToGroupsAsyncById()
		{
			Group[] groups = null;
			int itemArtifactId = 0;

			Arrange(async domain =>
			{
				domain.Create(2, out groups);

				await _workspaceAddToGroupsStrategy.AddWorkspaceToGroupsAsync(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray()).ConfigureAwait(false);
				itemArtifactId = 1035255;
			});

			await Sut.AddItemToGroupsAsync(DefaultWorkspace.ArtifactID, itemArtifactId, groups.Select(x => x.ArtifactID).ToArray()).ConfigureAwait(false);

			GroupSelector groupSelector = await Facade.Resolve<IItemGroupSelectorGetStrategy>().GetAsync(DefaultWorkspace.ArtifactID, itemArtifactId).ConfigureAwait(false);

			groupSelector.EnabledGroups.Select(x => x.Name).
				Should().BeEquivalentTo(groups.Select(x => x.Name));
		}
	}
}
