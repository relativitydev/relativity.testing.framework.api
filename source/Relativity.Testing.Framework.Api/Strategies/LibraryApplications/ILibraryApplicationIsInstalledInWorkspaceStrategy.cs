namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of determination whether the Library application is installed in given workspace.
	/// </summary>
	internal interface ILibraryApplicationIsInstalledInWorkspaceStrategy
	{
		/// <summary>
		/// Determines whether the Library application is installed in workspace.
		/// </summary>
		/// <param name="workspaceId">Workspace ID to check.</param>
		/// <param name="applicationId">Application ID to check.</param>
		/// <returns><see langword="true"/> if an application is installed in workspace; otherwise, <see langword="false"/>.</returns>
		bool IsInstalledInWorkspace(int workspaceId, int applicationId);
	}
}
