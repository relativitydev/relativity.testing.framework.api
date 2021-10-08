namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface ILibraryApplicationIsInstalledInWorkspaceStrategy
	{
		bool IsInstalledInWorkspace(int workspaceId, int applicationId);
	}
}
