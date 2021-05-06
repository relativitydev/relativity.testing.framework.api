namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting a list of users in a workspace.
	/// You can then use this list to assign owners to a view.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetAllWorkspaceViewOwnersStrategy<T>
	{
		/// <summary>
		/// Gets the entities by the specified workspace ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The entities.</returns>
		T[] GetViewOwners(int workspaceId);
	}
}
