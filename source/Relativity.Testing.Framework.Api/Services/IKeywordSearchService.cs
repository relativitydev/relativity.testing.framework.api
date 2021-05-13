using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the keyword search API service.
	/// </summary>
	public interface IKeywordSearchService
	{
		/// <summary>
		/// Creates the specified keyword search.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new keyword search.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		KeywordSearch Create(int workspaceId, KeywordSearch entity);

		/// <summary>
		/// Deletes the keyword search by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the keyword search.</param>
		/// <param name="entityId">The artifact ID of the keyword search.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the keyword search by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get keyword search.</param>
		/// <param name="entityId">The artifact ID of the keyword search.</param>
		/// <returns>The <see cref="KeywordSearch"/> entity or <see langword="null"/>.</returns>
		KeywordSearch Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the keyword search by the specified name.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get keyword search.</param>
		/// <param name="entityName">The name of the keyword search.</param>
		/// <returns>The <see cref="KeywordSearch"/> entity or <see langword="null"/>.</returns>
		KeywordSearch Get(int workspaceId, string entityName);

		/// <summary>
		/// Query the keyword seaches by specified condition.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get keyword search.</param>
		/// <param name="condition">The condition.</param>
		/// <returns>The array of keyword seaches.</returns>
		KeywordSearch[] Query(int workspaceId, string condition);

		/// <summary>
		/// Updates the specified keyword search.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new keyword search.</param>
		/// <param name="entity">The entity to update.</param>
		void Update(int workspaceId, KeywordSearch entity);

		/// <summary>
		/// Requires the specified keyword search.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> has a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <returns>The <see cref="KeywordSearch"/> entity or <see langword="null"/>.</returns>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require keyword search.</param>
		/// <param name="entity">The entity to require.</param>
		KeywordSearch Require(int workspaceId, KeywordSearch entity);
	}
}
