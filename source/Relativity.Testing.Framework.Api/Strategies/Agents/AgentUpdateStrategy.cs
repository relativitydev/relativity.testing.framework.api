using System;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentUpdateStrategy : IUpdateStrategy<Agent>
	{
		private readonly IRestService _restService;
		private readonly IGetAllStrategy<Agent> _getAllStrategy;
		private readonly IGetByIdStrategy<Agent> _getAgentByIdStrategy;

		public AgentUpdateStrategy(
			IRestService restService,
			IGetAllStrategy<Agent> getAllStrategy,
			IGetByIdStrategy<Agent> getAgentById)
		{
			_restService = restService;
			_getAllStrategy = getAllStrategy;
			_getAgentByIdStrategy = getAgentById;
		}

		public void Update(Agent entity)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID == 0)
			{
				if (entity.AgentType != null)
				{
					var entityByType = _getAllStrategy.GetAll().FirstOrDefault(x => x.AgentType == entity.AgentType);

					if (entityByType == null)
					{
						throw new ArgumentException("Can't find the entity.", nameof(entity));
					}
					else
					{
						entity.ArtifactID = entityByType.ArtifactID;
					}
				}
				else
				{
					throw new ArgumentException("This entity should have an artifact ID or name as an identifier.", nameof(entity));
				}
			}

			int runInterval = GetCurrentRunIntervalIfNotSet(entity);

			AgentRequest agentRequest = entity.ToAgentRequest();
			agentRequest.SetIntervalIfNotSet(runInterval);

			AgentUpdateRequest agentUpdateRequest = new AgentUpdateRequest
			{
				AgentRequest = agentRequest,
			};

			_restService.Put($"relativity.agents/workspace/-1/agents/{entity.ArtifactID}", agentUpdateRequest);
		}

		internal int GetCurrentRunIntervalIfNotSet(Agent entity)
		{
			int runInterval = entity.RunInterval > 0 ? entity.RunInterval : _getAgentByIdStrategy.Get(entity.ArtifactID).RunInterval;
			return runInterval;
		}
	}
}
