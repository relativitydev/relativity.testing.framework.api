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
	[NonParallelizable]
	[TestOf(typeof(IGetAdminGroupUsersStrategy))]
	internal class GetAdminGroupUsersStrategyFixture : ApiServiceTestFixture<IGetAdminGroupUsersStrategy>
	{
		private const int MissingId = 999_999_999;

		private Group _groupWithNoUsers;
		private Group _groupWithUser;
		private User _user;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x
				.Create(out _groupWithNoUsers)
				.Create(out _groupWithUser)
				.Create(out _user)
					.AddTo(_groupWithUser));
		}

		[Test]
		public void GetAsync_WithMissingGroup()
		{
			var exception = Assert.ThrowsAsync<HttpRequestException>(async () =>
				await Sut.GetAsync(MissingId).ConfigureAwait(false));

			exception.Message.Should().StartWith($"GroupID {MissingId} is invalid.");
		}

		[Test]
		public async Task GetAsync_WithNoUsers_ById()
		{
			var result = await Sut.GetAsync(_groupWithNoUsers.ArtifactID).ConfigureAwait(false);

			result.Should().BeEmpty();
		}

		[Test]
		public async Task GetAsync_WithNoUsers_ByName()
		{
			var result = await Sut.GetAsync(_groupWithNoUsers.Name).ConfigureAwait(false);

			result.Should().BeEmpty();
		}

		[Test]
		public async Task GetAsync_WithSingleUser_ById()
		{
			var result = await Sut.GetAsync(_groupWithUser.ArtifactID).ConfigureAwait(false);

			TestIfUsersListIsAsExpected(result);
		}

		[Test]
		public async Task GetAsync_WithSingleUser_ByName()
		{
			var result = await Sut.GetAsync(_groupWithUser.Name).ConfigureAwait(false);

			TestIfUsersListIsAsExpected(result);
		}

		private void TestIfUsersListIsAsExpected(List<NamedArtifact> result)
		{
			result.Count.Should().Be(1);
			result[0].ArtifactID.Should().Be(_user.ArtifactID);
			result[0].Name.Should().Be($"{_user.LastName}, {_user.FirstName}");
		}
	}
}
