using System;
using System.Net.Http;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Arrangement;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IWorkspaceGetGroupPermissionsStrategy))]
	internal class WorkspaceGetGroupPermissionsStrategyFixture : ApiServiceTestFixture<IWorkspaceGetGroupPermissionsStrategy>
	{
		private const int MissingId = 999_999_999;

		private Workspace _workspace;

		private Group _groupNotAddedToWorkspace;

		private Group _groupAddedToWorkspace;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x
				.Create(out _workspace)
				.Create(out _groupNotAddedToWorkspace)
				.Create(out _groupAddedToWorkspace)
					.AddTo(_workspace));
		}

		[Test]
		public void Get_WithMissingWorkspace()
		{
			var exception = Assert.Throws<HttpRequestException>(() =>
				Sut.Get(MissingId, _groupNotAddedToWorkspace.ArtifactID));

			exception.Message.Should().StartWith($"Workspace {MissingId} is invalid.");
		}

		[Test]
		public void Get_WithMissingGroup()
		{
			var result = Sut.Get(_workspace.ArtifactID, MissingId);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing_WithoutAssociation_ById()
		{
			var result = Sut.Get(_workspace.ArtifactID, _groupNotAddedToWorkspace.ArtifactID);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing_WithoutAssociation_ByName()
		{
			var result = Sut.Get(_workspace.ArtifactID, _groupNotAddedToWorkspace.Name);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing_WithAssociation_ById()
		{
			var result = Sut.Get(_workspace.ArtifactID, _groupAddedToWorkspace.ArtifactID);

			result.Should().NotBeNull();

			using (new AssertionScope())
			{
				result.GroupID.Should().BePositive();
				result.ObjectPermissions.Should().NotBeEmpty();
				result.TabVisibility.Should().NotBeEmpty();
				result.BrowserPermissions.Should().NotBeEmpty();
				result.MassActionPermissions.Should().NotBeEmpty();
				result.AdminPermissions.Should().NotBeEmpty();
				result.LastModified.Should().BeAfter(DateTime.MinValue);
			}
		}

		[Test]
		public void Get_Existing_WithAssociation_ByName()
		{
			var result = Sut.Get(_workspace.ArtifactID, _groupAddedToWorkspace.Name);

			result.Should().NotBeNull();

			using (new AssertionScope())
			{
				result.GroupID.Should().BePositive();
				result.ObjectPermissions.Should().NotBeEmpty();
				result.TabVisibility.Should().NotBeEmpty();
				result.BrowserPermissions.Should().NotBeEmpty();
				result.MassActionPermissions.Should().NotBeEmpty();
				result.AdminPermissions.Should().NotBeEmpty();
				result.LastModified.Should().BeAfter(DateTime.MinValue);
			}
		}
	}
}
