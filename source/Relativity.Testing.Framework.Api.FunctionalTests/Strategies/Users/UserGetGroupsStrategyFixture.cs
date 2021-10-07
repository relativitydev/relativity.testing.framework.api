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

			var result = Sut.GetGroups(user.ArtifactID);

			Assert.IsNotEmpty(result);
		}

		[Test]
		public void GetGroups_MissingUser_Throws()
		{
			Assert.Throws<HttpRequestException>(() => Sut.GetGroups(int.MaxValue));
		}
	}
}
