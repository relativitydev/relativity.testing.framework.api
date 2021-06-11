using Relativity.Testing.Framework.Api.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting BatchSet by ID.
	/// </summary>
	public interface IExistsBatchSetByIdStrategy
	{
		/// <summary>
		/// Determines whether the entity with the specified ID exists.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The artifact ID of the entity.</param>
		/// <param name="userCredentials">User credentials to be used when perfroming action over Relativity Api.</param>
		/// <returns><see langword="true"/> if an entity exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(int workspaceId, int entityId, UserCredentials userCredentials = null);
	}
}
