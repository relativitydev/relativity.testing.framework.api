using System.Collections.Generic;
using System.Data;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IScriptRunTableJobStrategy
	{
		/// <summary>
		/// Executes a script with table action and parameters.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and should be executed.</param>
		/// <param name="scriptId">The ArtifactID of the script to execute.</param>
		/// <param name="inputs">Inputs and their values for the script.</param>
		/// <param name="actionQueryRequest">The query to execute. By default takes all fields without condition.</param>
		/// <returns>The table of results.</returns>
		DataTable Run(int workspaceId, int scriptId, List<ScriptInput> inputs = null, ActionQueryRequest actionQueryRequest = null);
	}
}
