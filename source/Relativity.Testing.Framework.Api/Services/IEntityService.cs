using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the entity API service.
	/// </summary>
	public interface IEntityService
	{
		/// <summary>
		/// Creates the specified production.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to create entity.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Entity Create(int workspaceId, Entity entity);

		/// <summary>
		/// Gets the entity by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the entity.</param>
		/// <param name="entityId">The Artifact ID of the entity.</param>
		/// <returns>>The <see cref="Entity"/> entity or <see langword="null"/>.</returns>
		Entity Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the all entities.</param>
		/// <returns>The collection of <see cref="Entity"/> entities.</returns>
		Entity[] GetAll(int workspaceId);

		/// <summary>
		/// Determines whether the entity with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the entity.</param>
		/// <returns><see langword="true"/> if a entity exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the entity.</param>
		/// <param name="entity">The entity to update.</param>
		void Update(int workspaceId, Entity entity);

		/// <summary>
		/// Requires the specified entity.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="Entity.FullName"/> full name property of <paramref name="entity"/> have a value, gets entity by full name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <returns>The <see cref="Entity"/> entity or <see langword="null"/>.</returns>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require entity.</param>
		/// <param name="entity">The entity to require.</param>
		Entity Require(int workspaceId, Entity entity);

		/// <summary>
		/// Deletes the entity by the specified artifact ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the entity.</param>
		/// <param name="entityId">The Artifact ID of entity.</param>
		void Delete(int workspaceId, int entityId);
	}
}
