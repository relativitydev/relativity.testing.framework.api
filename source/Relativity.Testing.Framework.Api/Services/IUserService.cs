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
		/// Creates the specified user.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// <code>
		/// User user = new User
		/// {
		/// 	FirtName = "User First Name",
		/// 	LastName = "User Last Name",
		/// 	EmailAddress = "test@email.com",
		/// 	Type = "External",
		/// 	Password = "TestPassword2345!",
		/// 	DefaultSelectedFileType = UserDefaultSelectedFileType.LongText,
		/// }
		/// User createdUser = _userService.Create(user);
		/// </code>
		/// </example>
		User Create(User entity);

		/// <summary>
		/// Requires the specified user.
		/// Returns existing object if the <paramref name="entity"/> has the properties (ArtifactID, Email) set to be able to get the entity;
		/// otherwise creates a new entity.
		/// </summary>
		/// <param name="entity">The entity to require.</param>
		/// <param name="ensureNew">The boolean value indicating whether we going to delete the same entity. By default true.</param>
		/// <returns>The entity required.</returns>
		/// <example>
		/// <code>
		/// User existingUser = new User
		/// {
		/// 	FirtName = "Existing User First Name",
		/// 	LastName = "Existing User Last Name",
		/// 	EmailAddress = "existing_user@email.com",
		/// 	ArtifactID = 1
		/// }
		/// User requiredUser = _userService.Require(user, false);
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// User userToCreate = new User
		/// {
		/// 	FirtName = "User First Name",
		/// 	LastName = "User Last Name",
		/// 	EmailAddress = "not_existing_user@email.com",
		/// 	Type = "External",
		/// 	Password = "TestPassword2345!",
		/// 	DefaultSelectedFileType = UserDefaultSelectedFileType.LongText,
		/// }
		/// User createdUser = _userService.Require(user);
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// User existingUser = new User
		/// {
		/// 	FirtName = "Existing User First Name",
		/// 	LastName = "Existing User Last Name",
		/// 	EmailAddress = "existing_user@email.com",
		/// 	ArtifactID = 1
		/// }
		/// User recreatedUser = _userService.Require(user);
		/// </code>
		/// </example>
		User Require(User entity, bool ensureNew = true);

		/// <summary>
		/// Deletes the user by ID.
		/// </summary>
		/// <param name="id">The artifact ID of the user.</param>
		/// <example>
		/// <code>
		/// int userID = 23236;
		/// _userService.Delete(userID);
		/// </code>
		/// </example>
		void Delete(int id);

		/// <summary>
		/// Gets the user by ID.
		/// </summary>
		/// <param name="id">The artifact ID of the user.</param>
		/// <returns>The <see cref="User"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int userID = 23236;
		/// User user =_userService.Get(userID);
		/// </code>
		/// </example>
		User Get(int id);

		/// <summary>
		/// Gets the user by the specified email address.
		/// </summary>
		/// <param name="email">The email address.</param>
		/// <returns>The <see cref="User"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// string userEmail = "some_user@email.com";
		/// User user =_userService.GetByEmail(userEmail);
		/// </code>
		/// </example>
		User GetByEmail(string email);

		/// <summary>
		/// Determines whether the user with the specified email address exists.
		/// </summary>
		/// <param name="email">The email address.</param>
		/// <returns><see langword="true"/> if a user exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// string userEmail = "some_user@email.com";
		/// bool userExists =_userService.ExistsByEmail(userEmail);
		/// </code>
		/// </example>
		bool ExistsByEmail(string email);

		/// <summary>
		/// Adds the user to the group.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <example>
		/// <code>
		/// int userID = 324546;
		/// int groupID = 78943;
		/// _userService.AddToGroup(userID, groupID);
		/// </code>
		/// </example>
		void AddToGroup(int userId, int groupId);

		/// <summary>
		/// Removes the user from the group.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <example>
		/// <code>
		/// int userID = 324546;
		/// int groupID = 78943;
		/// _userService.RemoveFromGroup(userID, groupID);
		/// </code>
		/// </example>
		void RemoveFromGroup(int userId, int groupId);

		/// <summary>
		/// Updates the specified user.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <example>
		/// <code>
		/// User existingUser = _userService.GetByEmail("some_existing_user@email.com");
		/// existingUser.Password = "newPassword123*";
		/// existingUser.ChangePasswordNextLogin = true;
		/// existingUser.RelativityAccess = false;
		/// _userService.Update(existingUser);
		/// </code>
		/// </example>
		void Update(User entity);
	}
}
