using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IFolderGetExpandedNodesStrategy
	{
		List<Folder> Get(int workspaceArtifactID, List<int> expandedNodesArtifactIDs, int selectedFolderArtifactID = 0);
	}
}
