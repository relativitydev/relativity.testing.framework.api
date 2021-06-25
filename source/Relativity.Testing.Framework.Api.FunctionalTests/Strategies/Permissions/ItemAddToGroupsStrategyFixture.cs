using System.Linq;
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
		public void AddItemToGroupsByName()
		{
			Group[] groups = null;
			int itemArtifactId = 0;

			Arrange(domain =>
			{
				domain.Create(2, out groups);

				_workspaceAddToGroupsStrategy.AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());
				itemArtifactId = 1035255;
			});

			Sut.AddItemToGroups(DefaultWorkspace.ArtifactID, itemArtifactId, groups.Select(x => x.Name).ToArray());

			GroupSelector groupSelector = Facade.Resolve<IItemGroupSelectorGetStrategy>().
				Get(DefaultWorkspace.ArtifactID, itemArtifactId);

			groupSelector.EnabledGroups.Select(x => x.Name).
				Should().BeEquivalentTo(groups.Select(x => x.Name));
		}

		[Test]
		public void AddItemToGroupsById()
		{
			Group[] groups = null;
			int itemArtifactId = 0;

			Arrange(domain =>
			{
				domain.Create(2, out groups);

				_workspaceAddToGroupsStrategy.AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());
				itemArtifactId = 1035255;
			});

			Sut.AddItemToGroups(DefaultWorkspace.ArtifactID, itemArtifactId, groups.Select(x => x.ArtifactID).ToArray());

			GroupSelector groupSelector = Facade.Resolve<IItemGroupSelectorGetStrategy>().
				Get(DefaultWorkspace.ArtifactID, itemArtifactId);

			groupSelector.EnabledGroups.Select(x => x.Name).
				Should().BeEquivalentTo(groups.Select(x => x.Name));
		}
	}
}
