using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[TestOf(typeof(IItemPermissionService))]
	internal class ItemRemoveFromGroupsStrategyFixture : ApiServiceTestFixture<IItemPermissionService>
	{
		private IWorkspacePermissionService _workspacePermissionService;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_workspacePermissionService = Facade.Resolve<IWorkspacePermissionService>();
		}

		[Test]
		public void RemoveItemFromGroupsAsyncByName()
		{
			Group[] groups = null;
			ObjectType objectType = null;

			Arrange(domain =>
			{
				domain.Create(2, out groups);
				ArrangeWorkingWorkspace(x => x.Create(new ObjectType()).Pick(out objectType));

				Facade.Resolve<IAdminPermissionService>()
					.AddToGroups(groups.Select(x => x.ArtifactID).ToArray());

				_workspacePermissionService.AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());

				Facade.Resolve<IItemLevelSecuritySetStrategy>()
					.SetAsync(DefaultWorkspace.ArtifactID, objectType.ArtifactID, true).GetAwaiter().GetResult();

				Sut.AddItemToGroups(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.Name).ToArray());
			});

			Sut.RemoveItemFromGroups(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.Name).ToArray());

			GroupSelector groupSelector = Sut.GetItemGroupSelector(DefaultWorkspace.ArtifactID, objectType.ArtifactID);

			groupSelector.DisabledGroups.Select(x => x.Name).Should().BeEquivalentTo(groups.Select(x => x.Name));
		}

		[Test]
		public void RemoveItemFromGroupsAsyncById()
		{
			Group[] groups = null;
			ObjectType objectType = null;

			Arrange(domain =>
			{
				domain.Create(2, out groups);
				ArrangeWorkingWorkspace(x => x.Create(new ObjectType()).Pick(out objectType));

				Facade.Resolve<IAdminPermissionService>().AddToGroupsAsync(groups.Select(x => x.ArtifactID).ToArray());

				_workspacePermissionService.AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());

				Facade.Resolve<IItemLevelSecuritySetStrategy>().SetAsync(DefaultWorkspace.ArtifactID, objectType.ArtifactID, true).GetAwaiter().GetResult();

				Sut.AddItemToGroups(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.Name).ToArray());
			});

			Sut.RemoveItemFromGroups(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());

			GroupSelector groupSelector = Sut.GetItemGroupSelector(DefaultWorkspace.ArtifactID, objectType.ArtifactID);

			groupSelector.DisabledGroups.Select(x => x.Name).Should().BeEquivalentTo(groups.Select(x => x.Name));
		}
	}
}
