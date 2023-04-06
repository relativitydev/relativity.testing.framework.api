using System.Collections.Generic;
using System.Net.Http;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(UserGetGroupsStrategy))]
	internal class UserGetGroupsStrategyFixture : ApiServiceTestFixture<IUserGetGroupsStrategy>
	{
		[Test]
		public void GetGroups_ExistingUser()
		{
			User user = null;

			Arrange(x => x.Create(out user));

			IList<NamedArtifact> result = Sut.GetGroups(user.ArtifactID);

			Assert.IsNotEmpty(result);
		}

		[Test]
		public void GetGroups_MissingUser_Throws()
		{
			Assert.Throws<HttpRequestException>(() => Sut.GetGroups(int.MaxValue));
		}

		[Test]
		public void GetGroupsByGroupId_OnlyReturnsListedGroup()
		{
			User user = null;
			Group group = null;

			Arrange(x => x
				.Create(out user)
				.Create(out group));

			Facade.Resolve<IUserAddToGroupStrategy>().AddToGroup(user.ArtifactID, group.ArtifactID);

			IList<NamedArtifact> result = Sut.GetGroupsByGroupId(user.ArtifactID, group.ArtifactID);

			Assert.That(result, Has.Exactly(1).Items, "Count of groups incorrect");
			Assert.That(result[0], Has.Property("ArtifactID").EqualTo(group.ArtifactID), "Group artifact ID incorrect");
		}

		[Test]
		public void GetGroupsByGroupId_UserNotMemberOfGroup()
		{
			User user = null;
			Group group = null;

			Arrange(x => x
				.Create(out user)
				.Create(out group));

			IList<NamedArtifact> result = Sut.GetGroupsByGroupId(user.ArtifactID, group.ArtifactID);

			Assert.That(result, Is.Empty, "List of groups should be empty");
		}
	}
}
