using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the object type API service.
	/// </summary>
	public interface IObjectTypeService
	{
		/// <summary>
		/// Creates the specified object type.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new object type,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		ObjectType Create(int workspaceId, ObjectType entity);

		/// <summary>
		/// Requires the specified object type.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> have a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require object type,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		ObjectType Require(int workspaceId, ObjectType entity);

		/// <summary>
		/// Deletes the object type by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the object type,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the object type.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the object type by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the object type,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the object type.</param>
		/// <returns>>The <see cref="ObjectType"/> entity or <see langword="null"/>.</returns>
		ObjectType Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the object type by the specified name.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the object type,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the object type.</param>
		/// <returns>>The <see cref="ObjectType"/> entity or <see langword="null"/>.</returns>
		ObjectType Get(int workspaceId, string entityName);

		/// <summary>
		/// Updates the specified object type.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the object type,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		void Update(int workspaceId, ObjectType entity);

		/// <summary>
		/// Gets the list of dependencies.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The entity ID.</param>
		/// <returns>The list of dependencies.</returns>
		List<Dependency> GetDependencies(int workspaceId, int entityId);

		/// <summary>
		/// Gets a list of all object types available to be a parent object type for a given workspace.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace to view all the available parent object types. Use -1 to indicate the admin workspace.</param>
		/// <returns>All oject types available to be parents in a workspace.</returns>
		List<ObjectType> GetAvailableParentObjectTypes(int workspaceId);
	}
}
