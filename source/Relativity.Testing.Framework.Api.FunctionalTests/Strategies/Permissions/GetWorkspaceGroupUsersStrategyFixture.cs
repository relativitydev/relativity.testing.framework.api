using System.Net.Http;
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

		public GetWorkspaceGroupUsersStrategyFixture()
		{
		}

		public GetWorkspaceGroupUsersStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

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
		public void Get_WithMissingWorkspace()
		{
			var exception = Assert.Throws<HttpRequestException>(() =>
				Sut.Get(MissingId, _groupWithUser.ArtifactID));

			exception.Message.Should().StartWith($"Workspace {MissingId} is invalid.");
		}

		[Test]
		public void Get_WithMissingGroup()
		{
			var exception = Assert.Throws<HttpRequestException>(() =>
				Sut.Get(_workspace.ArtifactID, MissingId));

			exception.Message.Should().StartWith($"GroupID {MissingId} is invalid.");
		}

		[Test]
		public void Get_WithNoUsers_ById()
		{
			var result = Sut.Get(_workspace.ArtifactID, _groupWithNoUsers.ArtifactID);

			result.Should().BeEmpty();
		}

		[Test]
		public void Get_WithNoUsers_ByName()
		{
			var result = Sut.Get(_workspace.ArtifactID, _groupWithNoUsers.Name);

			result.Should().BeEmpty();
		}

		[Test]
		public void Get_WithSingleUser_ById()
		{
			var result = Sut.Get(_workspace.ArtifactID, _groupWithUser.ArtifactID);

			result.Count.Should().Be(1);
			result[0].ArtifactID.Should().Be(_user.ArtifactID);
			result[0].Name.Should().Be($"{_user.LastName}, {_user.FirstName}");
		}

		[Test]
		public void Get_WithSingleUser_ByName()
		{
			var result = Sut.Get(_workspace.ArtifactID, _groupWithUser.Name);

			result.Count.Should().Be(1);
			result[0].ArtifactID.Should().Be(_user.ArtifactID);
			result[0].Name.Should().Be($"{_user.LastName}, {_user.FirstName}");
		}
	}
}
