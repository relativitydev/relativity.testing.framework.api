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
		/// Folder rootFolder = _folderService.Get(workspaceArtifactId);
		/// </code>
		/// </example>
		Folder GetWorkspaceRootFolder(int workspaceArtifactID);
	}
}
