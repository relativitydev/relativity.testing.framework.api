namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the generic strategy of ensuring of the existance of the entity by ID.
	/// Uses <see cref="IExistsByIdStrategy{T}"/> strategy for checking the entity.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	public class EnsureWorkspaceEntityExistsByIdStrategy<T> : IEnsureWorkspaceEntityExistsByIdStrategy<T>
	{
		private readonly IExistsWorkspaceEntityByIdStrategy<T> _existsByIdStrategy;

		/// <summary>
		/// Initializes a new instance of the <see cref="EnsureWorkspaceEntityExistsByIdStrategy{T}"/> class.
		/// </summary>
		/// <param name="existsByIdStrategy">An instance of <see cref="IExistsByIdStrategy{T}"/>.</param>
		public EnsureWorkspaceEntityExistsByIdStrategy(IExistsWorkspaceEntityByIdStrategy<T> existsByIdStrategy)
		{
			_existsByIdStrategy = existsByIdStrategy;
		}

		/// <summary>
		/// Ensures whether the entity with the specified ID exists.
		/// If it does not, throws <see cref="ObjectNotFoundException" />.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The artifact ID of the entity.</param>
		/// <exception cref="ObjectNotFoundException">Failed to find entity by ID.</exception>
		public void EnsureExists(int workspaceId, int entityId)
		{
			if (!_existsByIdStrategy.Exists(workspaceId, entityId))
				throw ObjectNotFoundException.CreateForNotFoundById<T>(entityId);
		}
	}
}
