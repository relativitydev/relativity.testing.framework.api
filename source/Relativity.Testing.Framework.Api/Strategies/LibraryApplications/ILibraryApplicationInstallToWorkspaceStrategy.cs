namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of installation Library application to given workspace from library.
	/// </summary>
	internal interface ILibraryApplicationInstallToWorkspaceStrategy
	{
		/// <summary>
		/// Installs Library application to given workspace from library.
		/// </summary>
		/// <param name="workspaceId">>Workspace ID to install the application to.</param>
		/// <param name="applicationId">Application ID to install.</param>
		void InstallToWorkspace(int workspaceId, int applicationId);
	}
}
