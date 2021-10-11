using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the Folder API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IFolderService _folderService = relativityFacade.Resolve&lt;IFolderService&gt;();
	/// </code>
	/// </example>
	public interface IFolderService
	{
		/// <summary>
		/// Queries for unstructured list of [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html)s.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace.</param>
		/// <param name="query">The query that may be an empty query or include conditions.</param>
		/// <param name="length">Indicates the number of returned results. The default value is 0 for length, and the number of returned results defaults to 10000.</param>
		/// <returns>A [QueryResult](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.QueryResult-1.html) with a list of all folders in the workspace that are available to requesting user.</returns>
		/// <example> This example shows how to query for folder with given Name.
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// var query = new Query
		/// {
		/// 	Condition = "'Name' == 'Test'"
		/// };
		/// Folder folderWithTestName = _folderService.Get(workspaceArtifactId, query, 1).Results.FirstOrDefault();
		/// </code>
		/// </example>
		/// <example> This example shows how to query for all folders in the workspace.
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// var query = new Query();
		/// QueryResult&lt;NamedArtifact&gt; folders = _folderService.Get(workspaceArtifactId, query)
		/// </code>
		/// </example>
		QueryResult<NamedArtifact> Query(int workspaceArtifactID, Query query, int length = 0);

		/// <summary>
		/// Gets the root [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html) of the workspace.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace.</param>
		/// <returns>The root [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html) of the workspace.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// Folder rootFolder = _folderService.Get(workspaceArtifactId);
		/// </code>
		/// </example>
		Folder GetWorkspaceRootFolder(int workspaceArtifactID);
	}
}
