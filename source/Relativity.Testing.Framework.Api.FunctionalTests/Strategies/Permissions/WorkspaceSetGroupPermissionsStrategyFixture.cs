using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Arrangement;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IWorkspaceSetGroupPermissionsStrategy))]
	internal class WorkspaceSetGroupPermissionsStrategyFixture : ApiServiceTestFixture<IWorkspaceSetGroupPermissionsStrategy>
	{
		private Workspace _workspace;

		private Group _group;

		private IWorkspaceGetGroupPermissionsStrategy _workspaceGetGroupPermissionsStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x
				.Create(out _workspace)
				.Create(out _group)
					.AddTo(_workspace));

			_workspaceGetGroupPermissionsStrategy = Facade.Resolve<IWorkspaceGetGroupPermissionsStrategy>();
		}

		[Test]
		public void SetAsync_WithMissingWorkspace()
		{
			int missingId = int.MaxValue;

			var exception = Assert.ThrowsAsync<HttpRequestException>(async () =>
				await Sut.SetAsync(missingId, new GroupPermissions()).ConfigureAwait(false));

			exception.Message.Should().StartWith($"Workspace {missingId} is invalid.");
		}

		[Test]
		public void SetAsync_WithNullPermissions()
		{
			Assert.ThrowsAsync<ArgumentNullException>(async () =>
				await Sut.SetAsync(_workspace.ArtifactID, null).ConfigureAwait(false));
		}

		[Test]
		public async Task SetAsync_ById()
		{
			GroupPermissions permissions = null;

			Arrange(async () =>
				permissions = await _workspaceGetGroupPermissionsStrategy.GetAsync(_workspace.ArtifactID, _group.ArtifactID).ConfigureAwait(false));

			permissions.ObjectPermissions[0].ViewSelected = !permissions.ObjectPermissions[0].ViewSelected;

			await Sut.SetAsync(_workspace.ArtifactID, permissions).ConfigureAwait(false);

			GroupPermissions gotPermissions = await _workspaceGetGroupPermissionsStrategy.GetAsync(_workspace.ArtifactID, _group.ArtifactID).ConfigureAwait(false);

			gotPermissions.Should().BeEquivalentTo(permissions, o => o.Excluding(x => x.LastModified));
			gotPermissions.LastModified.Should().BeAfter(permissions.LastModified);
		}

		[Test]
		public async Task SetAsync_ByName()
		{
			GroupPermissions permissions = null;

			Arrange(async () =>
				permissions = await _workspaceGetGroupPermissionsStrategy.GetAsync(_workspace.ArtifactID, _group.Name).ConfigureAwait(false));

			permissions.ObjectPermissions[0].ViewSelected = !permissions.ObjectPermissions[0].ViewSelected;

			await Sut.SetAsync(_workspace.ArtifactID, permissions).ConfigureAwait(false);

			GroupPermissions gotPermissions = await _workspaceGetGroupPermissionsStrategy.GetAsync(_workspace.ArtifactID, _group.Name).ConfigureAwait(false);

			gotPermissions.Should().BeEquivalentTo(permissions, o => o.Excluding(x => x.LastModified));
			gotPermissions.LastModified.Should().BeAfter(permissions.LastModified);
		}
	}
}
