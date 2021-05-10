using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the service that provides a pool of users.
	/// </summary>
	public interface IAccountPoolService
	{
		/// <summary>
		/// Gets or sets a value indicating whether the standard user will be removed.
		/// The default value is <c>falce</c>.
		/// </summary>
		bool CleanUp { get; set; }

		/// <summary>
		/// Gets or sets the standard account email format.
		/// The default value is <c>"atuser{0}@mail.com"</c>.
		/// </summary>
		string StandardAccountEmailFormat { get; set; }

		/// <summary>
		/// Gets or sets the standard account first name format.
		/// The default value is <c>"AT {0}"</c>.
		/// </summary>
		string StandardAccountFirstNameFormat { get; set; }

		/// <summary>
		/// Gets or sets the standard account last name format.
		/// The default value is <c>"User"</c>.
		/// </summary>
		string StandardAccountLastNameFormat { get; set; }

		/// <summary>
		/// Gets or sets the standard account password.
		/// </summary>
		string StandardAccountPassword { get; set; }

		/// <summary>
		/// Gets or sets the standard account type.
		/// </summary>
		string StandardAccountType { get; set; }

		/// <summary>
		/// Gets or sets the standard account <see cref="Client"/>.
		/// </summary>
		Client StandardAccountClient { get; set; }

		/// <summary>
		/// Gets or sets the type of the standard account default selected file type.
		/// </summary>
		UserDefaultSelectedFileType StandardAccountDefaultSelectedFileType { get; set; }

		/// <summary>
		/// Gets or sets the standard account document viewer.
		/// </summary>
		UserDocumentViewer StandardAccountDocumentViewer { get; set; }

		/// <summary>
		/// Gets or sets the standard account group names.
		/// </summary>
		List<string> StandardAccountGroupNames { get; set; }

		/// <summary>
		/// Acquires the standard account.
		/// </summary>
		/// <returns>The account base information.</returns>
		AccountBaseInfo AcquireStandardAccount();

		/// <summary>
		/// Creates new standard account.
		/// If it exists prior to method invocation, deletes it and creates new one.
		/// </summary>
		/// <returns>The account base information.</returns>
		AccountBaseInfo DeleteAndAcquireStandardAccount();

		/// <summary>
		/// Gets the standard account.
		/// </summary>
		/// <param name="email">The user email.</param>
		/// <returns>The account base information.</returns>
		AccountBaseInfo GetStandardAccount(string email);

		/// <summary>
		/// Determines whether there is a standard account with the specified email.
		/// </summary>
		/// <param name="email">The user email.</param>
		/// <returns>
		///   <c>true</c> if there is a standard account with the specified email]; otherwise, <c>false</c>.
		/// </returns>
		bool IsStandardAccount(string email);

		/// <summary>
		/// Prepares (creates if there are no existing) the specified count of standard accounts.
		/// </summary>
		/// <param name="count">The count of accounts.</param>
		void PrepareStandardAccounts(int count);

		/// <summary>
		/// Releases the account.
		/// </summary>
		/// <param name="email">The user email.</param>
		void ReleaseAccount(string email);
	}
}
