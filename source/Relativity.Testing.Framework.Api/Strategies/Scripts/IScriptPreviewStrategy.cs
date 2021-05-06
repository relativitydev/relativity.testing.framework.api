using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IScriptPreviewStrategy
	{
		/// <summary>
		/// Gives a preview of what the exact SQL the script will run given a set of parameters.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists.</param>
		/// <param name="scriptId">The ArtifactID of the script to preview.</param>
		/// <param name="inputs">Inputs and their values for the script.</param>
		/// <param name="timeZoneOffset">The time zone offset for the script in hours.</param>
		/// <returns>SQL statement with the parameters inserted into it.</returns>
		string PreviewScript(int workspaceId, int scriptId, List<ScriptInput> inputs = null, double timeZoneOffset = 0);
	}
}
