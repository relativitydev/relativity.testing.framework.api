using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[TestOf(typeof(IItemPermissionService))]
	internal class ItemChangeGroupPermissionsStrategyFixture : ApiServiceTestFixture<IItemPermissionService>
	{
		private Group _group;
		private ObjectType _objectType;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x
				.Create(out _group));

			_objectType = Facade.Resolve<IObjectTypeService>()
				.Create(-1, new ObjectType());

			Facade.Resolve<IAdminPermissionService>().AddToGroups(_group.ArtifactID);
		}

		[Test]
		[NonParallelizable]
		public void Enable_ViewEditByName()
		{
			const string permissionName = "Object Rule";

			Sut.SetItemGroupPermissions(-1, _objectType.ArtifactID, _group.Name, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.ViewEdit));

			var result = Sut.GetItemGroupPermissions(-1, _objectType.ArtifactID, _group.ArtifactID).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeTrue();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		[NonParallelizable]
		public void Enable_ViewEditById()
		{
			const string permissionName = "Object Rule";

			Sut.SetItemGroupPermissions(-1, _objectType.ArtifactID, _group.ArtifactID, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.ViewEdit));

			var result = Sut.GetItemGroupPermissions(-1, _objectType.ArtifactID, _group.ArtifactID).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeTrue();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		[NonParallelizable]
		public void Disable_All_BesidesViewByName()
		{
			const string permissionName = "Object Rule";

			Sut.SetItemGroupPermissions(-1, _objectType.ArtifactID, _group.Name, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View));

			var result = Sut.GetItemGroupPermissions(-1, _objectType.ArtifactID, _group.ArtifactID).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		[NonParallelizable]
		public void Disable_All_BesidesViewById()
		{
			const string permissionName = "Object Rule";

			Sut.SetItemGroupPermissions(-1, _objectType.ArtifactID, _group.ArtifactID, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View));

			var result = Sut.GetItemGroupPermissions(-1, _objectType.ArtifactID, _group.ArtifactID).ObjectPermissions.First(x => x.Name == permissionName);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}
	}
}
