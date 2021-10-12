using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class FolderService : IFolderService
	{
		private readonly IFolderCreateStrategy _createStrategy;

		private readonly IFolderGetByIdStrategy _getByIdStrategy;

		private readonly IFolderQueryStrategy _queryStrategy;

		private readonly IFolderGetWorkspaceRootFolderStrategy _getWorkspaceRootFolderStrategy;

		private readonly IFolderGetSubfoldersStrategy _getSubfoldersStrategy;

		public FolderService(
			IFolderCreateStrategy createStrategy,
			IFolderGetByIdStrategy getByIdStrategy,
			IFolderQueryStrategy queryStrategy,
			IFolderGetWorkspaceRootFolderStrategy getWorkspaceRootFolderStrategy,
			IFolderGetSubfoldersStrategy getSubfoldersStrategy)
		{
			_createStrategy = createStrategy;
			_getByIdStrategy = getByIdStrategy;
			_queryStrategy = queryStrategy;
			_getWorkspaceRootFolderStrategy = getWorkspaceRootFolderStrategy;
			_getSubfoldersStrategy = getSubfoldersStrategy;
		}

		public Folder Create(int workspaceArtifactID, Folder folder)
			=> _createStrategy.Create(workspaceArtifactID, folder);

		public Folder Get(int workspaceArtifactID, int folderArtifactID, int? parentFolderArtifactID = null)
			=> _getByIdStrategy.Get(workspaceArtifactID, folderArtifactID, parentFolderArtifactID);

		public QueryResult<NamedArtifact> Query(int workspaceArtifactID, Query query, int length = 0)
			=> _queryStrategy.Query(workspaceArtifactID, query, length);

		public Folder GetWorkspaceRootFolder(int workspaceArtifactID)
			=> _getWorkspaceRootFolderStrategy.Get(workspaceArtifactID);

		public List<Folder> GetSubfolders(int workspaceArtifactID, int parentFolderArtifactID)
			=> _getSubfoldersStrategy.Get(workspaceArtifactID, parentFolderArtifactID);
	}
}
