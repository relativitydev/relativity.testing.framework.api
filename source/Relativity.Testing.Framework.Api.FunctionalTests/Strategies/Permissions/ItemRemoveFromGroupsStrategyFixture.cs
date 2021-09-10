using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IItemRemoveFromGroupsStrategy))]
	[Parallelizable(ParallelScope.Fixtures)]
	internal class ItemRemoveFromGroupsStrategyFixture : ApiServiceTestFixture<IItemRemoveFromGroupsStrategy>
	{
		private IWorkspaceAddToGroupsStrategy _workspaceAddToGroupsStrategy;
		private IItemAddToGroupsStrategy _itemAddToGroupsStrategy;
		private IItemGroupSelectorGetStrategy _itemGroupSelectorGetStrategy;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_workspaceAddToGroupsStrategy = Facade.Resolve<IWorkspaceAddToGroupsStrategy>();
			_itemAddToGroupsStrategy = Facade.Resolve<IItemAddToGroupsStrategy>();
			_itemGroupSelectorGetStrategy = Facade.Resolve<IItemGroupSelectorGetStrategy>();
		}

		[Test]
		public void RemoveItemFromGroupsByName()
		{
			Group[] groups = null;
			ObjectType objectType = null;

			Arrange(domain =>
			{
				domain.Create(2, out groups);
				ArrangeWorkingWorkspace(x => x.Create(new ObjectType()).Pick(out objectType));

				Facade.Resolve<IAdminAddToGroupsStrategy>()
					.AddToGroups(groups.Select(x => x.ArtifactID).ToArray());

				_workspaceAddToGroupsStrategy.AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());

				Facade.Resolve<IItemLevelSecuritySetStrategy>()
					.Set(DefaultWorkspace.ArtifactID, objectType.ArtifactID, true);

				_itemAddToGroupsStrategy.AddItemToGroups(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.Name).ToArray());
			});

			Sut.RemoveItemFromGroups(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.Name).ToArray());

			GroupSelector groupSelector = _itemGroupSelectorGetStrategy.
				Get(DefaultWorkspace.ArtifactID, objectType.ArtifactID);

			groupSelector.DisabledGroups.Select(x => x.Name).
				Should().BeEquivalentTo(groups.Select(x => x.Name));
		}

		[Test]
		public void RemoveItemFromGroupsById()
		{
			Group[] groups = null;
			ObjectType objectType = null;

			Arrange(domain =>
			{
				domain.Create(2, out groups);
				ArrangeWorkingWorkspace(x => x.Create(new ObjectType()).Pick(out objectType));

				Facade.Resolve<IAdminAddToGroupsStrategy>()
					.AddToGroups(groups.Select(x => x.ArtifactID).ToArray());

				_workspaceAddToGroupsStrategy.AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());

				Facade.Resolve<IItemLevelSecuritySetStrategy>()
					.Set(DefaultWorkspace.ArtifactID, objectType.ArtifactID, true);

				_itemAddToGroupsStrategy.AddItemToGroups(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.Name).ToArray());
			});

			Sut.RemoveItemFromGroups(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());

			GroupSelector groupSelector = _itemGroupSelectorGetStrategy.
				Get(DefaultWorkspace.ArtifactID, objectType.ArtifactID);

			groupSelector.DisabledGroups.Select(x => x.Name).
				Should().BeEquivalentTo(groups.Select(x => x.Name));
		}
	}
}
