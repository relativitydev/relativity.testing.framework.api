using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderGetByIdStrategy : IFolderGetByIdStrategy
	{
		private readonly IFolderGetSubfoldersStrategy _folderGetSubfoldersStrategy;

		private readonly IFolderGetWorkspaceRootFolderStrategy _folderGetWorkspaceRootFolderStrategy;

		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		private readonly IArtifactIdValidator _artifactIdValidator;

		public FolderGetByIdStrategy(
			IFolderGetSubfoldersStrategy folderGetSubfoldersStrategy,
			IFolderGetWorkspaceRootFolderStrategy folderGetWorkspaceRootFolderStrategy,
			IWorkspaceIdValidator workspaceIdValidator,
			IArtifactIdValidator artifactIdValidator)
		{
			_folderGetSubfoldersStrategy = folderGetSubfoldersStrategy;
			_folderGetWorkspaceRootFolderStrategy = folderGetWorkspaceRootFolderStrategy;
			_workspaceIdValidator = workspaceIdValidator;
			_artifactIdValidator = artifactIdValidator;
		}

		public Folder Get(int workspaceArtifactID, int folderArtifactID, int? parentFolderArtifactID = null)
		{
			_workspaceIdValidator.Validate(workspaceArtifactID);
			_artifactIdValidator.Validate(folderArtifactID, "Folder");

			int parentFolderID = parentFolderArtifactID ?? _folderGetWorkspaceRootFolderStrategy.Get(workspaceArtifactID).ArtifactID;

			_artifactIdValidator.Validate(parentFolderID, "Parent Folder");

			List<Folder> result = _folderGetSubfoldersStrategy.Get(workspaceArtifactID, parentFolderID);

			return result.FirstOrDefault(folder => folder.ArtifactID == folderArtifactID);
		}
	}
}
