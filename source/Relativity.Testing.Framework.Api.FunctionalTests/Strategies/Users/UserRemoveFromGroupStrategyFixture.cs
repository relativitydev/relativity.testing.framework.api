using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Arrangement;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(UserRemoveFromGroupStrategy))]
	internal class UserRemoveFromGroupStrategyFixture : ApiServiceTestFixture<IUserRemoveFromGroupStrategy>
	{
		private const int MissingId = 999_999_999;

		[Test]
		public void RemoveFromGroup_MissingUser()
		{
			Group group = null;

			Arrange(x => x.Create(out group));

			var exception = Assert.Throws<ObjectNotFoundException>(() =>
				Sut.RemoveFromGroup(MissingId, group.ArtifactID));

			exception.Message.Should().Be($"Failed to find User entity by {MissingId} ID.");
		}

		[Test]
		public void RemoveFromGroup_MissingGroup()
		{
			User user = null;

			Arrange(x => x.Create(out user));

			var exception = Assert.Throws<ObjectNotFoundException>(() =>
				Sut.RemoveFromGroup(user.ArtifactID, MissingId));

			exception.Message.Should().Be($"Failed to find Group entity by {MissingId} ID.");
		}

		[Test]
		public void RemoveFromGroup_Existing()
		{
			User user = null;
			Group group = null;

			Arrange(x => x
				.Create(out user)
				.Create(out group)
				.Add(user));

			Sut.RemoveFromGroup(user.ArtifactID, group.ArtifactID);

			var gotUser = Facade.Resolve<IUserGetByEmailStrategy>().Get(user.EmailAddress);

			gotUser.Groups.Should().NotContain(x => x.ArtifactID == group.ArtifactID);
		}
	}
}
