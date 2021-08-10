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
		public void RemoveItemFromGroupsByName()
		{
			Group[] groups = null;
			ObjectType objectType = null;
			ArrangeWorkspace(out groups, out objectType);

			Sut.RemoveItemFromGroups(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.Name).ToArray());

			GroupSelector groupSelector = Sut.GetItemGroupSelector(DefaultWorkspace.ArtifactID, objectType.ArtifactID);

			groupSelector.DisabledGroups.Select(x => x.Name).Should().BeEquivalentTo(groups.Select(x => x.Name));
		}

		[Test]
		public void RemoveItemFromGroupsById()
		{
			Group[] groups = null;
			ObjectType objectType = null;
			ArrangeWorkspace(out groups, out objectType);

			Sut.RemoveItemFromGroups(DefaultWorkspace.ArtifactID, objectType.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());

			GroupSelector groupSelector = Sut.GetItemGroupSelector(DefaultWorkspace.ArtifactID, objectType.ArtifactID);

			groupSelector.DisabledGroups.Select(x => x.Name).Should().BeEquivalentTo(groups.Select(x => x.Name));
		}

		private void ArrangeWorkspace(out Group[] groups, out ObjectType objectType)
		{
			Group[] localGroups = null;
			ObjectType localObjectType = null;

			Arrange(domain =>
			{
				domain.Create(2, out localGroups);
				ArrangeWorkingWorkspace(x => x.Create(new ObjectType()).Pick(out localObjectType));

				Facade.Resolve<IAdminPermissionService>().AddToGroupsAsync(localGroups.Select(x => x.ArtifactID).ToArray());

				_workspacePermissionService.AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, localGroups.Select(x => x.ArtifactID).ToArray());

				Facade.Resolve<IItemLevelSecuritySetStrategy>().SetAsync(DefaultWorkspace.ArtifactID, localObjectType.ArtifactID, true).GetAwaiter().GetResult();

				Sut.AddItemToGroups(DefaultWorkspace.ArtifactID, localObjectType.ArtifactID, localGroups.Select(x => x.Name).ToArray());
			});

			groups = localGroups;
			objectType = localObjectType;
		}
	}
}
