using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of waiting for a specific status of a production.
	/// </summary>
	internal interface IProductionsWaitForStatusStrategy
	{
		/// <summary>
		/// Waits for status of a specific production.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of a workspace.</param>
		/// <param name="entityId">The Artifact ID of a production.</param>
		/// <param name="expectedStatus">Expected Status of a production.</param>
		/// <param name="seconds">Time to wait for expected status in seconds.</param>
		void WaitForStatus(int workspaceId, int entityId, ProductionStatus expectedStatus, int seconds);
	}
}
