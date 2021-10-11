using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class FolderService : IFolderService
	{
		private readonly IFolderGetWorkspaceRootFolderStrategy _getWorkspaceRootFolderStrategy;

		public FolderService(IFolderGetWorkspaceRootFolderStrategy getWorkspaceRootFolderStrategy)
		{
			_getWorkspaceRootFolderStrategy = getWorkspaceRootFolderStrategy;
		}

		public Folder GetWorkspaceRootFolder(int workspaceArtifactID)
			=> _getWorkspaceRootFolderStrategy.Get(workspaceArtifactID);
	}
}
