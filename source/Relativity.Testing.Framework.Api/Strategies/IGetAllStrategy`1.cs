namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting all entities of particular type.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	public interface IGetAllStrategy<T>
	{
		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns>The entities.</returns>
		T[] GetAll();
	}
}
