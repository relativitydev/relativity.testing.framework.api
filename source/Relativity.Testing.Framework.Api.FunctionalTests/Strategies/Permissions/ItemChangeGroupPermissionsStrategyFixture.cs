using System.Linq;
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
				.AddToGroups(_group.ArtifactID);

			_itemGetGroupPermissionsStrategy = Facade.Resolve<IItemGetGroupPermissionsStrategy>();
		}

		[Test]
		public void Enable_ViewEditByName()
		{
			const string permissionName = "Object Rule";

			Sut.Set(-1, _objectType.ArtifactID, _group.Name, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.ViewEdit));

			var result = _itemGetGroupPermissionsStrategy.Get(-1, _objectType.ArtifactID, _group.ArtifactID).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeTrue();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		public void Enable_ViewEditById()
		{
			const string permissionName = "Object Rule";

			Sut.Set(-1, _objectType.ArtifactID, _group.ArtifactID, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.ViewEdit));

			var result = _itemGetGroupPermissionsStrategy.Get(-1, _objectType.ArtifactID, _group.ArtifactID).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeTrue();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		public void Disable_All_BesidesViewByName()
		{
			const string permissionName = "Object Rule";

			Sut.Set(-1, _objectType.ArtifactID, _group.Name, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View));

			var result = _itemGetGroupPermissionsStrategy.Get(-1, _objectType.ArtifactID, _group.ArtifactID).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		public void Disable_All_BesidesViewById()
		{
			const string permissionName = "Object Rule";

			Sut.Set(-1, _objectType.ArtifactID, _group.ArtifactID, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View));

			var result = _itemGetGroupPermissionsStrategy.Get(-1, _objectType.ArtifactID, _group.ArtifactID).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}
	}
}
