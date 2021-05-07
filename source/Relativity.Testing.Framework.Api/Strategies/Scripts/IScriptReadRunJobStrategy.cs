using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Retrieves a script run job.
	/// </summary>
	internal interface IScriptReadRunJobStrategy
	{
		/// <summary>
		/// Retrieves a script run job.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and was executed.</param>
		/// <param name="runJobId">The ID of the script run job.</param>
		/// <returns>A <see cref="RunJob"/> describing the script run job.</returns>
		RunJob ReadRunJob(int workspaceId, Guid runJobId);
	}
}
