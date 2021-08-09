using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[NonParallelizable]
	[TestOf(typeof(IAdminPermissionService))]
	internal class AdminPermissionServiceFixture : ApiServiceTestFixture<IAdminPermissionService>
	{
		private Group _group;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x
				.Create(out _group));

			Facade.Resolve<IAdminPermissionService>().AddToGroups(_group.ArtifactID);
		}

		[Test]
		public async Task Enable_All_ById()
		{
			ObjectPermission permission = null;

			Arrange(() =>
				permission = Sut.GetAdminGroupPermissions(_group.ArtifactID).ObjectPermissions.First(x => x.Name == "Agent"));

			Sut.SetAdminGroupPermissions(_group.ArtifactID, x => x.ObjectPermissions[permission.Name].EnableAll());

			var result = (await Sut.GetAdminGroupPermissionsAsync(_group.ArtifactID).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permission.Name);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeTrue();
			result.DeleteSelected.Should().BeTrue();
			result.AddSelected.Should().BeTrue();
			result.EditSecuritySelected.Should().BeTrue();
		}

		[Test]
		public async Task Enable_All_ByName()
		{
			ObjectPermission permission = null;

			Arrange(() =>
				permission = Sut.GetAdminGroupPermissions(_group.ArtifactID).ObjectPermissions.First(x => x.Name == "Agent"));

			Sut.SetAdminGroupPermissions(_group.Name, x => x.ObjectPermissions[permission.Name].EnableAll());

			var result = (await Sut.GetAdminGroupPermissionsAsync(_group.Name).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permission.Name);

			result.ViewSelected.Should().BeTrue();
			result.EditSelected.Should().BeTrue();
			result.DeleteSelected.Should().BeTrue();
			result.AddSelected.Should().BeTrue();
			result.EditSecuritySelected.Should().BeTrue();
		}

		[Test]
		public async Task Disable_All_ById()
		{
			ObjectPermission permission = null;

			Arrange(() =>
				permission = Sut.GetAdminGroupPermissions(_group.ArtifactID).ObjectPermissions.First(x => x.Name == "Agent"));

			Sut.SetAdminGroupPermissions(_group.ArtifactID, x => x.ObjectPermissions[permission.Name].DisableAll());

			var result = (await Sut.GetAdminGroupPermissionsAsync(_group.ArtifactID).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permission.Name);

			result.ViewSelected.Should().BeFalse();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}

		[Test]
		public async Task Disable_All_ByName()
		{
			ObjectPermission permission = null;

			Arrange(() =>
				permission = Sut.GetAdminGroupPermissions(_group.ArtifactID).ObjectPermissions.First(x => x.Name == "Agent"));

			Sut.SetAdminGroupPermissions(_group.Name, x => x.ObjectPermissions[permission.Name].DisableAll());

			var result = (await Sut.GetAdminGroupPermissionsAsync(_group.Name).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permission.Name);

			result.ViewSelected.Should().BeFalse();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}
	}
}
