using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the agents by agent type name.
	/// </summary>
	internal interface IAgentGetAllByAgentTypeNameStrategy
	{
		/// <summary>
		/// Gets all agents by specified agent type name.
		/// </summary>
		/// <param name="typeName">The agent type name.</param>
		/// <returns>The entities.</returns>
		Agent[] GetAllByTypeName(string typeName);
	}
}
