using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the service that provides a pool of users and default properties for them.
	/// </summary>
	/// <example>
	/// <code>
	/// private readonly IAccountPoolService _accountPoolService;
	/// ...
	/// _accountPoolService = relativityFacade.Resolve&#60;IAccountPoolService&#62;();
	/// </code>
	/// </example>
	public interface IAccountPoolService
	{
		/// <summary>
		/// <para>Gets or sets a value indicating whether the pooled accounts will be cleaned up when no longer in the current session.</para>
		/// <para>The default value is <c>false</c>.</para>
		/// </summary>
		/// <value>
		/// By default this is set to <c>false</c>, but a value of <c>true</c> will cause the TestSession to clean up the pooled accounts when no longer in scope.
		/// </value>
		/// <example>
		/// <code>
		/// _accountPoolService.CleanUp = true;
		/// </code>
		/// </example>
		bool CleanUp { get; set; }

		/// <summary>
		/// <para>Gets or sets the standard account email format.</para>
		/// <para>The default value is <c>"atuser{0}@mail.com"</c>.</para>
		/// </summary>
		/// <value>
		/// A string format to use for the email address of pooled users.
		/// </value>
		/// <example>
		/// <code>
		/// _accountPoolService.StandardAccountEmailFormat = $"MyCoolRAP_{{0}}_svc@test.com";
		/// </code>
		/// </example>
		string StandardAccountEmailFormat { get; set; }

		/// <summary>
		/// <para>Gets or sets the standard account first name format.</para>
		/// <para>The default value is <c>"AT {0}"</c>.</para>
		/// </summary>
		/// <value>
		/// A string format to use for the first name of pooled users.
		/// </value>
		/// <example>
		/// <code>
		/// _accountPoolService.StandardAccountFirstNameFormat = $"MyCoolRAP {{0}}";
		/// </code>
		/// </example>
		string StandardAccountFirstNameFormat { get; set; }

		/// <summary>
		/// <para>Gets or sets the standard account last name format.</para>
		/// <para>The default value is <c>"User"</c>.</para>
		/// </summary>
		/// <value>
		/// A string format to use for the last name of pooled users.
		/// </value>
		/// <example>
		/// <code>
		/// _accountPoolService.StandardAccountLastNameFormat = "ASpecialLastName";
		/// </code>
		/// </example>
		string StandardAccountLastNameFormat { get; set; }

		/// <summary>
		/// Gets or sets the standard account password.
		/// </summary>
		/// <value>
		/// A string to use as the password of the pooled users.
		/// </value>
		/// <example>
		/// <code>
		/// _accountPoolService.StandardAccountPassword = Randomizer.GetString("AT1_!@#");
		/// </code>
		/// </example>
		string StandardAccountPassword { get; set; }

		/// <summary>
		/// <para>Gets or sets the standard account type.</para>
		/// <para>The default value is <c>"Internal"</c>.</para>
		/// </summary>
		/// <value>
		/// A type to use for the pooled users.
		/// </value>
		/// <example>
		/// <code>
		/// _accountPoolService.StandardAccountType = "External";
		/// </code>
		/// </example>
		string StandardAccountType { get; set; }

		/// <summary>
		/// Gets or sets the standard account [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html).
		/// </summary>
		/// <value>
		/// A [Client](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Client.html) to use for the pooled users.
		/// </value>
		/// <example>
		/// <code>
		/// _accountPoolService.StandardAccountType = SomeOtherClient;
		/// </code>
		/// </example>
		Client StandardAccountClient { get; set; }

		/// <summary>
		/// Gets or sets the type of the standard account <see cref="UserDefaultSelectedFileType"/>.
		/// </summary>
		/// <value>
		/// A <see cref="UserDefaultSelectedFileType"/> to use for the pooled users.
		/// </value>
		/// <example>
		/// <code>
		/// _accountPoolService.StandardAccountDefaultSelectedFileType = UserDefaultSelectedFileType.Viewer;
		/// </code>
		/// </example>
		UserDefaultSelectedFileType StandardAccountDefaultSelectedFileType { get; set; }

		/// <summary>
		/// Gets or sets the standard account <see cref="UserDocumentViewer"/>.
		/// </summary>
		/// <value>
		/// A <see cref="UserDocumentViewer"/> to use for the pooled users.
		/// </value>
		/// <example>
		/// <code>
		/// _accountPoolService.StandardAccountDocumentViewer = UserDocumentViewer.Default;
		/// </code>
		/// </example>
		UserDocumentViewer StandardAccountDocumentViewer { get; set; }

		/// <summary>
		/// Gets or sets the list of groups that the standard accounts are a part of.
		/// <para>The default groups are <see cref="GroupNames.SystemAdministrators"/>, <see cref="GroupNames.DomainUsers"/>, and <see cref="GroupNames.Everyone"/>.</para>
		/// </summary>
		/// <value>
		/// A list of group names for the pooled users to be added to.
		/// </value>
		/// <example>
		/// <code>
		/// Group aeroGroup = _groupService.Require(name, aeroClient);
		/// List&#60;string&#62; groups = { "Everyone", aeroGroup.Name }
		/// _accountPoolService.StandardAccountGroupNames = groups;
		/// </code>
		/// </example>
		List<string> StandardAccountGroupNames { get; set; }

		/// <summary>
		/// <para>Acquires a user from the account pool.</para>
		/// <para>Will create a user if there are none left in the account pool.</para>
		/// <para>Users are created based on the StandardAccount properties that have been set.</para>
		/// </summary>
		/// <returns>The <see cref="AccountBaseInfo"/>.</returns>
		/// <example>
		/// <code>
		/// AccountBaseInfo user1 = _accountPoolService.AcquireStandardAccount();
		/// AccountBaseInfo user2 = _accountPoolService.AcquireStandardAccount();
		/// </code>
		/// </example>
		AccountBaseInfo AcquireStandardAccount();

		/// <summary>
		/// <para>Acquires a user from the account pool, but will forcibly delete and recreate it first.</para>
		/// <para>Will create a user if there are none left in the account pool.</para>
		/// <para>This should be used over <see cref="AcquireStandardAccount"/> when the account might already exist, and you do not know what the password for it is.</para>
		/// </summary>
		/// <returns>The <see cref="AccountBaseInfo"/>.</returns>
		/// <example>
		/// <code>
		/// AccountBaseInfo user1 = _accountPoolService.DeleteAndAcquireStandardAccount();
		/// AccountBaseInfo user2 = _accountPoolService.DeleteAndAcquireStandardAccount();
		/// </code>
		/// </example>
		AccountBaseInfo DeleteAndAcquireStandardAccount();

		/// <summary>
		/// Gets a user from the account pool.
		/// </summary>
		/// <param name="email">The user email.</param>
		/// <returns>The <see cref="AccountBaseInfo"/> or <c>null</c> if there are no accounts left in the pool.</returns>
		/// <example>
		/// <code>
		/// _accountPoolService.PrepareStandardAccounts(1);
		/// AccountBaseInfo user1 = _accountPoolService.GetStandardAccount(); // Returns a user.
		/// AccountBaseInfo user2 = _accountPoolService.GetStandardAccount(); // Returns null.
		/// </code>
		/// </example>
		AccountBaseInfo GetStandardAccount(string email);

		/// <summary>
		/// Determines whether there is a standard account with the specified email.
		/// </summary>
		/// <param name="email">The user email.</param>
		/// <returns><c>true</c> if there is a standard account with the specified email otherwise, <c>false</c>.</returns>
		/// <example>
		/// <code>
		/// _accountPoolService.StandardAccountEmailFormat = $"MyCoolRAP_{{0}}_svc@test.com";
		/// _accountPoolService.PrepareStandardAccounts(1);
		/// _accountPoolService.IsStandardAccount("MyCoolRAP_1_svc@test.com"); // Returns true.
		/// _accountPoolService.IsStandardAccount("MyCoolRAP_2_svc@test.com"); // Returns false.
		/// </code>
		/// </example>
		bool IsStandardAccount(string email);

		/// <summary>
		/// <para>Prepares (creates if there are no existing) the specified count of standard accounts.</para>
		/// <para>Users are created based on the StandardAccount properties that have been set.</para>
		/// </summary>
		/// <param name="count">The count of accounts.</param>
		/// <example>
		/// <code>
		/// _accountPoolService.PrepareStandardAccounts(2);
		/// </code>
		/// </example>
		void PrepareStandardAccounts(int count);

		/// <summary>
		/// Releases the account, allowing it to be acquired at a later time.
		/// </summary>
		/// <param name="email">The user email.</param>
		/// <example>
		/// <code>
		/// _accountPoolService.StandardAccountEmailFormat = $"MyCoolRAP_{{0}}_svc@test.com";
		/// AccountBaseInfo user1 = _accountPoolService.AcquireStandardAccount(); // Creates and returns "MyCoolRAP_1_svc@test.com"
		/// AccountBaseInfo user2 = _accountPoolService.AcquireStandardAccount(); // Creates and returns "MyCoolRAP_2_svc@test.com"
		/// _accountPoolService.ReleaseAccount("MyCoolRAP_1_svc@test.com");
		/// user1 = _accountPoolService.AcquireStandardAccount(); // Returns the already created and previously released "MyCoolRAP_1_svc@test.com"
		/// </code>
		/// </example>
		void ReleaseAccount(string email);
	}
}
