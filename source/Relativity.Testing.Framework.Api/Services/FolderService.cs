using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class FolderService : IFolderService
	{
		private readonly IFolderQueryStrategy _queryStrategy;

		private readonly IFolderGetWorkspaceRootFolderStrategy _getWorkspaceRootFolderStrategy;

		public FolderService(
			IFolderQueryStrategy queryStrategy,
			IFolderGetWorkspaceRootFolderStrategy getWorkspaceRootFolderStrategy)
		{
			_queryStrategy = queryStrategy;
			_getWorkspaceRootFolderStrategy = getWorkspaceRootFolderStrategy;
		}

		public Folder GetWorkspaceRootFolder(int workspaceArtifactID)
			=> _getWorkspaceRootFolderStrategy.Get(workspaceArtifactID);

		public QueryResult<NamedArtifact> Query(int workspaceArtifactID, Query query, int length = 0)
			=> _queryStrategy.Query(workspaceArtifactID, query, length);
	}
}
