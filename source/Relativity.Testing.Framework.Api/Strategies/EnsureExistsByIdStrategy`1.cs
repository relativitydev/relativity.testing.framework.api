namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the generic strategy of ensuring of the existance of the entity by ID.
	/// Uses <see cref="IExistsByIdStrategy{T}"/> strategy for checking the entity.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal class EnsureExistsByIdStrategy<T> : IEnsureExistsByIdStrategy<T>
	{
		private readonly IExistsByIdStrategy<T> _existsByIdStrategy;

		/// <summary>
		/// Initializes a new instance of the <see cref="EnsureExistsByIdStrategy{T}"/> class.
		/// </summary>
		/// <param name="existsByIdStrategy">An instance of <see cref="IExistsByIdStrategy{T}"/>.</param>
		public EnsureExistsByIdStrategy(IExistsByIdStrategy<T> existsByIdStrategy)
		{
			_existsByIdStrategy = existsByIdStrategy;
		}

		/// <summary>
		/// Ensures whether the entity with the specified ID exists.
		/// If it does not, throws [ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html).
		/// </summary>
		/// <param name="id">The artifact ID of the entity.</param>
		/// <exception>[ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html)Failed to find entity by ID.</exception>
		public void EnsureExists(int id)
		{
			if (!_existsByIdStrategy.Exists(id))
				throw ObjectNotFoundException.CreateForNotFoundById<T>(id);
		}
	}
}
