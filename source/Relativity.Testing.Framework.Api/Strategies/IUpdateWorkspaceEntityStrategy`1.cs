namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of workspace entity update.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	public interface IUpdateWorkspaceEntityStrategy<T>
	{
		/// <summary>
		/// Updates the specified workspace entity.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entity">The entity to update.</param>
		void Update(int workspaceId, T entity);
	}
}
