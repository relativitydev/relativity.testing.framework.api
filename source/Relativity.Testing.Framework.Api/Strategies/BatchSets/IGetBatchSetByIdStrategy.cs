using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.BatchSets
{
	public interface IGetBatchSetByIdStrategy
	{
		/// <summary>
		/// Gets the entity by the specified workspace ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="batchSetId">The BatchSet ID.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns>The entity.</returns>
		BatchSet Get(int workspaceId, int batchSetId, UserCredentials userCredentials = null);
	}
}
