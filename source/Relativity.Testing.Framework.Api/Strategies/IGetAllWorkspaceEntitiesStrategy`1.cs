namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the all entities in workspace by workspace ID.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetAllWorkspaceEntitiesStrategy<T>
	{
		/// <summary>
		/// Gets the entities by the specified workspace ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The entities.</returns>
		T[] GetAll(int workspaceId);
	}
}
