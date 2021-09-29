using System.Collections.Generic;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the agent API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _agentService = relativityFacade.Resolve&#60;IAgentService&#62;();
	/// </code>
	/// </example>
	public interface IAgentService
	{
		/// <summary>
		/// Creates the specified agent.
		/// </summary>
		/// <param name="entity">The [Agent](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Agent.html)agent</see> entity to create.</param>
		/// <returns>The created [Agent](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Agent.html)agent</see> entity.</returns>
		/// <example>
		/// <code>
		/// var agentType =  relativityFacade.Resolve&#60;IGetByNameStrategy&#60;AgentType&#62;&#62;()
		/// 		.Get("Some Agent Type");
		/// var agentServer = relativityFacade.Resolve&#60;IAgentServerGetAvailableStrategy&#62;().GetAvailable(_agentType.Name)[0];
		/// var entity = new Agent
		/// {
		/// 	AgentType = agentType,
		/// 	AgentServer = agentServer,
		/// 	RunInterval = 25
		/// }
		/// var createdAgent = _agentService.Create(entity);
		/// </code>
		/// </example>
		Agent Create(Agent entity);

		/// <summary>
		/// Requires the specified Agent(s).
		/// <list type="number">
		/// <item><description>If [Artifact.ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="entity"/> has positive value, gets entity by id and updates it.</description></item>
		/// <item><description>If [Agent.AgentType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Agent.html#Relativity_Testing_Framework_Models_Agent_AgentType) property of <paramref name="entity"/> exists, gets entity by type and updates it.</description></item>
		/// <item><description>Otherwise creates a new entity using [ICreateStrategy{T}](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Strategies.ICreateStrategy-1.html).</description></item>
		/// </list>
		/// </summary>
		/// <param name="entity">The [Agent](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Agent.html)agent</see> entity to require.</param>
		/// <returns>The required entity .</returns>
		/// <example>
		/// <code>
		/// var agentToUpdate = _agentService.Get(agentArtifactId);
		/// agentToUpdate.LoggingLevel = AgentLoggingLevelType.Critical;
		///
		/// var updatedAgent = _agentService.Require(agentToUpdate); // Finds agent by id, updates LoggingLevel property and returns it
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// var agentType =  relativityFacade.Resolve&#60;IGetByNameStrategy&#60;AgentType&#62;&#62;()
		/// 		.Get("Some Agent Type");
		/// var agentServer = relativityFacade.Resolve&#60;IAgentServerGetAvailableStrategy&#62;().GetAvailable(_agentType.Name)[0];
		/// var agentToCreate = new Agent
		/// {
		/// 	AgentType = agentType,
		/// 	AgentServer = agentServer,
		/// 	RunInterval = 25
		/// }
		///
		/// var createdAgent = _agentService.Require(agentToCreate); // Creates new agent and returns it
		/// </code>
		/// </example>
		Agent Require(Agent entity);

		/// <summary>
		/// Deletes the agent by ID.
		/// </summary>
		/// <param name="id">The artifact ID of the agent.</param>
		/// <example>
		/// <code>
		/// _agentService.Delete(agentToDeleteArtifactId);
		/// </code>
		/// </example>
		void Delete(int id);

		/// <summary>
		/// Gets all agent types.
		/// </summary>
		/// <returns>The collection of [AgentType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.AgentType.html) entities.</returns>
		/// <example>
		/// <code>
		/// IEnumerable&#60;AgentType&#62; allAgentTypes = _agentService.GetAllAgentTypes();
		/// </code>
		/// </example>
		IEnumerable<AgentType> GetAllAgentTypes();

		/// <summary>
		/// Gets available agent servers for specified agent type.
		/// </summary>
		/// <param name="agentTypeName">The agent type name.</param>
		/// <returns>The collection of [AgentServer](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.AgentServer.html) entities.</returns>
		/// <example>
		/// <code>
		/// IEnumerable&#60;AgentServer&#62; availableAgentServes = _agentService.GetAvailableAgentServers(someAgent.AgentType.Name);
		/// </code>
		/// </example>
		IEnumerable<AgentServer> GetAvailableAgentServers(string agentTypeName);

		/// <summary>
		/// Gets the agent by the specified ID.
		/// </summary>
		/// <param name="id">The artifact ID of the agent.</param>
		/// <returns>The [Agent](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Agent.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Agent agent = _agentService.Get(someAgentArtifatId);
		/// </code>
		/// </example>
		Agent Get(int id);

		/// <summary>
		/// Gets all agents.
		/// </summary>
		/// <returns>The collection of [Agent](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Agent.html) entities.</returns>
		/// <example>
		/// <code>
		/// IEnumerable&#60;Agent&#62; allAgents = _agentService.GetAllAgents();
		/// </code>
		/// </example>
		IEnumerable<Agent> GetAllAgents();

		/// <summary>
		/// Gets all agents by specified agent type.
		/// </summary>
		/// <param name="agentTypeName">The agent type name.</param>
		/// <returns>The collection of [Agent](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Agent.html) entities.</returns>
		/// <example>
		/// <code>
		/// IEnumerable&#60;Agent&#62; = _agentService.GetAllAgentsForAgentType("Sample Agent Type Name");
		/// </code>
		/// </example>
		IEnumerable<Agent> GetAllAgentsForAgentType(string agentTypeName);

		/// <summary>
		/// Gets agent type by specified name.
		/// </summary>
		/// <param name="name">The agent type name.</param>
		/// <returns>The collection of [AgentType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.AgentType.html) entities.</returns>
		/// <example>
		/// <code>
		/// AgentType sampleAgentType = _agentService.GetAgentType("Sample Agent Type Name");
		/// </code>
		/// </example>
		AgentType GetAgentType(string name);

		/// <summary>
		/// Updates the specified Agent.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <example>
		/// <code>
		/// var agentToUpdate = _agentService.Get(agentArtifactId);
		/// agentToUpdate.Enabled = false;
		/// _agentService.Update(agentToUpdate);
		/// </code>
		/// </example>
		void Update(Agent entity);
	}
}
