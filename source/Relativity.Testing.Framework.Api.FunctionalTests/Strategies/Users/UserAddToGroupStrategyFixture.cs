using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(UserAddToGroupStrategy))]
	internal class UserAddToGroupStrategyFixture : ApiServiceTestFixture<IUserAddToGroupStrategy>
	{
		private const int MissingId = 999_999_999;

		public UserAddToGroupStrategyFixture()
		{
		}

		public UserAddToGroupStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void AddToGroup_MissingUser()
		{
			Group group = null;

			Arrange(x => x.Create(out group));

			var exception = Assert.Throws<ObjectNotFoundException>(() =>
				Sut.AddToGroup(MissingId, group.ArtifactID));

			exception.Message.Should().Be($"Failed to find User entity by {MissingId} ID.");
		}

		[Test]
		public void AddToGroup_MissingGroup()
		{
			User user = null;

			Arrange(x => x.Create(out user));

			var exception = Assert.Throws<ObjectNotFoundException>(() =>
				Sut.AddToGroup(user.ArtifactID, MissingId));

			exception.Message.Should().Be($"Failed to find Group entity by {MissingId} ID.");
		}

		[Test]
		public void AddToGroup_Existing()
		{
			User user = null;
			Group group = null;

			Arrange(x => x
				.Create(out user)
				.Create(out group));

			Sut.AddToGroup(user.ArtifactID, group.ArtifactID);

			var gotUser = Facade.Resolve<IUserGetByEmailStrategy>().Get(user.EmailAddress);

			gotUser.Groups.Should().Contain(x => x.ArtifactID == group.ArtifactID);
		}
	}
}
