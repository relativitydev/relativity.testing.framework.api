using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class FolderService : IFolderService
	{
		private readonly IFolderDeleteUnusedStrategy _deleteUnusedStrategy;

		private readonly IFolderQueryStrategy _queryStrategy;

		private readonly IFolderGetWorkspaceRootFolderStrategy _getWorkspaceRootFolderStrategy;

		public FolderService(
			IFolderDeleteUnusedStrategy deleteUnusedStrategy,
			IFolderQueryStrategy queryStrategy,
			IFolderGetWorkspaceRootFolderStrategy getWorkspaceRootFolderStrategy)
		{
			_deleteUnusedStrategy = deleteUnusedStrategy;
			_queryStrategy = queryStrategy;
			_getWorkspaceRootFolderStrategy = getWorkspaceRootFolderStrategy;
		}

		public QueryResult<Artifact> DeleteUnused(int workspaceArtifactID)
			=> _deleteUnusedStrategy.Delete(workspaceArtifactID);

		public QueryResult<NamedArtifact> Query(int workspaceArtifactID, Query query, int length = 0)
			=> _queryStrategy.Query(workspaceArtifactID, query, length);

		public Folder GetWorkspaceRootFolder(int workspaceArtifactID)
			=> _getWorkspaceRootFolderStrategy.Get(workspaceArtifactID);
	}
}
