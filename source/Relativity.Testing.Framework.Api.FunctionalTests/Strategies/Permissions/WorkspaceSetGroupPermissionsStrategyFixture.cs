using System;
using System.Net.Http;
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

		public WorkspaceSetGroupPermissionsStrategyFixture()
		{
		}

		public WorkspaceSetGroupPermissionsStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

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
		public void Set_WithMissingWorkspace()
		{
			int missingId = int.MaxValue;

			var exception = Assert.Throws<HttpRequestException>(() =>
				Sut.Set(missingId, new GroupPermissions()));

			exception.Message.Should().StartWith($"Workspace {missingId} is invalid.");
		}

		[Test]
		public void Set_WithNullPermissions()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Set(_workspace.ArtifactID, null));
		}

		[Test]
		public void Set_ById()
		{
			GroupPermissions permissions = null;

			Arrange(() =>
				permissions = _workspaceGetGroupPermissionsStrategy.Get(_workspace.ArtifactID, _group.ArtifactID));

			permissions.ObjectPermissions[0].ViewSelected = !permissions.ObjectPermissions[0].ViewSelected;

			Sut.Set(_workspace.ArtifactID, permissions);

			GroupPermissions gotPermissions = _workspaceGetGroupPermissionsStrategy.Get(_workspace.ArtifactID, _group.ArtifactID);

			gotPermissions.Should().BeEquivalentTo(permissions, o => o.Excluding(x => x.LastModified));
			gotPermissions.LastModified.Should().BeAfter(permissions.LastModified);
		}

		[Test]
		public void Set_ByName()
		{
			GroupPermissions permissions = null;

			Arrange(() =>
				permissions = _workspaceGetGroupPermissionsStrategy.Get(_workspace.ArtifactID, _group.Name));

			permissions.ObjectPermissions[0].ViewSelected = !permissions.ObjectPermissions[0].ViewSelected;

			Sut.Set(_workspace.ArtifactID, permissions);

			GroupPermissions gotPermissions = _workspaceGetGroupPermissionsStrategy.Get(_workspace.ArtifactID, _group.Name);

			gotPermissions.Should().BeEquivalentTo(permissions, o => o.Excluding(x => x.LastModified));
			gotPermissions.LastModified.Should().BeAfter(permissions.LastModified);
		}
	}
}
