using System;
using System.Collections.Generic;
using System.Data;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ScriptService : IScriptService
	{
		private readonly ICreateWorkspaceEntityStrategy<Script> _createWorkspaceEntityStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<Script> _deleteWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Script> _getWorkspaceEntityByIdStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Script> _updateWorkspaceEntityStrategy;

		private readonly IScriptPreviewStrategy _scriptPreviewStrategy;
		private readonly IScriptEnqueueRunJobStrategy _scriptEnqueueRunJobStrategy;
		private readonly IScriptReadRunJobStrategy _scriptReadRunJobStrategy;
		private readonly IScriptQueryActionJobResultsStrategy _scriptQueryActionJobResultsStrategy;
		private readonly IScriptRunStatusJobStrategy _scriptRunStatusJobStrategy;
		private readonly IScriptRunTableJobStrategy _scriptRunTableJobStrategy;

		public ScriptService(
			ICreateWorkspaceEntityStrategy<Script> createWorkspaceEntityStrategy,
			IDeleteWorkspaceEntityByIdStrategy<Script> deleteWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByIdStrategy<Script> getWorkspaceEntityByIdStrategy,
			IUpdateWorkspaceEntityStrategy<Script> updateWorkspaceEntityStrategy,
			IScriptPreviewStrategy scriptPreviewStrategy,
			IScriptEnqueueRunJobStrategy scriptEnqueueRunJobStrategy,
			IScriptReadRunJobStrategy scriptReadRunJobStrategy,
			IScriptQueryActionJobResultsStrategy scriptQueryActionJobResultsStrategy,
			IScriptRunStatusJobStrategy scriptRunStatusJobStrategy,
			IScriptRunTableJobStrategy scriptRunTableJobStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
			_scriptPreviewStrategy = scriptPreviewStrategy;
			_scriptEnqueueRunJobStrategy = scriptEnqueueRunJobStrategy;
			_scriptReadRunJobStrategy = scriptReadRunJobStrategy;
			_scriptQueryActionJobResultsStrategy = scriptQueryActionJobResultsStrategy;
			_scriptRunStatusJobStrategy = scriptRunStatusJobStrategy;
			_scriptRunTableJobStrategy = scriptRunTableJobStrategy;
		}

		public Script Create(int workspaceId, Script entity)
			 => _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public Script Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public void Update(int workspaceId, Script entity)
			=> _updateWorkspaceEntityStrategy.Update(workspaceId, entity);

		public string Preview(int workspaceId, int scriptId, List<ScriptInput> inputs = null, double timeZoneOffset = 0)
			=> _scriptPreviewStrategy.PreviewScript(workspaceId, scriptId, inputs, timeZoneOffset);

		public EnqueueRunJobResponse EnqueueRunJob(int workspaceId, int scriptId, List<ScriptInput> inputs = null, double timeZoneOffset = 0)
			=> _scriptEnqueueRunJobStrategy.EnqueueRunJob(workspaceId, scriptId, inputs, timeZoneOffset);

		public RunJob ReadRunJob(int workspaceId, Guid runJobId)
			=> _scriptReadRunJobStrategy.ReadRunJob(workspaceId, runJobId);

		public ActionResultsQueryResponse QueryActionJobResults(int workspaceId, Guid runJobId, int actionIndex, ActionQueryRequest actionQueryRequest = null, int start = 0, int length = 100)
			=> _scriptQueryActionJobResultsStrategy.QueryActionJobResults(workspaceId, runJobId, actionIndex, actionQueryRequest, start, length);

		public int RunStatusAction(int workspaceId, int scriptId, List<ScriptInput> inputs = null)
			=> _scriptRunStatusJobStrategy.Run(workspaceId, scriptId, inputs);

		public DataTable RunTableAction(int workspaceId, int scriptId, List<ScriptInput> inputs = null, ActionQueryRequest actionQueryRequest = null)
			=> _scriptRunTableJobStrategy.Run(workspaceId, scriptId, inputs, actionQueryRequest);
	}
}
