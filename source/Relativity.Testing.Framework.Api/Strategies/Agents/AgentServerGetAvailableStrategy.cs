using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentServerGetAvailableStrategy : IAgentServerGetAvailableStrategy
	{
		private readonly IRestService _restService;
		private readonly IGetByNameStrategy<AgentType> _getByNameStrategy;

		public AgentServerGetAvailableStrategy(
			IRestService restService,
			IGetByNameStrategy<AgentType> getByNameStrategy)
		{
			_restService = restService;
			_getByNameStrategy = getByNameStrategy;
		}

		public AgentServer[] GetAvailable(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException(nameof(typeName));
			}

			var agentType = _getByNameStrategy.Get(typeName);

			if (agentType == null)
			{
				throw ObjectNotFoundException.CreateForNotFoundByName<AgentType>(typeName);
			}

			return _restService.Get<AgentServer[]>(
				$"relativity.agents/workspace/-1/agenttypes/{agentType.ArtifactID}/availableagentservers");
		}
	}
}
