using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the available agent servers for agent type.
	/// </summary>
	internal interface IAgentServerGetAvailableStrategy
	{
		/// <summary>
		/// Gets available agent servers by specified agent type name.
		/// </summary>
		/// <param name="typeName">The agent type name.</param>
		/// <returns>The entities.</returns>
		AgentServer[] GetAvailable(string typeName);
	}
}
