using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ScriptReadRunJobStrategy : IScriptReadRunJobStrategy
	{
		private readonly IRestService _restService;

		public ScriptReadRunJobStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public RunJob ReadRunJob(int workspaceId, Guid runJobId)
		{
			return _restService.Get<RunJob>($"Relativity.Scripts/workspace/{workspaceId}/Scripts/runs/{runJobId}");
		}
	}
}
