namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of ensuring of the existance of the entity by ID.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IEnsureExistsByIdStrategy<T>
	{
		/// <summary>
		/// Ensures whether the entity with the specified ID exists.
		/// If it does not, throws <see cref="ObjectNotFoundException"/>.
		/// </summary>
		/// <param name="id">The artifact ID of the entity.</param>
		/// [ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html)Failed to find entity by ID.</exception>
		void EnsureExists(int id);
	}
}
