using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the markup API service.
	/// </summary>
	public interface IMarkupSetService
	{
		/// <summary>
		///  Creates the specified markup set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to create the markup set,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The <see cref="MarkupSet"/> entity or <see langword="null"/>.</returns>
		MarkupSet Create(int workspaceId, MarkupSet entity);

		/// <summary>
		/// Gets the markup set by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the markup set,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The Artifact ID of the markup set.</param>
		/// <returns>The <see cref="MarkupSet"/> entity or <see langword="null"/>.</returns>
		MarkupSet Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the markup set by the specified name.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the markup set,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the markup set.</param>
		/// <returns>The <see cref="MarkupSet"/> entity or <see langword="null"/>.</returns>
		MarkupSet Get(int workspaceId, string entityName);

		/// <summary>
		/// Requires the specified markup set.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> has a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <returns>The <see cref="MarkupSet"/> entity or <see langword="null"/>.</returns>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require markup set.</param>
		/// <param name="entity">The entity to require.</param>
		MarkupSet Require(int workspaceId, MarkupSet entity);

		/// <summary>
		/// Determines whether the markup set with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to check existing of markup set,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The Artifact ID of the markup set.</param>
		/// <returns><see langword="true"/> if a markup set exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified markup set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the markup set,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		void Update(int workspaceId, MarkupSet entity);

		/// <summary>
		/// Deletes the markup set by the specified artifact ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the markup set,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The Artifact ID of the markup set.</param>
		void Delete(int workspaceId, int entityId);
	}
}
