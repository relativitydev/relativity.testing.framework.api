using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class FolderService : IFolderService
	{
		private readonly IFolderUpdateStrategy _updateStrategy;

		private readonly IFolderGetWorkspaceRootFolderStrategy _getWorkspaceRootFolderStrategy;

		public FolderService(
			IFolderUpdateStrategy updateStrategy,
			IFolderGetWorkspaceRootFolderStrategy getWorkspaceRootFolderStrategy)
		{
			_updateStrategy = updateStrategy;
			_getWorkspaceRootFolderStrategy = getWorkspaceRootFolderStrategy;
		}

		public Folder GetWorkspaceRootFolder(int workspaceArtifactID)
			=> _getWorkspaceRootFolderStrategy.Get(workspaceArtifactID);

		public Folder Update(int workspaceArtifactID, Folder folder)
			=> _updateStrategy.Update(workspaceArtifactID, folder);
	}
}
