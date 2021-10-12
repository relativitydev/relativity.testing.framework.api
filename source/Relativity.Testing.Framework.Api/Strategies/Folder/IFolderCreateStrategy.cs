using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IFolderCreateStrategy
	{
		Folder Create(int workspaceArtifactID, Folder folder);
	}
}
