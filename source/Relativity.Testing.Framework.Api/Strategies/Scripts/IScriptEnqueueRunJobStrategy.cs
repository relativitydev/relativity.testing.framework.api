using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Enqueues a script run job.
	/// </summary>
	internal interface IScriptEnqueueRunJobStrategy
	{
		/// <summary>
		/// Enqueues a script run job.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and should be executed.</param>
		/// <param name="scriptId">The ArtifactID of the script to execute.</param>
		/// <param name="inputs">Inputs and their values for the script, empty by default.</param>
		/// <param name="timeZoneOffset">The time zone offset for the script, zero by default.</param>
		/// <returns>An <see cref="EnqueueRunJobResponse"/> describing the queued job.</returns>
		EnqueueRunJobResponse EnqueueRunJob(int workspaceId, int scriptId, List<ScriptInput> inputs = null, double timeZoneOffset = 0);
	}
}
