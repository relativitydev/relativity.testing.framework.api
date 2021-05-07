using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IScriptRunStatusJobStrategy
	{
		/// <summary>
		/// Executes a script with status action and parameters.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and should be executed.</param>
		/// <param name="scriptId">The ArtifactID of the script to execute.</param>
		/// <param name="inputs">Inputs and their values for the script.</param>
		/// <returns>The count of affected rows.</returns>
		int Run(int workspaceId, int scriptId, List<ScriptInput> inputs = null);
	}
}
