using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the user API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _userService = relativityFacade.Resolve&lt;IUserService&gt;();
	/// </code>
	/// </example>
	public interface IUserService
	{
		/// <summary>
		/// Creates the specified <see cref="User"/>.
		/// </summary>
		/// <param name="user">The <see cref="User"/> to create.</param>
		/// <returns>The created <see cref="User"/>.</returns>
		/// <example>
		/// <code>
		/// User user = new User
		/// {
		/// 	FirstName = "User First Name",
		/// 	LastName = "User Last Name",
		/// 	EmailAddress = "test@email.com",
		/// 	Type = "External",
		/// 	Password = "TestPassword2345!",
		/// 	DefaultSelectedFileType = UserDefaultSelectedFileType.LongText,
		/// }
		/// User createdUser = _userService.Create(user);
		/// </code>
		/// </example>
		User Create(User user);

		/// <summary>
		/// Requires the specified <see cref="User"/>.
		/// Returns existing <see cref="User"/> if the <paramref name="user"/> has the properties (ArtifactID, Email) set to be able to get the user;
		/// otherwise creates a new user.
		/// </summary>
		/// <param name="user">The <see cref="User"/> to require.</param>
		/// <param name="ensureNew">The boolean value indicating whether we going to delete the same user. By default true.</param>
		/// <returns>The <see cref="User"/> required.</returns>
		/// <example> This shows how to require existing user and get it without recreating it.
		/// <code>
		/// User existingUser = new User
		/// {
		/// 	FirstName = "Existing User First Name",
		/// 	LastName = "Existing User Last Name",
		/// 	EmailAddress = "existing_user@email.com",
		/// 	ArtifactID = 1
		/// }
		/// User requiredUser = _userService.Require(user, false);
		/// </code>
		/// </example>
		/// <example> This shows how to create a new user from given model and get it using Require method.
		/// <code>
		/// User userToCreate = new User
		/// {
		/// 	FirstName = "User First Name",
		/// 	LastName = "User Last Name",
		/// 	EmailAddress = "not_existing_user@email.com",
		/// 	Type = "External",
		/// 	Password = "TestPassword2345!",
		/// 	DefaultSelectedFileType = UserDefaultSelectedFileType.LongText,
		/// }
		/// User createdUser = _userService.Require(user);
		/// </code>
		/// </example>
		/// <example> This shows how to recreate existing user and get it using Require method.
		/// <code>
		/// User existingUser = new User
		/// {
		/// 	FirstName = "Existing User First Name",
		/// 	LastName = "Existing User Last Name",
		/// 	EmailAddress = "existing_user@email.com",
		/// 	ArtifactID = 1
		/// }
		/// User recreatedUser = _userService.Require(user);
		/// </code>
		/// </example>
		User Require(User user, bool ensureNew = true);

		/// <summary>
		/// Deletes the <see cref="User"/> by ArtifactID.
		/// </summary>
		/// <param name="artifactID">The ArtifactID of the user.</param>
		/// <example>
		/// <code>
		/// int userID = 23236;
		/// _userService.Delete(userID);
		/// </code>
		/// </example>
		void Delete(int artifactID);

		/// <summary>
		/// Gets the <see cref="User"/> by ArtifactID.
		/// </summary>
		/// <param name="artifactID">The ArtifactID of the user.</param>
		/// <returns>The <see cref="User"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int userID = 23236;
		/// User user =_userService.Get(userID);
		/// </code>
		/// </example>
		User Get(int artifactID);

		/// <summary>
		/// Gets the <see cref="User"/> by the specified email address.
		/// </summary>
		/// <param name="email">The email address of the user.</param>
		/// <returns>The <see cref="User"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// string userEmail = "some_user@email.com";
		/// User user =_userService.GetByEmail(userEmail);
		/// </code>
		/// </example>
		User GetByEmail(string email);

		/// <summary>
		/// Determines whether the <see cref="User"/> with the specified email address exists.
		/// </summary>
		/// <param name="email">The email address of the user.</param>
		/// <returns><see langword="true"/> if a user exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// string userEmail = "some_user@email.com";
		/// bool userExists =_userService.ExistsByEmail(userEmail);
		/// </code>
		/// </example>
		bool ExistsByEmail(string email);

		/// <summary>
		/// Adds the <see cref="User"/> to the group.
		/// </summary>
		/// <param name="userArtifactID">The user ArtifactID.</param>
		/// <param name="groupArtifactID">The group ArtifactID.</param>
		/// <example>
		/// <code>
		/// int userID = 324546;
		/// int groupID = 78943;
		/// _userService.AddToGroup(userID, groupID);
		/// </code>
		/// </example>
		void AddToGroup(int userArtifactID, int groupArtifactID);

		/// <summary>
		/// Removes the <see cref="User"/> from the group.
		/// </summary>
		/// <param name="userArtifactID">The user ArtifactID.</param>
		/// <param name="groupArtifactID">The group ArtifactID.</param>
		/// <example>
		/// <code>
		/// int userID = 324546;
		/// int groupID = 78943;
		/// _userService.RemoveFromGroup(userID, groupID);
		/// </code>
		/// </example>
		void RemoveFromGroup(int userArtifactID, int groupArtifactID);

		/// <summary>
		/// Updates the specified <see cref="User"/>.
		/// </summary>
		/// <param name="user">The <see cref="User"/> to update.</param>
		/// <returns>The updated <see cref="User"/>.</returns>
		/// <example>
		/// <code>
		/// User existingUser = _userService.GetByEmail("some_existing_user@email.com");
		/// existingUser.Password = "newPassword123*";
		/// existingUser.ChangePasswordNextLogin = true;
		/// existingUser.RelativityAccess = false;
		/// _userService.Update(existingUser);
		/// </code>
		/// </example>
		User Update(User user);
	}
}
