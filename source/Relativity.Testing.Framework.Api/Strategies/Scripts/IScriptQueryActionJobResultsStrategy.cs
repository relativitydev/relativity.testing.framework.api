using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Queries the results of a completed action job.
	/// </summary>
	internal interface IScriptQueryActionJobResultsStrategy
	{
		/// <summary>
		/// Queries the results of a completed action job.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and was executed.</param>
		/// <param name="runJobId">The ID of the script run job.</param>
		/// <param name="actionIndex">The index of the action. This is the numeric index of the action in the script starting at 0.</param>
		/// <param name="actionQueryRequest">The query to execute. By default takes all fields without condition.</param>
		/// <param name="start">The result cursor. Default value is zero.</param>
		/// <param name="length">The maximum length of results to return. Default value is 100.</param>
		/// <returns>The action query's result in the form of an <see cref="ActionResultsQueryResponse"/>.</returns>
		ActionResultsQueryResponse QueryActionJobResults(int workspaceId, Guid runJobId, int actionIndex, ActionQueryRequest actionQueryRequest = null, int start = 0, int length = 100);
	}
}
