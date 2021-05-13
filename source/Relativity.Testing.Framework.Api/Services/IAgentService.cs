using System.Collections.Generic;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the agent API service.
	/// </summary>
	public interface IAgentService
	{
		/// <summary>
		/// Creates the specified agent.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Agent Create(Agent entity);

		/// <summary>
		/// Requires the specified Agent.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by id and updates it.</item>
		/// <item>If <see cref="Agent.AgentType"/> property of <paramref name="entity"/> is existed, gets entity by type and updates it.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The required entity .</returns>
		Agent Require(Agent entity);

		/// <summary>
		/// Deletes the agent by ID.
		/// </summary>
		/// <param name="id">The artifact ID of the agent.</param>
		void Delete(int id);

		/// <summary>
		/// Gets all agent types.
		/// </summary>
		/// <returns>The collection of <see cref="AgentType"/> entities.</returns>
		IEnumerable<AgentType> GetAllAgentTypes();

		/// <summary>
		/// Gets available agent servers for specified agent type.
		/// </summary>
		/// <param name="agentTypeName">The agent type name.</param>
		/// <returns>The collection of <see cref="AgentServer"/> entities.</returns>
		IEnumerable<AgentServer> GetAvailableAgentServers(string agentTypeName);

		/// <summary>
		/// Gets the agent by the specified ID.
		/// </summary>
		/// <param name="id">The artifact ID of the agent.</param>
		/// <returns>The <see cref="Agent"/> entity or <see langword="null"/>.</returns>
		Agent Get(int id);

		/// <summary>
		/// Gets all agents.
		/// </summary>
		/// <returns>The collection of <see cref="Agent"/> entities.</returns>
		IEnumerable<Agent> GetAllAgents();

		/// <summary>
		/// Gets all agents by specified agent type.
		/// </summary>
		/// <param name="agentTypeName">The agent type name.</param>
		/// <returns>The collection of <see cref="Agent"/> entities.</returns>
		IEnumerable<Agent> GetAllAgentsForAgentType(string agentTypeName);

		/// <summary>
		/// Gets agent type by specified name.
		/// </summary>
		/// <param name="name">The agent type name.</param>
		/// <returns>The collection of <see cref="AgentType"/> entities.</returns>
		AgentType GetAgentType(string name);

		/// <summary>
		/// Updates the specified Agent.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		void Update(Agent entity);
	}
}
