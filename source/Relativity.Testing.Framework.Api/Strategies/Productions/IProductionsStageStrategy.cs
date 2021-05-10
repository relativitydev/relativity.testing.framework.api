namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of staging a production.
	/// </summary>
	internal interface IProductionsStageStrategy
	{
		/// <summary>
		/// Stages a specific production.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a production.</param>
		/// <param name="seconds">Time to wait for "Staged" status.</param>
		void Stage(int workspaceId, int entityId, int seconds = 60);
	}
}
