using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.Toggle
{
	internal class ToggleGetByNameStrategy : IGetByNameStrategy<Api.Toggle>
	{
		private readonly IGetWorkspaceEntityByNameStrategy<Script> _getWorkspaceEntityByNameStrategy;
		private readonly ICreateWorkspaceEntityStrategy<Script> _createWorkspaceEntityStrategy;
		private readonly IScriptRunTableJobStrategy _scriptRunTableJob;

		public ToggleGetByNameStrategy(IGetWorkspaceEntityByNameStrategy<Script> getWorkspaceEntityByNameStrategy, ICreateWorkspaceEntityStrategy<Script> createWorkspaceEntityStrategy, IScriptRunTableJobStrategy scriptRunTableJob)
		{
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_scriptRunTableJob = scriptRunTableJob;
		}

		public Api.Toggle Get(string name)
		{
			var script = VerifyScriptExist();
			var rows = _scriptRunTableJob.Run(-1, script.ArtifactID, actionQueryRequest: new ActionQueryRequest { Condition = $"Name == {name}" }).Rows;
			if (rows.Count == 0)
			{
				return null;
			}

			return new Api.Toggle { Name = rows[0]["Name"].ToString(), IsEnabled = (bool)rows[0]["IsEnabled"] };
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
