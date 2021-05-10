namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the entity by workspace ID and entity name.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	public interface IGetWorkspaceEntityByNameStrategy<T>
	{
		/// <summary>
		/// Gets the entity by the specified workspace ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityName">The entity name.</param>
		/// <returns>The entity.</returns>
		T Get(int workspaceId, string entityName);
	}
}
