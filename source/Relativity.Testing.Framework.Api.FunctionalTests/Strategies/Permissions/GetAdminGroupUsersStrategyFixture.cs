using System.Collections.Generic;
using System.Net.Http;
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
		public void Get_WithMissingGroup()
		{
			var exception = Assert.Throws<HttpRequestException>(() =>
				Sut.Get(MissingId));

			exception.Message.Should().StartWith($"GroupID {MissingId} is invalid.");
		}

		[Test]
		public void Get_WithNoUsers_ById()
		{
			var result = Sut.Get(_groupWithNoUsers.ArtifactID);

			result.Should().BeEmpty();
		}

		[Test]
		public void Get_WithNoUsers_ByName()
		{
			var result = Sut.Get(_groupWithNoUsers.Name);

			result.Should().BeEmpty();
		}

		[Test]
		public void Get_WithSingleUser_ById()
		{
			var result = Sut.Get(_groupWithUser.ArtifactID);

			TestIfUsersListIsAsExpected(result);
		}

		[Test]
		public void Get_WithSingleUser_ByName()
		{
			var result = Sut.Get(_groupWithUser.Name);

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
