using System;
using System.Collections.Generic;
using System.Data;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the script API service.
	/// </summary>
	public interface IScriptService
	{
		/// <summary>
		/// Creates the specified script.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new script.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Script Create(int workspaceId, Script entity);

		/// <summary>
		/// Deletes the script by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the script.</param>
		/// <param name="entityId">The artifact ID of the script.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the script by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get script.</param>
		/// <param name="entityId">The artifact ID of the script.</param>
		/// <returns>The <see cref="Script"/> entity or <see langword="null"/>.</returns>
		Script Get(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified script.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update script.</param>
		/// <param name="entity">The entity to update.</param>
		/// <returns>The updated entity.</returns>
		Script Update(int workspaceId, Script entity);

		/// <summary>
		/// Gives a preview of what the exact SQL the script will run given a set of parameters.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists.</param>
		/// <param name="scriptId">The ArtifactID of the script to preview.</param>
		/// <param name="inputs">Inputs and their values for the script.</param>
		/// <param name="timeZoneOffset">The time zone offset for the script in hours.</param>
		/// <returns>SQL statement with the parameters inserted into it.</returns>
		string Preview(int workspaceId, int scriptId, List<ScriptInput> inputs = null, double timeZoneOffset = 0);

		/// <summary>
		/// Enqueues a script run job.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and should be executed.</param>
		/// <param name="scriptId">The ArtifactID of the script to execute.</param>
		/// <param name="inputs">Inputs and their values for the script, empty by default.</param>
		/// <param name="timeZoneOffset">The time zone offset for the script, zero by default.</param>
		/// <returns>An <see cref="EnqueueRunJobResponse"/> describing the queued job.</returns>
		EnqueueRunJobResponse EnqueueRunJob(int workspaceId, int scriptId, List<ScriptInput> inputs = null, double timeZoneOffset = 0);

		/// <summary>
		/// Retrieves a script run job.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and was executed.</param>
		/// <param name="runJobId">The ID of the script run job.</param>
		/// <returns>A <see cref="RunJob"/> describing the script run job.</returns>
		RunJob ReadRunJob(int workspaceId, Guid runJobId);

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

		/// <summary>
		/// Executes a script with status action and parameters.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and should be executed.</param>
		/// <param name="scriptId">The ArtifactID of the script to execute.</param>
		/// <param name="inputs">Inputs and their values for the script.</param>
		/// <returns>The count of affected rows.</returns>
		int RunStatusAction(int workspaceId, int scriptId, List<ScriptInput> inputs = null);

		/// <summary>
		/// Executes a script with table action and parameters.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and should be executed.</param>
		/// <param name="scriptId">The ArtifactID of the script to execute.</param>
		/// <param name="inputs">Inputs and their values for the script.</param>
		/// <param name="actionQueryRequest">The query to execute. By default takes all fields without condition.</param>
		/// <returns>The table of results.</returns>
		DataTable RunTableAction(int workspaceId, int scriptId, List<ScriptInput> inputs = null, ActionQueryRequest actionQueryRequest = null);

		/// <summary>
		/// Creates and/or enables a ScriptRunManager Agent to make sure that the environment can run scripts.
		/// </summary>
		void EnsureEnvironmentCanRunScripts();
	}
}
