using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[NonParallelizable]
	[TestOf(typeof(IAdminChangeGroupPermissionsStrategy))]
	internal class AdminChangeGroupPermissionsStrategyFixture : ApiServiceTestFixture<IAdminChangeGroupPermissionsStrategy>
	{
		private Group _group;
		private IAdminGetGroupPermissionsStrategy _adminGetGroupPermissionsStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x
				.Create(out _group));

			_adminGetGroupPermissionsStrategy = Facade.Resolve<IAdminGetGroupPermissionsStrategy>();

			Facade.Resolve<IAdminAddToGroupsStrategy>()
				.AddToGroups(_group.ArtifactID);
		}

		[Test]
		public void Enable_All_ById()
		{
			ObjectPermission permission = null;

			Arrange(() =>
				permission = _adminGetGroupPermissionsStrategy.Get(_group.ArtifactID).ObjectPermissions.First(x => x.Name == "Agent"));

			Sut.Set(_group.ArtifactID, x => x.ObjectPermissions[permission.Name].EnableAll());

			var result = _adminGetGroupPermissionsStrategy.Get(_group.ArtifactID).ObjectPermissions.First(x => x.Name == permission.Name);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeTrue();
			result.DeleteSelected.Should().BeTrue();
			result.AddSelected.Should().BeTrue();
			result.EditSecuritySelected.Should().BeTrue();
		}

		[Test]
		public void Enable_All_ByName()
		{
			ObjectPermission permission = null;

			Arrange(() =>
				permission = _adminGetGroupPermissionsStrategy.Get(_group.ArtifactID).ObjectPermissions.First(x => x.Name == "Agent"));

			Sut.Set(_group.Name, x => x.ObjectPermissions[permission.Name].EnableAll());

			var result = _adminGetGroupPermissionsStrategy.Get(_group.ArtifactID).ObjectPermissions.First(x => x.Name == permission.Name);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeTrue();
			result.DeleteSelected.Should().BeTrue();
			result.AddSelected.Should().BeTrue();
			result.EditSecuritySelected.Should().BeTrue();
		}

		[Test]
		public void Disable_All_ById()
		{
			ObjectPermission permission = null;

			Arrange(() =>
				permission = _adminGetGroupPermissionsStrategy.Get(_group.ArtifactID).ObjectPermissions.First(x => x.Name == "Agent"));

			Sut.Set(_group.ArtifactID, x => x.ObjectPermissions[permission.Name].DisableAll());

			var result = _adminGetGroupPermissionsStrategy.Get(_group.ArtifactID).ObjectPermissions.First(x => x.Name == permission.Name);

			result.ViewSelected.Should().BeFalse();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		public void Disable_All_ByName()
		{
			ObjectPermission permission = null;

			Arrange(() =>
				permission = _adminGetGroupPermissionsStrategy.Get(_group.ArtifactID).ObjectPermissions.First(x => x.Name == "Agent"));

			Sut.Set(_group.Name, x => x.ObjectPermissions[permission.Name].DisableAll());

			var result = _adminGetGroupPermissionsStrategy.Get(_group.Name).ObjectPermissions.First(x => x.Name == permission.Name);

			result.ViewSelected.Should().BeFalse();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}
	}
}
