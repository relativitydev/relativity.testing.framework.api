using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IFolderUpdateStrategy
	{
		Folder Update(int workspaceArtifactID, Folder folder);
	}
}
