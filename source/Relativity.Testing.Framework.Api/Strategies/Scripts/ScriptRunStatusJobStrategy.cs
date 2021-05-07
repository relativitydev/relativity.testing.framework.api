using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ScriptRunStatusJobStrategy : IScriptRunStatusJobStrategy
	{
		private readonly IScriptEnqueueRunJobStrategy _scriptEnqueueRunJobStrategy;
		private readonly IScriptReadRunJobStrategy _scriptReadRunJobStrategy;

		public ScriptRunStatusJobStrategy(
			IScriptEnqueueRunJobStrategy scriptEnqueueRunJobStrategy,
			IScriptReadRunJobStrategy scriptReadRunJobStrategy)
		{
			_scriptEnqueueRunJobStrategy = scriptEnqueueRunJobStrategy;
			_scriptReadRunJobStrategy = scriptReadRunJobStrategy;
		}

		public int Run(int workspaceId, int scriptId, List<ScriptInput> inputs = null)
		{
			var enqueueRunJobResponse = _scriptEnqueueRunJobStrategy.EnqueueRunJob(workspaceId, scriptId, inputs, 0);

			var runJob = _scriptReadRunJobStrategy.ReadRunJob(workspaceId, enqueueRunJobResponse.RunJobID);

			while (runJob.Status != RunJobStatus.Completed)
			{
				if (runJob.Status == RunJobStatus.CompletedWithErrors || runJob.Status == RunJobStatus.Errored)
				{
					throw new Exception(runJob.ErrorMessage);
				}

				runJob = _scriptReadRunJobStrategy.ReadRunJob(workspaceId, enqueueRunJobResponse.RunJobID);
			}

			var action = runJob.ActionJobs.Find(x => x.RowsAffected != null);

			return action.RowsAffected.Value;
		}
	}
}
