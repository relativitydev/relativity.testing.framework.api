using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.Toggle
{
	internal class ToggleGetByAllStrategy : IGetAllStrategy<Api.Toggle>
	{
		private readonly IGetWorkspaceEntityByNameStrategy<Script> _getWorkspaceEntityByNameStrategy;
		private readonly ICreateWorkspaceEntityStrategy<Script> _createWorkspaceEntityStrategy;
		private readonly IScriptRunTableJobStrategy _scriptRunTableJob;

		public ToggleGetByAllStrategy(IGetWorkspaceEntityByNameStrategy<Script> getWorkspaceEntityByNameStrategy, ICreateWorkspaceEntityStrategy<Script> createWorkspaceEntityStrategy, IScriptRunTableJobStrategy scriptRunTableJob)
		{
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_scriptRunTableJob = scriptRunTableJob;
		}

		public Api.Toggle[] GetAll()
		{
			var script = VerifyScriptExist();
			var rows = _scriptRunTableJob.Run(-1, script.ArtifactID).Rows;
			if (rows.Count == 0)
			{
				return null;
			}

			List<Api.Toggle> toggles = new List<Api.Toggle>();
			foreach (DataRow row in rows)
			{
				toggles.Add(new Api.Toggle { Name = row.ToString(), IsEnabled = (bool)row["IsEnabled"] });
			}

			return toggles.ToArray();
		}

		public Script VerifyScriptExist()
		{
			var scriptName = "RTF_GetToggles";
			var script = _getWorkspaceEntityByNameStrategy.Get(-1, scriptName);
			if (script == null)
			{
				string scriptBody = $"<script><name>{scriptName}</name><description></description><category></category><display type=\"itemlist\" /><action returns=\"table\"><![CDATA[SELECT TOP (1000) [Name],[IsEnabled] FROM [EDDS].[eddsdbo].[Toggle]]]></action></script>";
				script = _createWorkspaceEntityStrategy.Create(-1, new Script
				{
					ScriptBody = scriptBody
				});
			}

			return script;
		}
	}
}
