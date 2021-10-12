using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IFolderQueryStrategy
	{
		QueryResult<NamedArtifact> Query(int workspaceArtifactID, Query query, int length = 0);
	}
}
