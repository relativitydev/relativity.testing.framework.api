using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the production API service.
	/// </summary>
	public interface IProductionService
	{
		/// <summary>
		/// Creates the specified production.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new production.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Production Create(int workspaceId, Production entity);

		/// <summary>
		/// Gets the production by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the production.</param>
		/// <param name="entityId">The Artifact ID of the production.</param>
		/// <returns>>The <see cref="Production"/> entity or <see langword="null"/>.</returns>
		Production Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the production by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the production.</param>
		/// <param name="entityName">The Artifact ID of the production.</param>
		/// <returns>>The <see cref="Production"/> entity or <see langword="null"/>.</returns>
		Production Get(int workspaceId, string entityName);

		/// <summary>
		/// Gets all productions.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the all productions.</param>
		/// <returns>The collection of <see cref="Production"/> entities.</returns>
		Production[] GetAll(int workspaceId);

		/// <summary>
		/// Determines whether the production with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the production.</param>
		/// <returns><see langword="true"/> if a production exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Delete the production by the specified artifact ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the production.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets status of a specific production.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a production.</param>
		/// <returns>Returns status of a specific production.</returns>
		ProductionStatus GetStatus(int workspaceId, int entityId);

		/// <summary>
		/// Stages a specific production.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a production.</param>
		/// <param name="seconds">Time to wait for "Staged" status.</param>
		void Stage(int workspaceId, int entityId, int seconds = 60);

		/// <summary>
		/// Runs a specific production.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a production.</param>
		/// <param name="timeout">Time to wait for "Produced" status.</param>
		void Run(int workspaceId, int entityId, int timeout = 120);
	}
}
