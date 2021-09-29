using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of batches purge.
	/// </summary>
	internal interface IPurgeBatchesStrategy
	{
		/// <summary>
		/// Runs purge bathces operation for a specific batch set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a batch set.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>>The [BatchProcessResult](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.BatchProcessResult.html) entity.</returns>
		BatchProcessResult PurgeBatches(int workspaceId, int entityId, UserCredentials userCredentials = null);
	}
}
