using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[Ignore("Refactor when item support will be implemented. By item, we mean ObjectType or Field or FieldCategory.")]
	[TestOf(typeof(IItemPermissionService))]
	internal class ItemAddToGroupsStrategyFixture : ApiServiceTestFixture<IItemPermissionService>
	{
		private IWorkspacePermissionService _workspacePermissionService;

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_workspacePermissionService = Facade.Resolve<IWorkspacePermissionService>();
		}

		[Test]
		public void AddItemToGroupsAsyncByName()
		{
			Group[] groups = null;
			int itemArtifactId = 0;

			Arrange(domain =>
			{
				domain.Create(2, out groups);

				_workspacePermissionService.AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());
				itemArtifactId = 1035255;
			});

			Sut.AddItemToGroups(DefaultWorkspace.ArtifactID, itemArtifactId, groups.Select(x => x.Name).ToArray());

			GroupSelector groupSelector = Sut.GetItemGroupSelector(DefaultWorkspace.ArtifactID, itemArtifactId);

			groupSelector.EnabledGroups.Select(x => x.Name).
				Should().BeEquivalentTo(groups.Select(x => x.Name));
		}

		[Test]
		public void AddItemToGroupsAsyncById()
		{
			Group[] groups = null;
			int itemArtifactId = 0;

			Arrange(domain =>
			{
				domain.Create(2, out groups);

				_workspacePermissionService.AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, groups.Select(x => x.ArtifactID).ToArray());
				itemArtifactId = 1035255;
			});

			Sut.AddItemToGroups(DefaultWorkspace.ArtifactID, itemArtifactId, groups.Select(x => x.ArtifactID).ToArray());

			GroupSelector groupSelector = Sut.GetItemGroupSelector(DefaultWorkspace.ArtifactID, itemArtifactId);

			groupSelector.EnabledGroups.Select(x => x.Name).
				Should().BeEquivalentTo(groups.Select(x => x.Name));
		}
	}
}
