using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the batch set API service.
	/// </summary>
	public interface IBatchSetService
	{
		/// <summary>
		/// Creates the specified batch set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new batch set.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		BatchSet Create(int workspaceId, BatchSet entity);

		/// <summary>
		/// Gets the batch set by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the batch set.</param>
		/// <param name="entityId">The Artifact ID of the batch set.</param>
		/// <returns>>The <see cref="Production"/> entity or <see langword="null"/>.</returns>
		BatchSet Get(int workspaceId, int entityId);

		/// <summary>
		/// Determines whether the batch set with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace.</param>
		/// <param name="entityId">The Artifact ID of the batch set.</param>
		/// <returns><see langword="true"/> if a batch set exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Runs create batches operation for a specific batch set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a batch set.</param>
		/// <returns>>The <see cref="BatchProcessResult"/> entity.</returns>
		BatchProcessResult CreateBatches(int workspaceId, int entityId);

		/// <summary>
		/// Runs purge bathces operation for a specific batch set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a batch set.</param>
		/// <returns>>The <see cref="BatchProcessResult"/> entity.</returns>
		BatchProcessResult PurgeBatches(int workspaceId, int entityId);
	}
}
