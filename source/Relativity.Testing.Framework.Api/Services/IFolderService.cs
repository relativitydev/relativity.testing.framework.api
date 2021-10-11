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
		/// Deletes unused (empty) folders from the workspace.
		/// </summary>
		/// <param name="workspaceArtifactID">The ArtifactID of the workspace.</param>
		/// <returns>[Query Result](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.QueryResult-1.html) that lists the deleted folders.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactId = 1015427;
		/// QueryResult&lt;Artifact&gt; = _folderService.DeleteUnused(workspaceArtifactId);
		/// </code>
		/// </example>
		QueryResult<Artifact> DeleteUnused(int workspaceArtifactID);

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
