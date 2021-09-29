namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of ensuring of the existance of the workspace entity by ID.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IEnsureWorkspaceEntityExistsByIdStrategy<T>
	{
		/// <summary>
		/// Ensures whether the entity with the specified ID exists.
		/// If it does not, throws [ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html).
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The artifact ID of the entity.</param>
		/// [ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html)Failed to find entity by ID.
		void EnsureExists(int workspaceId, int entityId);
	}
}
