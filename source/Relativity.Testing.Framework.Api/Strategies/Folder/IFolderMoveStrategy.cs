using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IFolderMoveStrategy
	{
		FolderMoveResponse Move(int workspaceArtifactID, int folderArtifactID, int destinationFolderArtifactID);
	}
}
