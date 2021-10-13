﻿using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class FolderService : IFolderService
	{
		private readonly IFolderCreateStrategy _createStrategy;

		private readonly IFolderGetByIdStrategy _getByIdStrategy;

		private readonly IFolderDeleteUnusedStrategy _deleteUnusedStrategy;

		private readonly IFolderQueryStrategy _queryStrategy;

		private readonly IFolderUpdateStrategy _updateStrategy;

		private readonly IFolderGetWorkspaceRootFolderStrategy _getWorkspaceRootFolderStrategy;

		private readonly IFolderGetSubfoldersStrategy _getSubfoldersStrategy;

		private readonly IFolderGetAccessStatusStrategy _getAccessStatusStrategy;

		public FolderService(
			IFolderCreateStrategy createStrategy,
			IFolderGetByIdStrategy getByIdStrategy,
			IFolderDeleteUnusedStrategy deleteUnusedStrategy,
			IFolderUpdateStrategy updateStrategy,
			IFolderQueryStrategy queryStrategy,
			IFolderGetWorkspaceRootFolderStrategy getWorkspaceRootFolderStrategy,
			IFolderGetSubfoldersStrategy getSubfoldersStrategy,
			IFolderGetAccessStatusStrategy getAccessStatusStrategy)
		{
			_createStrategy = createStrategy;
			_getByIdStrategy = getByIdStrategy;
			_deleteUnusedStrategy = deleteUnusedStrategy;
			_updateStrategy = updateStrategy;
			_queryStrategy = queryStrategy;
			_getWorkspaceRootFolderStrategy = getWorkspaceRootFolderStrategy;
			_getSubfoldersStrategy = getSubfoldersStrategy;
			_getAccessStatusStrategy = getAccessStatusStrategy;
		}

		public Folder Create(int workspaceArtifactID, Folder folder)
			=> _createStrategy.Create(workspaceArtifactID, folder);

		public Folder Get(int workspaceArtifactID, int folderArtifactID, int? parentFolderArtifactID = null)
			=> _getByIdStrategy.Get(workspaceArtifactID, folderArtifactID, parentFolderArtifactID);

		public QueryResult<Artifact> DeleteUnused(int workspaceArtifactID)
			=> _deleteUnusedStrategy.Delete(workspaceArtifactID);

		public Folder Update(int workspaceArtifactID, Folder folder)
			=> _updateStrategy.Update(workspaceArtifactID, folder);

		public QueryResult<NamedArtifact> Query(int workspaceArtifactID, Query query, int length = 0)
			=> _queryStrategy.Query(workspaceArtifactID, query, length);

		public Folder GetWorkspaceRootFolder(int workspaceArtifactID)
			=> _getWorkspaceRootFolderStrategy.Get(workspaceArtifactID);

		public List<Folder> GetSubfolders(int workspaceArtifactID, int parentFolderArtifactID)
			=> _getSubfoldersStrategy.Get(workspaceArtifactID, parentFolderArtifactID);

		public FolderAccessStatus GetAccessStatus(int workspaceArtifactID, int folderArtifactID)
			=> _getAccessStatusStrategy.Get(workspaceArtifactID, folderArtifactID);
	}
}
