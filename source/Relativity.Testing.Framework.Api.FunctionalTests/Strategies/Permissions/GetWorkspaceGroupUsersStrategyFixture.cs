using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Arrangement;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceGroupUsersStrategy))]
	internal class GetWorkspaceGroupUsersStrategyFixture : ApiServiceTestFixture<IGetWorkspaceGroupUsersStrategy>
	{
		private const int MissingId = 999_999_999;
		private Workspace _workspace;
		private Group _groupWithNoUsers;
		private Group _groupWithUser;
		private User _user;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x
				.Create(out _workspace)
				.Create(out _groupWithNoUsers)
					.AddTo(_workspace)
				.Create(out _groupWithUser)
					.AddTo(_workspace)
				.Create(out _user)
					.AddTo(_groupWithUser));
		}

		[Test]
		public void GetAsync_WithMissingWorkspace()
		{
			var exception = Assert.ThrowsAsync<HttpRequestException>(async () =>
				await Sut.GetAsync(MissingId, _groupWithUser.ArtifactID).ConfigureAwait(false));

			exception.Message.Should().StartWith($"Workspace {MissingId} is invalid.");
		}

		[Test]
		public void GetAsync_WithMissingGroup()
		{
			var exception = Assert.ThrowsAsync<HttpRequestException>(async () =>
				await Sut.GetAsync(_workspace.ArtifactID, MissingId).ConfigureAwait(false));

			exception.Message.Should().StartWith($"GroupID {MissingId} is invalid.");
		}

		[Test]
		public async Task GetAsync_WithNoUsers_ById()
		{
			var result = await Sut.GetAsync(_workspace.ArtifactID, _groupWithNoUsers.ArtifactID).ConfigureAwait(false);

			result.Should().BeEmpty();
		}

		[Test]
		public async Task GetAsync_WithNoUsers_ByName()
		{
			var result = await Sut.GetAsync(_workspace.ArtifactID, _groupWithNoUsers.Name).ConfigureAwait(false);

			result.Should().BeEmpty();
		}

		[Test]
		public async Task GetAsync_WithSingleUser_ById()
		{
			var result = await Sut.GetAsync(_workspace.ArtifactID, _groupWithUser.ArtifactID).ConfigureAwait(false);

			CheckIfUsersListIsAsExpected(result);
		}

		[Test]
		public async Task GetAsync_WithSingleUser_ByName()
		{
			var result = await Sut.GetAsync(_workspace.ArtifactID, _groupWithUser.Name).ConfigureAwait(false);

			CheckIfUsersListIsAsExpected(result);
		}

		private void CheckIfUsersListIsAsExpected(List<NamedArtifact> result)
		{
			result.Count.Should().Be(1);
			result[0].ArtifactID.Should().Be(_user.ArtifactID);
			result[0].Name.Should().Be($"{_user.LastName}, {_user.FirstName}");
		}
	}
}
