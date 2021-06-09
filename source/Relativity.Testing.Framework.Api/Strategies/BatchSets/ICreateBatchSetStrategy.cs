using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of creating BatchSet.
	/// </summary>
	internal interface ICreateBatchSetStrategy
	{
		/// <summary>
		/// Creates batch set.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entity">The BatchSet to create.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>>The <see cref="BatchSet"/> BatchSet.</returns>
		BatchSet Create(int workspaceId, BatchSet entity, UserCredentials userCredentials = null);
	}
}
