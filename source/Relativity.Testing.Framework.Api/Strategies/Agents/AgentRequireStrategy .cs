using System;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentRequireStrategy : IRequireStrategy<Agent>
	{
		private readonly IRestService _restService;
		private readonly IUpdateStrategy<Agent> _updateStrategy;
		private readonly IGetByIdStrategy<Agent> _getByIdStrategy;
		private readonly ICreateStrategy<Agent> _createStrategy;
		private readonly IAgentGetAllByAgentTypeNameStrategy _agentGetAllByAgentTypeNameStrategy;

		public AgentRequireStrategy(
			IRestService restService,
			IUpdateStrategy<Agent> updateStrategy,
			IGetByIdStrategy<Agent> getByIdStrategy,
			ICreateStrategy<Agent> createStrategy,
			IAgentGetAllByAgentTypeNameStrategy agentGetAllByAgentTypeNameStrategy)
		{
			_restService = restService;
			_updateStrategy = updateStrategy;
			_getByIdStrategy = getByIdStrategy;
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
				_updateStrategy.Update(entity);
				return _getByIdStrategy.Get(entity.ArtifactID);
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
					_updateStrategy.Update(entity);
					return _getByIdStrategy.Get(entity.ArtifactID);
				}
			}
			else
			{
				throw new ArgumentException($"{nameof(Agent)} model should have {nameof(Agent.AgentType)} set.", nameof(entity));
			}
		}
	}
}
