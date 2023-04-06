namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the entity by ID.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetByIdStrategy<T>
	{
		/// <summary>
		/// Gets the entity by the specified ID.
		/// </summary>
		/// <param name="id">The entity ID.</param>
		/// <returns>The entity or <see langword="null"/>.</returns>
		T Get(int id);
	}
}
