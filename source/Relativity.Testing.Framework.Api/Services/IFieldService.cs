using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the field API service.
	/// </summary>
	public interface IFieldService
	{
		/// <summary>
		/// Creates the specified field.
		/// </summary>
		/// <typeparam name="TFieldModel">The field type.</typeparam>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		TFieldModel Create<TFieldModel>(int workspaceId, TFieldModel entity)
			where TFieldModel : Field;

		/// <summary>
		/// Deletes the field by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the field.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the field by the specified ID.
		/// Returns only base parameters of field.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the field.</param>
		/// <returns>The <see cref="Field"/> entity or <see langword="null"/>.</returns>
		Field Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the field of specified type by the specified ID.
		/// </summary>
		/// <typeparam name="TFieldModel">The field type.</typeparam>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the field.</param>
		/// <returns>The <see cref="Field"/> entity or <see langword="null"/>.</returns>
		TFieldModel Get<TFieldModel>(int workspaceId, int entityId)
			where TFieldModel : Field;

		/// <summary>
		/// Gets the field by the specified name.
		/// Returns only base parameters of field.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the object type.</param>
		/// <returns>The <see cref="Field"/> entity or <see langword="null"/>.</returns>
		Field Get(int workspaceId, string entityName);

		/// <summary>
		/// Gets the field of specified type by the specified name.
		/// </summary>
		/// <typeparam name="TFieldModel">The field type.</typeparam>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the field.</param>
		/// <returns>The <see cref="Field"/> entity or <see langword="null"/>.</returns>
		TFieldModel Get<TFieldModel>(int workspaceId, string entityName)
			where TFieldModel : Field;

		/// <summary>
		/// Updates the specified field.
		/// </summary>
		/// <typeparam name="TFieldModel">The field type.</typeparam>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		void Update<TFieldModel>(int workspaceId, TFieldModel entity)
			where TFieldModel : Field;

		/// <summary>
		/// Requires the specified field.
		/// </summary>
		/// <typeparam name="TFieldModel">The field type.</typeparam>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		/// /// <returns>The entity required.</returns>
		TFieldModel Require<TFieldModel>(int workspaceId, TFieldModel entity)
			where TFieldModel : Field;
	}
}
