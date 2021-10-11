using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class FolderService : IFolderService
	{
		private readonly IFolderDeleteUnusedStrategy _deleteUnusedStrategy;

		private readonly IFolderGetWorkspaceRootFolderStrategy _getWorkspaceRootFolderStrategy;

		public FolderService(
			IFolderDeleteUnusedStrategy deleteUnusedStrategy,
			IFolderGetWorkspaceRootFolderStrategy getWorkspaceRootFolderStrategy)
		{
			_deleteUnusedStrategy = deleteUnusedStrategy;
			_getWorkspaceRootFolderStrategy = getWorkspaceRootFolderStrategy;
		}

		public QueryResult<Artifact> DeleteUnused(int workspaceArtifactID)
			=> _deleteUnusedStrategy.Delete(workspaceArtifactID);

		public Folder GetWorkspaceRootFolder(int workspaceArtifactID)
			=> _getWorkspaceRootFolderStrategy.Get(workspaceArtifactID);
	}
}
