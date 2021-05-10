using System;
using System.Linq;using Relativity.Testing.Framework.Api.Services;using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentCreateStrategy : CreateStrategy<Agent>
	{
		private readonly IRestService _restService;
		private readonly IGetByIdStrategy<Agent> _getByIdStrategy;
		private readonly IGetByNameStrategy<AgentType> _getAgentTypeByNameStrategy;

		public AgentCreateStrategy(
			IRestService restService,
			IGetByIdStrategy<Agent> getByIdStrategy,
			IGetByNameStrategy<AgentType> getByNameStrategy)
		{
			_restService = restService;
			_getByIdStrategy = getByIdStrategy;
			_getAgentTypeByNameStrategy = getByNameStrategy;
		}

		protected override Agent DoCreate(Agent entity)
		{
			Validate(entity);

			AgentRequest agentRequest = entity.ToAgentRequest();
			agentRequest.SetIntervalIfNotSet();

			AgentCreateRequest agentCreateRequest = new AgentCreateRequest
			{
				AgentRequest = agentRequest,
				Count = 1
			};

			int artifactId = _restService.Post<int[]>("relativity.agents/workspace/-1/agents/", agentCreateRequest).First();
			return _getByIdStrategy.Get(artifactId);
		}

		private void Validate(Agent entity)
		{
			if (entity.AgentType == null)
			{
				throw new ArgumentException($"{nameof(Agent)} model should have {nameof(Agent.AgentType)} set.", nameof(entity));
			}

			if (entity.AgentServer == null)
			{
				throw new ArgumentException($"{nameof(Agent)} model should have {nameof(Agent.AgentServer)} set.", nameof(entity));
			}

			if (entity.AgentType.ArtifactID == 0 && entity.AgentType.Name != null)
			{
				entity.AgentType = _getAgentTypeByNameStrategy.Get(entity.AgentType.Name);

				if (entity.AgentType == null)
				{
					throw ObjectNotFoundException.CreateForNotFoundByName<AgentType>(entity.AgentType.Name);
				}
			}
		}
	}
}
