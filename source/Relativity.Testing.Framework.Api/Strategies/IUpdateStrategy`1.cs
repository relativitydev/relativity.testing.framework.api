namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of entity update.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IUpdateStrategy<T>
	{
		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		void Update(T entity);
	}
}
