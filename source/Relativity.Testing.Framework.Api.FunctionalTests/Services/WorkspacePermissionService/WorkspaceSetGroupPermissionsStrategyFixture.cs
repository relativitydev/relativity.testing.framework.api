using System;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Arrangement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[TestOf(typeof(IWorkspacePermissionService))]
	internal class WorkspaceSetGroupPermissionsStrategyFixture : ApiServiceTestFixture<IWorkspacePermissionService>
	{
		private Workspace _workspace;
		private Group _group;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x
				.Create(out _workspace)
				.Create(out _group)
					.AddTo(_workspace));
		}

		[Test]
		public void SetAsync_WithMissingWorkspace()
		{
			int missingId = int.MaxValue;

			var exception = Assert.Throws<HttpRequestException>(() =>
				Sut.SetWorkspaceGroupPermissions(missingId, new GroupPermissions()));

			exception.Message.Should().StartWith($"Workspace {missingId} is invalid.");
		}

		[Test]
		public void SetAsync_WithNullPermissions()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.SetWorkspaceGroupPermissions(_workspace.ArtifactID, null));
		}

		[Test]
		public void SetAsync_ById()
		{
			GroupPermissions permissions = null;

			Arrange(() =>
				permissions = Sut.GetWorkspaceGroupPermissions(_workspace.ArtifactID, _group.ArtifactID));

			permissions.ObjectPermissions[0].ViewSelected = !permissions.ObjectPermissions[0].ViewSelected;

			Sut.SetWorkspaceGroupPermissions(_workspace.ArtifactID, permissions);

			GroupPermissions gotPermissions = Sut.GetWorkspaceGroupPermissions(_workspace.ArtifactID, _group.ArtifactID);

			gotPermissions.Should().BeEquivalentTo(permissions, o => o.Excluding(x => x.LastModified));
			gotPermissions.LastModified.Should().BeAfter(permissions.LastModified);
		}

		[Test]
		public void SetAsync_ByName()
		{
			GroupPermissions permissions = null;

			Arrange(() =>
				permissions = Sut.GetWorkspaceGroupPermissions(_workspace.ArtifactID, _group.Name));

			permissions.ObjectPermissions[0].ViewSelected = !permissions.ObjectPermissions[0].ViewSelected;

			Sut.SetWorkspaceGroupPermissions(_workspace.ArtifactID, permissions);

			GroupPermissions gotPermissions = Sut.GetWorkspaceGroupPermissions(_workspace.ArtifactID, _group.Name);

			gotPermissions.Should().BeEquivalentTo(permissions, o => o.Excluding(x => x.LastModified));
			gotPermissions.LastModified.Should().BeAfter(permissions.LastModified);
		}
	}
}
