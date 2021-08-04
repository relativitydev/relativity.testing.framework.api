using System;
using System.Net.Http;
using System.Threading.Tasks;
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
		public void GetAsync_WithMissingWorkspace()
		{
			var exception = Assert.ThrowsAsync<HttpRequestException>(async () =>
				await Sut.GetAsync(MissingId, _groupNotAddedToWorkspace.ArtifactID).ConfigureAwait(false));

			exception.Message.Should().StartWith($"Workspace {MissingId} is invalid.");
		}

		[Test]
		public async Task GetAsync_WithMissingGroup()
		{
			var result = await Sut.GetAsync(_workspace.ArtifactID, MissingId).ConfigureAwait(false);

			result.Should().BeNull();
		}

		[Test]
		public async Task GetAsync_Existing_WithoutAssociation_ById()
		{
			var result = await Sut.GetAsync(_workspace.ArtifactID, _groupNotAddedToWorkspace.ArtifactID).ConfigureAwait(false);

			result.Should().BeNull();
		}

		[Test]
		public async Task GetAsync_Existing_WithoutAssociation_ByName()
		{
			var result = await Sut.GetAsync(_workspace.ArtifactID, _groupNotAddedToWorkspace.Name).ConfigureAwait(false);

			result.Should().BeNull();
		}

		[Test]
		public async Task GetAsync_Existing_WithAssociation_ById()
		{
			var result = await Sut.GetAsync(_workspace.ArtifactID, _groupAddedToWorkspace.ArtifactID).ConfigureAwait(false);

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
		public async Task GetAsync_Existing_WithAssociation_ByName()
		{
			var result = await Sut.GetAsync(_workspace.ArtifactID, _groupAddedToWorkspace.Name).ConfigureAwait(false);

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
