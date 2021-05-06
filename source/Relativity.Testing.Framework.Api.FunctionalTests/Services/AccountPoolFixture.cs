using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[TestOf(typeof(AccountPoolService))]
	[NonParallelizable]
	public class AccountPoolFixture : ApiTestFixture
	{
		private readonly List<AccountBaseInfo> _users = new List<AccountBaseInfo>();
		private int _createdUsersCounter;
		private IAccountPoolService _accountPoolService;
		private IUserService _userService;

		public AccountPoolFixture()
		{
		}

		public AccountPoolFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_accountPoolService = Facade.Resolve<IAccountPoolService>();
			_userService = Facade.Resolve<IUserService>();
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_accountPoolService.CleanUp = false;
			_accountPoolService.StandardAccountClient = null;
		}

		protected override void OnTearDownFixture()
		{
			foreach (AccountBaseInfo userInfo in _users)
			{
				User user = _userService.GetByEmail(userInfo.Email);
				_userService.Delete(user.ArtifactID);
			}

			base.OnTearDownFixture();
		}

		[Test]
		public void Create_WithDefaults_CreatesUser()
		{
			// Email needs to be unique, otherwise this is just going to grab an existing one.
			string randomString = Randomizer.GetString();
			_accountPoolService.StandardAccountEmailFormat = randomString + "{0}@test.com";

			AccountBaseInfo createdUser = _accountPoolService.AcquireStandardAccount();
			_users.Add(createdUser);
			_createdUsersCounter++;

			createdUser.Should().NotBeNull();
		}

		[Test]
		public void Create_WithSpecifiedType_ShouldUseThatType()
		{
			string randomString = Randomizer.GetString();

			_accountPoolService.StandardAccountType = "External";
			_accountPoolService.StandardAccountEmailFormat = randomString + "{0}@test.com";
			_accountPoolService.StandardAccountFirstNameFormat = randomString + "{0}";

			AccountBaseInfo createdUser = _accountPoolService.AcquireStandardAccount();
			_users.Add(createdUser);
			_createdUsersCounter++;

			User retrievedUser = _userService.GetByEmail(createdUser.Email);

			retrievedUser.Type.Should().Be(_accountPoolService.StandardAccountType);
		}

		[Test]
		public void Create_WithSpecifiedClient_ShouldUseThatClient()
		{
			string randomString = Randomizer.GetString();

			Client client = null;

			Arrange(x => x.Create(out client));

			_accountPoolService.StandardAccountClient = client;
			_accountPoolService.StandardAccountEmailFormat = randomString + "{0}@test.com";
			_accountPoolService.StandardAccountFirstNameFormat = randomString + "{0}";

			AccountBaseInfo createdUser = _accountPoolService.AcquireStandardAccount();
			_users.Add(createdUser);
			_createdUsersCounter++;

			User retrievedUser = _userService.GetByEmail(createdUser.Email);

			retrievedUser.Client.ArtifactID.Should().Be(client.ArtifactID);
		}

		[Test]
		public void Create_WithCleanUp_True()
		{
			string randomString = Randomizer.GetString();
			_accountPoolService.StandardAccountEmailFormat = randomString + "{0}@test.com";
			_accountPoolService.CleanUp = true;

			AccountBaseInfo createdUser = _accountPoolService.AcquireStandardAccount();
			_createdUsersCounter++;

			createdUser.Should().NotBeNull();
			Session.Dispose();

			var result = _userService.GetByEmail(createdUser.Email);
			result.Should().BeNull();
		}

		[Test]
		public void Recreate_ShouldCreateNewUser()
		{
			var recreatedUserExpectedEmail = string.Format(_accountPoolService.StandardAccountEmailFormat, _createdUsersCounter + 1);

			AccountBaseInfo recreatedUser = _accountPoolService.DeleteAndAcquireStandardAccount();
			_users.Add(recreatedUser);
			_createdUsersCounter++;

			recreatedUser.Email.Should().Be(recreatedUserExpectedEmail);
		}

		[Test]
		public void Recreate_WhenUserExists_ShouldDeleteAndCreateNewUser()
		{
			int userNumber = _createdUsersCounter + 1;

			User user = new User
			{
				EmailAddress = string.Format(_accountPoolService.StandardAccountEmailFormat, userNumber),
				FirstName = string.Format(_accountPoolService.StandardAccountFirstNameFormat, userNumber),
				LastName = string.Format(_accountPoolService.StandardAccountLastNameFormat, userNumber),
				Password = _accountPoolService.StandardAccountPassword,
				DefaultSelectedFileType = _accountPoolService.StandardAccountDefaultSelectedFileType,
				DocumentViewer = _accountPoolService.StandardAccountDocumentViewer,
				Type = _accountPoolService.StandardAccountType,
				Client = _accountPoolService.StandardAccountClient
			};

			var createdUser = _userService.Create(user);

			AccountBaseInfo recreatedUser = _accountPoolService.DeleteAndAcquireStandardAccount();
			_users.Add(recreatedUser);
			_createdUsersCounter++;

			var recreatedUserId = _userService.GetByEmail(recreatedUser.Email).ArtifactID;
			recreatedUserId.Should().NotBe(createdUser.ArtifactID);
		}
	}
}
