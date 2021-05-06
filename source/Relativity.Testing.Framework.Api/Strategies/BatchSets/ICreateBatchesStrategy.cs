using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of creating batches.
	/// </summary>
	internal interface ICreateBatchesStrategy
	{
		/// <summary>
		/// Runs create bathces operation for a specific batch set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a batch set.</param>
		/// <returns>>The <see cref="BatchProcessResult"/> entity.</returns>
		BatchProcessResult CreateBatches(int workspaceId, int entityId);
	}
}
