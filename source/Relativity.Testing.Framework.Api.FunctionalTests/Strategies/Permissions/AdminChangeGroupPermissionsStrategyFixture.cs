using System.Linq;
using System.Threading.Tasks;
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
				.AddToGroupsAsync(_group.ArtifactID).GetAwaiter().GetResult();
		}

		[Test]
		public async Task Enable_All_ById()
		{
			ObjectPermission permission = null;

			Arrange(() =>
				permission = _adminGetGroupPermissionsStrategy.GetAsync(_group.ArtifactID).GetAwaiter().GetResult().ObjectPermissions.First(x => x.Name == "Agent"));

			await Sut.SetAsync(_group.ArtifactID, x => x.ObjectPermissions[permission.Name].EnableAll()).ConfigureAwait(false);

			var result = (await _adminGetGroupPermissionsStrategy.GetAsync(_group.ArtifactID).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permission.Name);

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
				permission = _adminGetGroupPermissionsStrategy.GetAsync(_group.ArtifactID).GetAwaiter().GetResult().ObjectPermissions.First(x => x.Name == "Agent"));

			await Sut.SetAsync(_group.Name, x => x.ObjectPermissions[permission.Name].EnableAll()).ConfigureAwait(false);

			var result = (await _adminGetGroupPermissionsStrategy.GetAsync(_group.Name).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permission.Name);

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
				permission = _adminGetGroupPermissionsStrategy.GetAsync(_group.ArtifactID).GetAwaiter().GetResult().ObjectPermissions.First(x => x.Name == "Agent"));

			await Sut.SetAsync(_group.ArtifactID, x => x.ObjectPermissions[permission.Name].DisableAll()).ConfigureAwait(false);

			var result = (await _adminGetGroupPermissionsStrategy.GetAsync(_group.ArtifactID).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permission.Name);

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
				permission = _adminGetGroupPermissionsStrategy.GetAsync(_group.ArtifactID).GetAwaiter().GetResult().ObjectPermissions.First(x => x.Name == "Agent"));

			await Sut.SetAsync(_group.Name, x => x.ObjectPermissions[permission.Name].DisableAll()).ConfigureAwait(false);

			var result = (await _adminGetGroupPermissionsStrategy.GetAsync(_group.Name).ConfigureAwait(false)).ObjectPermissions.First(x => x.Name == permission.Name);

			result.ViewSelected.Should().BeFalse();
			result.EditSelected.Should().BeFalse();
			result.DeleteSelected.Should().BeFalse();
			result.AddSelected.Should().BeFalse();
			result.EditSecuritySelected.Should().BeFalse();
		}
	}
}
