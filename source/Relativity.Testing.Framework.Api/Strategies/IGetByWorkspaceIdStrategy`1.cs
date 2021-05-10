namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the entity by workspace ID.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetByWorkspaceIdStrategy<T>
	{
		/// <summary>
		/// Gets the entity by the specified workspace ID.
		/// </summary>
		/// <param name="id">The workspace ID.</param>
		/// <returns>The entity.</returns>
		T Get(int id);
	}
}
