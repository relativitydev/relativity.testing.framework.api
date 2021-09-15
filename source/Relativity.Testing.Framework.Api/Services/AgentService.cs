using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class AgentService : IAgentService
	{
		private readonly ICreateStrategy<Agent> _createStrategy;
		private readonly IDeleteByIdStrategy<Agent> _deleteByIdStrategy;
		private readonly IGetAllStrategy<AgentType> _getAllAgentTypesStrategy;
		private readonly IAgentServerGetAvailableStrategy _agentServerGetAvailableStrategy;
		private readonly IGetByIdStrategy<Agent> _getByIdStrategy;
		private readonly IGetAllStrategy<Agent> _getAllAgentsStrategy;
		private readonly IAgentGetAllByAgentTypeNameStrategy _agentGetAllByAgentTypeNameStrategy;
		private readonly IGetByNameStrategy<AgentType> _getByNameStrategy;
		private readonly IUpdateStrategy<Agent> _updateStrategy;
		private readonly IRequireStrategy<Agent> _requireStrategy;

		public AgentService(
			ICreateStrategy<Agent> createStrategy,
			IDeleteByIdStrategy<Agent> deleteByIdStrategy,
			IGetAllStrategy<AgentType> getAllAgentTypesStrategy,
			IAgentServerGetAvailableStrategy agentServerGetAvailableStrategy,
			IGetByIdStrategy<Agent> getByIdStrategy,
			IGetAllStrategy<Agent> getAllAgentsStrategy,
			IAgentGetAllByAgentTypeNameStrategy agentGetAllByAgentTypeNameStrategy,
			IGetByNameStrategy<AgentType> getByNameStrategy,
			IUpdateStrategy<Agent> updateStrategy,
			IRequireStrategy<Agent> requireStrategy)
		{
			_createStrategy = createStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_getAllAgentTypesStrategy = getAllAgentTypesStrategy;
			_agentServerGetAvailableStrategy = agentServerGetAvailableStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getAllAgentsStrategy = getAllAgentsStrategy;
			_agentGetAllByAgentTypeNameStrategy = agentGetAllByAgentTypeNameStrategy;
			_getByNameStrategy = getByNameStrategy;
			_updateStrategy = updateStrategy;
			_requireStrategy = requireStrategy;
		}

		public Agent Create(Agent entity)
			=> _createStrategy.Create(entity);

		public void Delete(int id)
			=> _deleteByIdStrategy.Delete(id);

		public IEnumerable<AgentType> GetAllAgentTypes()
			=> _getAllAgentTypesStrategy.GetAll();

		public IEnumerable<AgentServer> GetAvailableAgentServers(string agentTypeName)
		=> _agentServerGetAvailableStrategy.GetAvailable(agentTypeName);

		public Agent Get(int id)
			=> _getByIdStrategy.Get(id);

		public IEnumerable<Agent> GetAllAgents()
			=> _getAllAgentsStrategy.GetAll();

		public IEnumerable<Agent> GetAllAgentsForAgentType(string agentTypeName)
			=> _agentGetAllByAgentTypeNameStrategy.GetAllByTypeName(agentTypeName);

		public AgentType GetAgentType(string name)
			=> _getByNameStrategy.Get(name);

		public Agent Update(Agent entity)
			=> _updateStrategy.Update(entity);

		public Agent Require(Agent entity)
			=> _requireStrategy.Require(entity);
	}
}
