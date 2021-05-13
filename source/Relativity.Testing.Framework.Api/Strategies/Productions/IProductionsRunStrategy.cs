namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of running a production.
	/// </summary>
	internal interface IProductionsRunStrategy
	{
		/// <summary>
		/// Runs a specific production.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a production.</param>
		/// <param name="timeout">Time to wait for "Produced" status.</param>
		void Run(int workspaceId, int entityId, int timeout = 120);
	}
}
