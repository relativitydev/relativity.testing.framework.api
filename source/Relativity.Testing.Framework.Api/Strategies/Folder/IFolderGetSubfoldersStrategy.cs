using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IFolderGetSubfoldersStrategy
	{
		List<Folder> Get(int workspaceArtifactID, int parentFolderArtifactID);
	}
}
