using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting productions status.
	/// </summary>
	internal interface IGetProductionStatusStrategy
	{
		/// <summary>
		/// Gets status of a specific production.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a production.</param>
		/// <returns>Returns status of a specific production.</returns>
		ProductionStatus GetStatus(int workspaceId, int entityId);
	}
}
