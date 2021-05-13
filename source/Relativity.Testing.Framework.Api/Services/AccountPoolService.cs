using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Logging;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Session;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class AccountPoolService : IAccountPoolService
	{
		private readonly ILogService _logService;
		private readonly ICreateStrategy<User> _userCreateStrategy;
		private readonly IUserExistsByEmailStrategy _userExistsByEmailStrategy;
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;
		private readonly IDeleteByIdStrategy<User> _userDeleteByIdStrategy;
		private readonly IGetAllByNamesStrategy<Group> _groupGetAllByNamesStrategy;
		private readonly IObjectService _objectService;
		private static readonly List<AccountEntry> _standardAccountEntries = new List<AccountEntry>();
		private static readonly object _acquireLock = new object();

		public AccountPoolService(
			ILogService logService,
			ICreateStrategy<User> userCreateStrategy,
			IUserExistsByEmailStrategy userExistsByEmailStrategy,
			IUserGetByEmailStrategy userGetByEmailStrategy,
			IDeleteByIdStrategy<User> userDeleteByIdStrategy,
			IGetAllByNamesStrategy<Group> groupGetAllByNamesStrategy,
			IObjectService objectService)
		{
			_logService = logService;
			_userCreateStrategy = userCreateStrategy;
			_userExistsByEmailStrategy = userExistsByEmailStrategy;
			_userGetByEmailStrategy = userGetByEmailStrategy;
			_userDeleteByIdStrategy = userDeleteByIdStrategy;
			_groupGetAllByNamesStrategy = groupGetAllByNamesStrategy;
			_objectService = objectService;
		}

		public string StandardAccountEmailFormat { get; set; } = "atuser{0}@mail.com";

		public string StandardAccountFirstNameFormat { get; set; } = "AT {0}";

		public string StandardAccountLastNameFormat { get; set; } = "User";

		public string StandardAccountPassword { get; set; } = "Test1234!";

		public string StandardAccountType { get; set; } = "Internal";

		public Client StandardAccountClient { get; set; }

		public UserDefaultSelectedFileType StandardAccountDefaultSelectedFileType { get; set; }

		public UserDocumentViewer StandardAccountDocumentViewer { get; set; }

		public bool CleanUp { get; set; }

		public List<string> StandardAccountGroupNames { get; set; } = new List<string>
		{
			GroupNames.SystemAdministrators,
			GroupNames.DomainUsers,
			GroupNames.Everyone
		};

		public AccountBaseInfo DeleteAndAcquireStandardAccount()
		{
			lock (_acquireLock)
			{
				AccountBaseInfo accountInfo = GetNewStandardAccountInfo();
				DeleteStandardAccount(accountInfo.Email);

				CreateNewUser(accountInfo);

				AccountEntry accountEntry = new AccountEntry(accountInfo) { IsAcquired = true };
				_standardAccountEntries.Add(accountEntry);

				_logService.Trace($"Acquired {accountEntry.Info.Email} standard account");

				return accountInfo;
			}
		}

		private void DeleteStandardAccount(string email)
		{
			var existingUser = _userGetByEmailStrategy.Get(email);

			if (existingUser != null)
			{
				_standardAccountEntries.RemoveAll(account => account.Info.Email == email);
				_userDeleteByIdStrategy.Delete(existingUser.ArtifactID);
				_logService.Trace($"Removed {email} standard account");
			}
		}

		public AccountBaseInfo AcquireStandardAccount()
		{
			lock (_acquireLock)
			{
				AccountEntry entry = _standardAccountEntries.FirstOrDefault(x => !x.IsAcquired);

				if (entry == null)
				{
					entry = GetOrCreateStandardAccount();
				}

				entry.IsAcquired = true;

				_logService.Trace($"Acquired {entry.Info.Email} standard account");

				return entry.Info;
			}
		}

		public void PrepareStandardAccounts(int count)
		{
			for (int i = 0; i < count; i++)
			{
				GetOrCreateStandardAccount();
			}
		}

		private AccountEntry GetOrCreateStandardAccount()
		{
			AccountBaseInfo accountInfo = GetNewStandardAccountInfo();

			_logService.Trace($"Starting: Check {accountInfo.Email} user exists");

			bool existsUser = _userExistsByEmailStrategy.Exists(accountInfo.Email);

			_logService.Trace($"Finished: Check {accountInfo.Email} user exists: {existsUser}");

			if (!existsUser)
			{
				CreateNewUser(accountInfo);
			}

			AccountEntry accountEntry = new AccountEntry(accountInfo);

			_standardAccountEntries.Add(accountEntry);

			return accountEntry;
		}

		private AccountBaseInfo GetNewStandardAccountInfo()
		{
			int number = _standardAccountEntries.Count + 1;

			AccountBaseInfo accountInfo = new AccountBaseInfo
			{
				Email = string.Format(StandardAccountEmailFormat, number),
				FirstName = string.Format(StandardAccountFirstNameFormat, number),
				LastName = string.Format(StandardAccountLastNameFormat, number),
				Password = StandardAccountPassword
			};
			return accountInfo;
		}

		private void CreateNewUser(AccountBaseInfo accountInfo)
		{
			var groups = _groupGetAllByNamesStrategy.GetAll(StandardAccountGroupNames);
			StandardAccountClient = StandardAccountClient ?? _objectService.GetAll<Client>().FirstOrDefault();

			User user = new User
			{
				EmailAddress = accountInfo.Email,
				FirstName = accountInfo.FirstName,
				LastName = accountInfo.LastName,
				Password = accountInfo.Password,
				DefaultSelectedFileType = StandardAccountDefaultSelectedFileType,
				DocumentViewer = StandardAccountDocumentViewer,
				Groups = groups.Cast<Artifact>().ToList(),
				Type = StandardAccountType,
				Client = StandardAccountClient
			};

			_logService.Trace($"Starting: Create {accountInfo.Email} user");

			user = _userCreateStrategy.Create(user);

			if (!CleanUp)
			{
				TestSession.Current.SetCleanUp(user, false);
				TestSession.Global.Add(user);
			}

			_logService.Trace($"Finished: Create {accountInfo.Email} user");
		}

		public void ReleaseAccount(string email)
		{
			AccountEntry entry = _standardAccountEntries.FirstOrDefault(x => x.Info.Email == email);

			if (entry != null)
			{
				entry.IsAcquired = false;
				_logService.Trace($"Released {entry.Info.Email} standard account");
			}
		}

		public bool IsStandardAccount(string email)
		{
			return _standardAccountEntries.Any(x => x.Info.Email == email);
		}

		public AccountBaseInfo GetStandardAccount(string email)
		{
			return _standardAccountEntries.FirstOrDefault(x => x.Info.Email == email)?.Info;
		}

		private class AccountEntry
		{
			public AccountEntry(AccountBaseInfo accountInfo)
			{
				Info = accountInfo;
			}

			public AccountBaseInfo Info { get; set; }

			public bool IsAcquired { get; set; }
		}
	}
}
