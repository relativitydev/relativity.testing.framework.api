using System;
using System.Collections.Generic;
using System.Data;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ScriptRunTableJobStrategy : IScriptRunTableJobStrategy
	{
		private readonly IScriptEnqueueRunJobStrategy _scriptEnqueueRunJobStrategy;
		private readonly IScriptReadRunJobStrategy _scriptReadRunJobStrategy;
		private readonly IScriptQueryActionJobResultsStrategy _scriptQueryActionJobResultsStrategy;

		public ScriptRunTableJobStrategy(
			IScriptEnqueueRunJobStrategy scriptEnqueueRunJobStrategy,
			IScriptReadRunJobStrategy scriptReadRunJobStrategy,
			IScriptQueryActionJobResultsStrategy scriptQueryActionJobResultsStrategy)
		{
			_scriptEnqueueRunJobStrategy = scriptEnqueueRunJobStrategy;
			_scriptReadRunJobStrategy = scriptReadRunJobStrategy;
			_scriptQueryActionJobResultsStrategy = scriptQueryActionJobResultsStrategy;
		}

		public DataTable Run(int workspaceId, int scriptId, List<ScriptInput> inputs = null, ActionQueryRequest actionQueryRequest = null)
		{
			if (actionQueryRequest == null)
			{
				actionQueryRequest = new ActionQueryRequest();
			}

			var enqueueRunJobResponse = _scriptEnqueueRunJobStrategy.EnqueueRunJob(workspaceId, scriptId, inputs, 0);

			var runJob = _scriptReadRunJobStrategy.ReadRunJob(workspaceId, enqueueRunJobResponse.RunJobID);

			while (runJob.Status != RunJobStatus.Completed)
			{
				if (runJob.Status == RunJobStatus.CompletedWithErrors)
				{
					throw new Exception(runJob.ErrorMessage);
				}

				runJob = _scriptReadRunJobStrategy.ReadRunJob(workspaceId, enqueueRunJobResponse.RunJobID);
			}

			var getAction = enqueueRunJobResponse.Actions.Find(x => x.Verb == "GET");

			var actionIndex = enqueueRunJobResponse.Actions.IndexOf(getAction);

			var queryResponce = _scriptQueryActionJobResultsStrategy.QueryActionJobResults(workspaceId, enqueueRunJobResponse.RunJobID, actionIndex, actionQueryRequest);

			return GetDataTableFromQueryResponce(queryResponce);
		}

		private DataTable GetDataTableFromQueryResponce(ActionResultsQueryResponse actionResultsQueryResponse)
		{
			DataTable dataTable = new DataTable();
			foreach (var column in actionResultsQueryResponse.Columns)
			{
				var type = GetCollumnType(column.DataType);

				dataTable.Columns.Add(column.Name, Nullable.GetUnderlyingType(type) ?? type);
			}

			foreach (var row in actionResultsQueryResponse.Rows)
			{
				DataRow dataRow = dataTable.NewRow();

				for (int i = 0; i < row.Values.Count; i++)
				{
					dataRow[i] = row.Values[i] ?? DBNull.Value;
				}

				dataTable.Rows.Add(dataRow);
			}

			return dataTable;
		}

		private static Type GetCollumnType(ActionColumnDataType type)
		{
			string typeName;

			switch (type)
			{
				case ActionColumnDataType.Boolean:
					typeName = typeof(bool?).ToString();
					break;
				case ActionColumnDataType.Text:
					typeName = typeof(string).ToString();
					break;
				case ActionColumnDataType.Number:
					typeName = typeof(decimal?).ToString();
					break;
				case ActionColumnDataType.DateTime:
					typeName = typeof(DateTime?).ToString();
					break;
				default:
					typeName = typeof(object).ToString();
					break;
			}

			return Type.GetType(typeName);
		}
	}
}
