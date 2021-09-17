using System;
using System.Linq;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentRequireStrategy : IRequireStrategy<Agent>
	{
		private readonly IUpdateStrategy<Agent> _updateStrategy;
		private readonly ICreateStrategy<Agent> _createStrategy;
		private readonly IAgentGetAllByAgentTypeNameStrategy _agentGetAllByAgentTypeNameStrategy;

		public AgentRequireStrategy(
			IUpdateStrategy<Agent> updateStrategy,
			ICreateStrategy<Agent> createStrategy,
			IAgentGetAllByAgentTypeNameStrategy agentGetAllByAgentTypeNameStrategy)
		{
			_updateStrategy = updateStrategy;
			_createStrategy = createStrategy;
			_agentGetAllByAgentTypeNameStrategy = agentGetAllByAgentTypeNameStrategy;
		}

		public Agent Require(Agent entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID != 0)
			{
				return _updateStrategy.Update(entity);
			}

			if (entity.AgentType != null)
			{
				var existedEntity = _agentGetAllByAgentTypeNameStrategy.GetAllByTypeName(entity.AgentType.Name).FirstOrDefault();
				if (existedEntity == null)
				{
					return _createStrategy.Create(entity);
				}
				else
				{
					entity.ArtifactID = existedEntity.ArtifactID;
					return _updateStrategy.Update(entity);
				}
			}
			else
			{
				throw new ArgumentException($"{nameof(Agent)} model should have {nameof(Agent.AgentType)} set.", nameof(entity));
			}
		}
	}
}
