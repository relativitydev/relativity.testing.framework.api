using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the Library application API service.
	/// </summary>
	/// <example>
	/// <code>
	/// ILibraryApplicationService _libraryApplicationService = RelativityFacade.Resolve&lt;ILibraryApplicationService&gt;();
	/// </code>
	/// </example>
	public interface ILibraryApplicationService
	{
		/// <summary>
		/// Installs library application from RAP file.
		/// </summary>
		/// <param name="pathToRap">Path to the RAP file.</param>
		/// <param name="options">Represents a request for creating or updating a Library Application.</param>
		/// <returns>Artifact ID of installed application.</returns>
		/// <example>
		/// <code>
		/// string pathToRap = "path/to/cutting/edge/app.rap";
		/// var options = new LibraryApplicationInstallOptions { IgnoreVersion = true };
		/// int applicationID = _libraryApplicationService.InstallToLibrary(pathToRap, options);
		/// </code>
		/// </example>
		int InstallToLibrary(string pathToRap, LibraryApplicationInstallOptions options = null);

		/// <summary>
		/// Installs library application to given workspace from library.
		/// </summary>
		/// <param name="workspaceId">>Workspace ID to install the application to.</param>
		/// <param name="applicationId">Application ID to install.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int applicationID = 1015427;
		/// _libraryApplicationService.InstallToWorkspace(workspaceID, applicationID);
		/// </code>
		/// </example>
		void InstallToWorkspace(int workspaceId, int applicationId);

		/// <summary>
		/// Determines whether the library application is installed in workspace.
		/// </summary>
		/// <param name="workspaceId">Workspace ID to check.</param>
		/// <param name="applicationId">Application ID to check.</param>
		/// <returns><see langword="true"/> if an application is installed in workspace; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// string applicationName = "CuttingEdgeAppName";
		/// bool isInstalled = _libraryApplicationService.IsInstalledInWorkspace(workspaceID, applicationName);
		/// </code>
		/// </example>
		bool IsInstalledInWorkspace(int workspaceId, int applicationId);

		/// <summary>
		/// Gets the library application by the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The [LibraryApplication](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.LibraryApplication.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// string applicationName = "CuttingEdgeAppName";
		/// LibraryApplication application = _libraryApplicationService.Get(applicationName);
		/// </code>
		/// </example>
		LibraryApplication Get(string name);

		/// <summary>
		/// Gets the library application by the specified ID.
		/// </summary>
		/// <param name="applicationId">The ArtifactID of the application.</param>
		/// <returns>The [LibraryApplication](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.LibraryApplication.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int applicationID = 1015427;
		/// LibraryApplication application = _libraryApplicationService.Get(applicationID);
		/// </code>
		/// </example>
		LibraryApplication Get(int applicationId);

		/// <summary>
		/// Gets the library application by the specified GUID.
		/// </summary>
		/// <param name="identifier">The Library application GUID identifier.</param>
		/// <returns>The [LibraryApplication](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.LibraryApplication.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Guid identifier = Guid.Parse("4c8836a0-5138-4fe4-a1fd-04b04e6730a7");
		/// LibraryApplication application = _libraryApplicationService.Get(identifier);
		/// </code>
		/// </example>
		LibraryApplication Get(Guid identifier);

		/// <summary>
		/// Delete the application from the library.
		/// </summary>
		/// <param name="applicationId">The ArtifactID of the application to delete from the library.</param>
		/// <example>
		/// <code>
		/// int applicationID = 1015427;
		/// _libraryApplicationService.DeleteFromLibrary(applicationID);
		/// </code>
		/// </example>
		void DeleteFromLibrary(int applicationId);
	}
}
