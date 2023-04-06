namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the entity.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetStrategy<T>
	{
		/// <summary>
		/// Gets the entity.
		/// </summary>
		/// <returns>The entity.</returns>
		T Get();
	}
}
