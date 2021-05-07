namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of entity creation of workspace level.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	public interface ICreateWorkspaceEntityStrategy<T>
	{
		/// <summary>
		/// Creates the specified entity.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		T Create(int workspaceId, T entity);
	}
}
