using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IFolderGetByIdStrategy
	{
		Folder Get(int workspaceArtifactID, int folderArtifactID, int? parentFolderArtifactID = null);
	}
}
