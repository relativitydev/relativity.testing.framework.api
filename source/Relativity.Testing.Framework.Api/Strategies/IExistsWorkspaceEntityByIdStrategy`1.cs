namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the existance of the entity by ID.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IExistsWorkspaceEntityByIdStrategy<T>
	{
		/// <summary>
		/// Determines whether the entity with the specified ID exists.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The artifact ID of the entity.</param>
		/// <returns><see langword="true"/> if an entity exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(int workspaceId, int entityId);
	}
}
