using System.Collections.Generic;
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
		/// Creates the specified [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html) in specified workspace.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace.</param>
		/// <param name="folder">The [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html) to create. If ParentFolder does not have valid ArtifactID, the root folder of the repository will be set as the parent for created folder.</param>
		/// <returns>Created [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html).</returns>
		/// <example> This example shows how to create folder, which parent is the workspace root folder.
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// var folderToCreate = new Folder
		/// {
		/// 	Name = "Test Folder"
		/// };
		/// Folder createdFolder = _folderService.Create(workspaceArtifactId, folderToCreate)
		/// </code>
		/// </example>
		/// <example> This example shows how to create folder, which parent is folder with known ArtifactID.
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// int existingFolderArtifactID = 1015657;
		/// var folderToCreate = new Folder
		/// {
		/// 	Name = "Test Folder",
		/// 	ParentFolder = new NamedArtifact
		/// 	{
		/// 		ArtifactID = existingFolderArtifactID
		/// 	}
		/// };
		/// Folder createdFolder = _folderService.Create(workspaceArtifactId, folderToCreate);
		/// </code>
		/// </example>
		Folder Create(int workspaceArtifactID, Folder folder);

		/// <summary>
		/// Gets folder by ArtifactID. Parent Folder Artifact ID must be set if it's not the Workspace Root Folder (then it can be skipped).
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace.</param>
		/// <param name="folderArtifactID">The ArtifactID of the Folder.</param>
		/// <param name="parentFolderArtifactID">The ArtifactID of the parent folder.</param>
		/// <returns>The [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html).</returns>
		/// <example> This example shows how to get a folder by ArtifactID, which parent isn't the workspace root folder.
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// int existingFolderParentArtifactID = 1015656;
		/// int existingFolderArtifactID = 1015657;
		/// Folder folder = _folderService.Get(workspaceArtifactId, existingFolderArtifactID, existingFolderArtifactID);
		/// </code>
		/// </example>
		/// <example> This example shows how to get a folder by ArtifactID, which parent is workspace root folder.
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// int existingFolderArtifactID = 1015657;
		/// Folder folder = _folderService.Get(workspaceArtifactId, existingFolderArtifactID);
		/// </code>
		/// </example>
		Folder Get(int workspaceArtifactID, int folderArtifactID, int? parentFolderArtifactID = null);

		/// <summary>
		/// Deletes unused (empty) folders from the workspace.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace.</param>
		/// <returns>[QueryResult](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.QueryResult-1.html) that lists the deleted folders.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// QueryResult&lt;Artifact&gt; result = _folderService.DeleteUnused(workspaceArtifactId);
		/// </code>
		/// </example>
		QueryResult<Artifact> DeleteUnused(int workspaceArtifactID);

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
		/// QueryResult&lt;NamedArtifact&gt; folders = _folderService.Query(workspaceArtifactId, query)
		/// </code>
		/// </example>
		QueryResult<NamedArtifact> Query(int workspaceArtifactID, Query query, int length = 0);

		/// <summary>
		/// Updates [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html).
		/// </summary>
		/// <remarks>If the requests contains an Artifact ID that differs from ID for the original parent folder,
		/// the folder is moved to the parent folder specified in the request.
		/// If a parent folder isn't specified, the folder is moved to the root folder of the workspace.</remarks>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace.</param>
		/// <param name="folder">[Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html) to update.</param>
		/// <returns>Updated [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html).</returns>
		/// <example> This example shows how to update Name of the Folder.
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// int folderArtifactID = 10154324;
		/// int parentFolderArtifactID = 10154321;
		/// Folder folderToUpdate = _folderService.Get(workspaceArtifactId, folderArtifactID, parentFolderArtifactID);
		/// folderToUpdate.Name = "New Folder Name";
		/// Folder updatedFolder = _folderService.Update(workspaceArtifactId, folderToUpdate);
		/// </code>
		/// </example>
		/// <example> This example shows how to move folder to workspace root folder using Update method.
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// int existingFolderArtifactID = 10154324;
		/// Folder folderToUpdate = new Folder
		/// {
		/// 	Name = "Some Existing Folder Name",
		/// 	ArtifactID = existingFolderArtifactID
		/// };
		/// Folder updatedFolder = _folderService.Update(workspaceArtifactId, folderToUpdate);
		/// </code>
		/// </example>
		/// <example> This example shows how to move folder to some existing folder.
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// int existingFolderArtifactID = 10154324;
		/// int newParentFolderArtifactID = 101543223;
		/// Folder folderToUpdate = new Folder
		/// {
		/// 	Name = "Some Existing Folder Name",
		/// 	ArtifactID = existingFolderArtifactID,
		/// 	ParentFolder = new NamedArtifact
		/// 	{
		/// 		ArtifactID = newParentFolderArtifactID,
		/// 	}
		/// };
		/// Folder updatedFolder = _folderService.Update(workspaceArtifactId, folderToUpdate);
		/// </code>
		/// </example>
		Folder Update(int workspaceArtifactID, Folder folder);

		/// <summary>
		/// Gets the root [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html) of the workspace.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace.</param>
		/// <returns>The root [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html) of the workspace.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// Folder rootFolder = _folderService.GetWorkspaceRootFolder(workspaceArtifactId);
		/// </code>
		/// </example>
		Folder GetWorkspaceRootFolder(int workspaceArtifactID);

		/// <summary>
		/// Gets subfolders of given [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html).
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace.</param>
		/// <param name="parentFolderArtifactID">The ArtifactID of the parent folder.</param>
		/// <returns>The list of subfolders of the given [Folder](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Folder.html).</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// int existingFolderArtifactID = 1015657;
		/// List&lt;Folder&gt; subfolders = _folderService.GetSubfolders(workspaceArtifactId, existingFolderArtifactID);
		/// </code>
		/// </example>
		List<Folder> GetSubfolders(int workspaceArtifactID, int parentFolderArtifactID);
	}
}
