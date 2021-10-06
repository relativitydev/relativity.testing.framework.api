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
		/// <returns>A [RunJob](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.RunJob.html) describing the script run job.</returns>
		RunJob ReadRunJob(int workspaceId, Guid runJobId);
	}
}
