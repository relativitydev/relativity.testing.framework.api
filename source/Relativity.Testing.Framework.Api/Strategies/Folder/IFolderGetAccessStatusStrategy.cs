using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IFolderGetAccessStatusStrategy
	{
		FolderAccessStatus Get(int workspaceArtifactID, int folderArtifactID);
	}
}
