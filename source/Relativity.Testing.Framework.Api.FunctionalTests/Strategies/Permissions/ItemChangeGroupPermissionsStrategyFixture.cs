using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[NonParallelizable]
	[TestOf(typeof(IItemChangeGroupPermissionsStrategy))]
	internal class ItemChangeGroupPermissionsStrategyFixture : ApiServiceTestFixture<IItemChangeGroupPermissionsStrategy>
	{
		private Group _group;
		private ObjectType _objectType;
		private IItemGetGroupPermissionsStrategy _itemGetGroupPermissionsStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x
				.Create(out _group));

			_objectType = Facade.Resolve<IObjectTypeService>()
				.Create(-1, new ObjectType());

			Facade.Resolve<IAdminAddToGroupsStrategy>()
				.AddToGroupsAsync(_group.ArtifactID).GetAwaiter().GetResult();

			_itemGetGroupPermissionsStrategy = Facade.Resolve<IItemGetGroupPermissionsStrategy>();
		}

		[Test]
		public async Task Enable_ViewEditByName()
		{
			const string permissionName = "Object Rule";

			await Sut.SetAsync(-1, _objectType.ArtifactID, _group.Name, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.ViewEdit)).ConfigureAwait(false);

			var result = (await _itemGetGroupPermissionsStrategy.GetAsync(-1, _objectType.ArtifactID, _group.ArtifactID).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeTrue();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		public async Task Enable_ViewEditById()
		{
			const string permissionName = "Object Rule";

			await Sut.SetAsync(-1, _objectType.ArtifactID, _group.ArtifactID, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.ViewEdit)).ConfigureAwait(false);

			var result = (await _itemGetGroupPermissionsStrategy.GetAsync(-1, _objectType.ArtifactID, _group.ArtifactID).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeTrue();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		public async Task Disable_All_BesidesViewByName()
		{
			const string permissionName = "Object Rule";

			await Sut.SetAsync(-1, _objectType.ArtifactID, _group.Name, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View)).ConfigureAwait(false);

			var result = (await _itemGetGroupPermissionsStrategy.GetAsync(-1, _objectType.ArtifactID, _group.ArtifactID).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		public async Task Disable_All_BesidesViewById()
		{
			const string permissionName = "Object Rule";

			await Sut.SetAsync(-1, _objectType.ArtifactID, _group.ArtifactID, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View)).ConfigureAwait(false);

			var result = (await _itemGetGroupPermissionsStrategy.GetAsync(-1, _objectType.ArtifactID, _group.ArtifactID).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}
	}
}
