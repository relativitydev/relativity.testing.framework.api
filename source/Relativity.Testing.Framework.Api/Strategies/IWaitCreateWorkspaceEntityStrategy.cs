namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents wait for create workspace entity strategy.
	/// </summary>
	internal interface IWaitCreateWorkspaceEntityStrategy
	{
		/// <summary>
		/// Waits the entity by the specified ID.
		/// </summary>
		/// <typeparam name="T">The field type.</typeparam>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The entity ID.</param>
		void Wait<T>(int workspaceId, int entityId);
	}
}
