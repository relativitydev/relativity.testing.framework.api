using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the entity API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _entityService = relativityFacade.Resolve&lt;IEntityService&gt;();
	/// </code>
	/// </example>
	public interface IEntityService
	{
		/// <summary>
		/// Creates the specified production.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to create entity.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var entity = new Entity
		/// {
		/// 	FullName = Randomizer.GetString(),
		/// 	Type = new NamedArtifact { Name = "Other" },
		/// 	DocumentNumberingPrefix = Randomizer.GetString(),
		/// 	Classification = new List&lt;NamedArtifact&gt; { new NamedArtifact { Name = "Custodian – Processing" } }
		/// };
		/// var createdEntity = _entityService.Create(workspaceId, entity);
		/// </code>
		/// </example>
		Entity Create(int workspaceId, Entity entity);

		/// <summary>
		/// Gets the entity by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the entity.</param>
		/// <param name="entityId">The Artifact ID of the entity.</param>
		/// <returns>>The <see cref="Entity"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var someEntityId = 1;
		/// var entity = _entityService.Get(workspaceId, someEntityId);
		/// </code>
		/// </example>
		Entity Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the all entities.</param>
		/// <returns>The collection of <see cref="Entity"/> entities.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var entities = _entityService.GetAll(workspaceId);
		/// </code>
		/// </example>
		Entity[] GetAll(int workspaceId);

		/// <summary>
		/// Determines whether the entity with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the entity.</param>
		/// <returns><see langword="true"/> if a entity exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var someEntityId = 1;
		/// var entityExists = _entityService.Exists(workspaceId, someEntityId);
		/// </code>
		/// </example>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the entity.</param>
		/// <param name="entity">The entity to update.</param>
		/// <returns>The updated entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var someExistingEntityArtifactId = 1;
		/// var someExistingTypeArtifactId = 2;
		/// var entityToUpdate = new Entity
		/// {
		/// 	ArtifactId = someExistingEntityArtifactId,
		/// 	FirstName = "Updated First Name",
		/// 	LastName = "Updated Last Name",
		/// 	DocumentNumberingPrefix = Randomizer.GetString(),
		/// 	Type = new NamedArtifact { Name = "Person", ArtifactID = someExistingTypeArtifactId }
		/// };
		/// _entityService.Update(workspaceId, entityToUpdate);
		/// </code>
		/// </example>
		Entity Update(int workspaceId, Entity entity);

		/// <summary>
		/// Requires the specified entity.
		/// <list type="number">
		/// <item><description>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</description></item>
		/// <item><description>If <see cref="Entity.FullName"/> full name property of <paramref name="entity"/> have a value, gets entity by full name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</description></item>
		/// </list>
		/// </summary>
		/// <returns>The <see cref="Entity"/> entity or <see langword="null"/>.</returns>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require entity.</param>
		/// <param name="entity">The entity to require.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var someEntityId = 1;
		/// var entityToUpdate = _entityService.Get(workspaceId, someEntityId);
		/// entityToUpdate.FirstName = "Some Updated Name";
		/// entityToUpdate.DocumentNumberingPrefix = Randomizer.GetString();
		/// var updatedEntity = _entityService.Require(workspaceId, entityToUpdate); // Will get the entity by ArtifactId, update it and return updated entity
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var entityToUpdate = new Entity
		/// {
		/// 	FullName = "Some Existing Entity Full Name",
		/// 	DocumentNumberingPrefix = Randomizer.GetString(),
		/// 	Type = new NamedArtifact { Name = "Person", ArtifactID = someExistingTypeArtifactId }
		/// }
		/// var updatedEntity = _entityService.Require(workspaceId, entityToUpdate); // Will get the entity by Full Name, update it and return updated entity
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var entity = new Entity
		/// {
		/// 	FullName = Randomizer.GetString(),
		/// 	Type = new NamedArtifact { Name = "Other" },
		/// 	DocumentNumberingPrefix = Randomizer.GetString(),
		/// 	Classification = new List&lt;NamedArtifact&gt; { new NamedArtifact { Name = "Custodian – Processing" } }
		/// };
		/// var createdEntity = _entityService.Require(workspaceId, entity); // Will create new entity
		/// </code>
		/// </example>
		Entity Require(int workspaceId, Entity entity);

		/// <summary>
		/// Deletes the entity by the specified artifact ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the entity.</param>
		/// <param name="entityId">The Artifact ID of entity.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var someEntityId = 1;
		/// _entityService.Delete(workspaceId, someEntityId);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);
	}
}
