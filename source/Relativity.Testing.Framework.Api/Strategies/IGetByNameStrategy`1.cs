namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the entity by name.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetByNameStrategy<T>
	{
		/// <summary>
		/// Gets the entity by the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The entity or <see langword="null"/>.</returns>
		T Get(string name);
	}
}
