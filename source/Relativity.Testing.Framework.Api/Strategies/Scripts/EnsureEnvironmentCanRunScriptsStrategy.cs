using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class EnsureEnvironmentCanRunScriptsStrategy : IEnsureEnvironmentCanRunScriptsStrategy
	{
		private readonly IAgentService _agentService;
		private readonly IGetByNameStrategy<AgentType> _getByNameStrategy;

		public EnsureEnvironmentCanRunScriptsStrategy(IAgentService agentService, IGetByNameStrategy<AgentType> getByNameStrategy)
		{
			_agentService = agentService;
			_getByNameStrategy = getByNameStrategy;
		}

		public void EnsureEnvironmentCanRunScripts()
		{
			AgentType scriptRunManager = _getByNameStrategy.Get("Script Run Manager");
			AgentServer agentServer = _agentService.GetAvailableAgentServers(scriptRunManager.Name).First();

			_agentService.Require(new Agent
			{
				AgentServer = agentServer,
				AgentType = scriptRunManager,
				Enabled = true,
				RunInterval = 5
			});
		}
	}
}
