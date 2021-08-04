using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IItemRemoveFromGroupsStrategy))]
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
		public async Task RemoveItemFromGroupsAsyncByName()
		{
			Group[] groups = null;
			ObjectType objectType = null;

			Arrange(domain =>
			{
				domain.Create(2, out groups);
				ArrangeWorkingWorkspace(x => x.Create(new ObjectType()).Pick(out objectType));

				Facade.Resolve<IAdminAddToGroupsStrategy>()
					.AddToGroupsAsync(groups.Select(x => x.ArtifactID).ToArray()).GetAwaiter().GetResult();

				_workspaceAddToGroupsStrategy.AddWorkspaceToGroupsAsync(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray()).GetAwaiter().GetResult();

				Facade.Resolve<IItemLevelSecuritySetStrategy>()
					.SetAsync(DefaultWorkspace.ArtifactID, objectType.ArtifactID, true).GetAwaiter().GetResult();

				_itemAddToGroupsStrategy.AddItemToGroupsAsync(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.Name).ToArray()).GetAwaiter().GetResult();
			});

			await Sut.RemoveItemFromGroupsAsync(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.Name).ToArray()).ConfigureAwait(false);

			GroupSelector groupSelector = await _itemGroupSelectorGetStrategy.
				GetAsync(DefaultWorkspace.ArtifactID, objectType.ArtifactID).ConfigureAwait(false);

			groupSelector.DisabledGroups.Select(x => x.Name).
				Should().BeEquivalentTo(groups.Select(x => x.Name));
		}

		[Test]
		public async Task RemoveItemFromGroupsAsyncById()
		{
			Group[] groups = null;
			ObjectType objectType = null;

			Arrange(domain =>
			{
				domain.Create(2, out groups);
				ArrangeWorkingWorkspace(x => x.Create(new ObjectType()).Pick(out objectType));

				Facade.Resolve<IAdminAddToGroupsStrategy>().AddToGroupsAsync(groups.Select(x => x.ArtifactID).ToArray()).GetAwaiter().GetResult();

				_workspaceAddToGroupsStrategy.AddWorkspaceToGroupsAsync(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray()).GetAwaiter().GetResult();

				Facade.Resolve<IItemLevelSecuritySetStrategy>().SetAsync(DefaultWorkspace.ArtifactID, objectType.ArtifactID, true).GetAwaiter().GetResult();

				_itemAddToGroupsStrategy.AddItemToGroupsAsync(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.Name).ToArray()).GetAwaiter().GetResult();
			});

			await Sut.RemoveItemFromGroupsAsync(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.ArtifactID).ToArray()).ConfigureAwait(false);

			GroupSelector groupSelector = await _itemGroupSelectorGetStrategy.GetAsync(DefaultWorkspace.ArtifactID, objectType.ArtifactID).ConfigureAwait(false);

			groupSelector.DisabledGroups.Select(x => x.Name).
				Should().BeEquivalentTo(groups.Select(x => x.Name));
		}
	}
}
