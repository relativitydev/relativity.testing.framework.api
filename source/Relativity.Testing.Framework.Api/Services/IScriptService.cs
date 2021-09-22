using System;
using System.Collections.Generic;
using System.Data;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the script API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IScriptService _scriptService = RelativityFacade.Resolve&lt;IScriptService&gt;();
	/// </code>
	/// </example>
	public interface IScriptService
	{
		/// <summary>
		/// Creates the specified script.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new script.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// const string action = "&lt;action returns=\"table\"&gt;&lt;![CDATA[ SELECT TOP(10) * FROM[eddsdbo].[Artifact]]]&gt;&lt;/action&gt;";
		///
		/// var script = new Script
		/// {
		/// 	Name = "My script Name",
		/// 	Description = "About my script",
		/// 	Category = "My category",
		/// 	ScriptBody = $"&lt;script&gt;{action}&lt;/script&gt;"
		/// };
		///
		/// Script result = _scriptService.Create(workspaceID, script);
		/// </code>
		/// </example>
		Script Create(int workspaceId, Script entity);

		/// <summary>
		/// Deletes the script by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the script.</param>
		/// <param name="entityId">The artifact ID of the script.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int scriptID  = 1987156;
		///
		/// _scriptService.Delete(workspaceID, scriptID);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the script by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get script.</param>
		/// <param name="entityId">The artifact ID of the script.</param>
		/// <returns>The <see cref="Script"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int scriptID  = 1987156;
		///
		/// Script script = _scriptService.Get(workspaceID, scriptID);
		/// </code>
		/// </example>
		Script Get(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified script.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update script.</param>
		/// <param name="entity">The entity to update.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int scriptID  = 1987156;
		///
		/// Script existingScript = _scriptService.Get(workspaceID, scriptID);
		/// existingScript.Name = "New Name";
		/// existingScript.Description = "New Description";
		///
		/// _scriptService.Update(workspaceID, existingScript);
		/// </code>
		/// </example>
		void Update(int workspaceId, Script entity);

		/// <summary>
		/// Gives a preview of what the exact SQL the script will run given a set of parameters.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists.</param>
		/// <param name="scriptId">The ArtifactID of the script to preview.</param>
		/// <param name="inputs">Inputs and their values for the script.</param>
		/// <param name="timeZoneOffset">The time zone offset for the script in hours.</param>
		/// <returns>SQL statement with the parameters inserted into it.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int scriptID  = 1987156;
		/// var scriptInputs = new List&lt;ScriptInput&gt;();
		///
		/// scriptInputs.Add(new ScriptInputClass()
		/// {
		/// 	ID = "workspaceArtifactId",
		/// 	Value = workspaceID
		/// });
		///
		/// string sqlStatment = _scriptService.Preview(workspaceID, scriptID, scriptInputs);
		/// </code>
		/// </example>
		string Preview(int workspaceId, int scriptId, List<ScriptInput> inputs = null, double timeZoneOffset = 0);

		/// <summary>
		/// Enqueues a script run job.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and should be executed.</param>
		/// <param name="scriptId">The ArtifactID of the script to execute.</param>
		/// <param name="inputs">Inputs and their values for the script, empty by default.</param>
		/// <param name="timeZoneOffset">The time zone offset for the script, zero by default.</param>
		/// <returns>An <see cref="EnqueueRunJobResponse"/> describing the queued job.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int scriptID  = 1987156;
		/// var scriptInputs = new List&lt;ScriptInput&gt;();
		///
		/// scriptInputs.Add(new ScriptInputClass()
		/// {
		/// 	ID = "workspaceArtifactId",
		/// 	Value = workspaceID
		/// });
		///
		/// EnqueueRunJobResponse response = _scriptService.EnqueueRunJob(workspaceID, scriptID, scriptInputs);
		/// </code>
		/// </example>
		EnqueueRunJobResponse EnqueueRunJob(int workspaceId, int scriptId, List<ScriptInput> inputs = null, double timeZoneOffset = 0);

		/// <summary>
		/// Retrieves a script run job.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and was executed.</param>
		/// <param name="runJobId">The ID of the script run job.</param>
		/// <returns>A <see cref="RunJob"/> describing the script run job.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int scriptID  = 1987156;
		///
		/// EnqueueRunJobResponse enqueueRunJobResponse = _scriptService.EnqueueRunJob(workspaceID, scriptID);
		/// RunJob runJobResponse = _scriptService.ReadRunJob(workspaceID, enqueueRunJobResponse.RunJobID);
		/// </code>
		/// </example>
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
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int scriptID  = 1987156;
		/// var actionQueryRequest = new ActionQueryRequest();
		///
		/// EnqueueRunJobResponse enqueueRunJobResponse = _scriptService.EnqueueRunJob(workspaceID, scriptID);
		/// RunJob runJobResponse = _scriptService.ReadRunJob(workspaceID, enqueueRunJobResponse.RunJobID);
		///
		/// var getAction = enqueueRunJobResponse.Actions.Find(x => x.Verb == "GET");
		/// var actionIndex = enqueueRunJobResponse.Actions.IndexOf(getAction);
		///
		/// var queryRequest = new ActionQueryRequest()
		/// {
		/// 	Condition = "'IsAlive' == false",
		/// 	ColumnNames = new List&lt;string&gt;() { "*" },
		/// 	Sorts = new List&lt;ActionColumnSort&gt;(),
		/// }
		///
		/// ActionResultsQueryResponse queryResponce = _scriptService.QueryActionJobResults(workspaceID, enqueueRunJobResponse.RunJobID, actionIndex, queryRequest);
		/// </code>
		/// </example>
		ActionResultsQueryResponse QueryActionJobResults(int workspaceId, Guid runJobId, int actionIndex, ActionQueryRequest actionQueryRequest = null, int start = 0, int length = 100);

		/// <summary>
		/// Executes a script with status action and parameters.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and should be executed.</param>
		/// <param name="scriptId">The ArtifactID of the script to execute.</param>
		/// <param name="inputs">Inputs and their values for the script.</param>
		/// <returns>The count of affected rows.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int scriptID  = 1987156;
		/// var scriptInputs = new List&lt;ScriptInput&gt;();
		///
		/// scriptInputs.Add(new ScriptInputClass()
		/// {
		/// 	ID = "workspaceArtifactId",
		/// 	Value = workspaceID
		/// });
		///
		/// int result = _scriptService.RunStatusAction(workspaceID, scriptID, scriptInputs);
		/// </code>
		/// </example>
		int RunStatusAction(int workspaceId, int scriptId, List<ScriptInput> inputs = null);

		/// <summary>
		/// Executes a script with table action and parameters.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where the script exists and should be executed.</param>
		/// <param name="scriptId">The ArtifactID of the script to execute.</param>
		/// <param name="inputs">Inputs and their values for the script.</param>
		/// <param name="actionQueryRequest">The query to execute. By default takes all fields without condition.</param>
		/// <returns>The table of results.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 1234567;
		/// int scriptID  = 1987156;
		/// var scriptInputs = new List&lt;ScriptInput&gt;();
		///
		/// scriptInputs.Add(new ScriptInputClass()
		/// {
		/// 	ID = "workspaceArtifactId",
		/// 	Value = workspaceID
		/// });
		///
		/// var queryRequest = new ActionQueryRequest()
		/// {
		/// 	Condition = "'IsAlive' == false",
		/// 	ColumnNames = new List&lt;string&gt;() { "*" },
		/// 	Sorts = new List&lt;ActionColumnSort&gt;(),
		/// }
		///
		/// DataTable result = _scriptService.RunTableAction(workspaceID, scriptID, scriptInputs, queryRequest);
		/// </code>
		/// </example>
		DataTable RunTableAction(int workspaceId, int scriptId, List<ScriptInput> inputs = null, ActionQueryRequest actionQueryRequest = null);

		/// <summary>
		/// Creates and/or enables a ScriptRunManager Agent to make sure that the environment can run scripts.
		/// </summary>
		/// <example>
		/// <code>
		/// _scriptService.EnsureEnvironmentCanRunScripts();
		/// </code>
		/// </example>
		void EnsureEnvironmentCanRunScripts();
	}
}
