namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the entity by workspace and entity ID.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetWorkspaceEntityByIdStrategy<T>
	{
		/// <summary>
		/// Gets the entity by the specified workspace ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The entity ID.</param>
		/// <returns>The entity.</returns>
		T Get(int workspaceId, int entityId);
	}
}
