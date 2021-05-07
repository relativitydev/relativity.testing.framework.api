using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the Library application API service.
	/// </summary>
	public interface ILibraryApplicationService
	{
		/// <summary>
		/// Installs library application from RAP file.
		/// </summary>
		/// <param name="pathToRap">Path to the RAP file.</param>
		/// <param name="options">Represents a request for creating or updating a Library Application.</param>
		/// <returns>Artifact ID of installed application.</returns>
		int InstallToLibrary(string pathToRap, LibraryApplicationInstallOptions options = null);

		/// <summary>
		/// Installs library application to given workspace from library.
		/// </summary>
		/// <param name="workspaceId">>Workspace ID to install the application to.</param>
		/// <param name="applicationId">Application ID to install.</param>
		void InstallToWorkspace(int workspaceId, int applicationId);

		/// <summary>
		/// Determines whether the library application is installed in workspace.
		/// </summary>
		/// <param name="workspaceId">Workspace ID to check.</param>
		/// <param name="applicationId">Application ID to check.</param>
		/// <returns><see langword="true"/> if an application is installed in workspace; otherwise, <see langword="false"/>.</returns>
		bool IsInstalledInWorkspace(int workspaceId, int applicationId);

		/// <summary>
		/// Gets the library application by the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The <see cref="LibraryApplication"/> entity or <see langword="null"/>.</returns>
		LibraryApplication Get(string name);
	}
}
