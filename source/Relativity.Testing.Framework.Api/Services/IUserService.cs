using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the user API service.
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Creates the specified user.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		User Create(User entity);

		/// <summary>
		/// Requires the specified user.
		/// Returns existing object if the <paramref name="entity"/> has the properties (ArtifactID, Email) set to be able to get the entity;
		/// otherwise creates a new entity.
		/// </summary>
		/// <param name="entity">The entity to require.</param>
		/// <param name="ensureNew">The boolean value indicating whether we going to delete the same entity. By default true.</param>
		/// <returns>The entity required.</returns>
		User Require(User entity, bool ensureNew = true);

		/// <summary>
		/// Deletes the user by ID.
		/// </summary>
		/// <param name="id">The artifact ID of the user.</param>
		void Delete(int id);

		/// <summary>
		/// Gets the user by ID.
		/// </summary>
		/// <param name="id">The artifact ID of the user.</param>
		/// <returns>The <see cref="User"/> entity or <see langword="null"/>.</returns>
		User Get(int id);

		/// <summary>
		/// Gets the user by the specified email address.
		/// </summary>
		/// <param name="email">The email address.</param>
		/// <returns>The <see cref="User"/> entity or <see langword="null"/>.</returns>
		User GetByEmail(string email);

		/// <summary>
		/// Determines whether the user with the specified email address exists.
		/// </summary>
		/// <param name="email">The email address.</param>
		/// <returns><see langword="true"/> if a user exists; otherwise, <see langword="false"/>.</returns>
		bool ExistsByEmail(string email);

		/// <summary>
		/// Adds the user to the group.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		/// <param name="groupId">The group ID.</param>
		void AddToGroup(int userId, int groupId);

		/// <summary>
		/// Removes the user from the group.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		/// <param name="groupId">The group ID.</param>
		void RemoveFromGroup(int userId, int groupId);

		/// <summary>
		/// Updates the specified user.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		void Update(User entity);
	}
}
