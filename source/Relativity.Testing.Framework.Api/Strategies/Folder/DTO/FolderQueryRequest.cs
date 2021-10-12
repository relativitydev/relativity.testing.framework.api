using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderQueryRequest
	{
		public FolderQueryRequest(int workspaceArtifactID, Query query, int length)
		{
			WorkspaceArtifactID = workspaceArtifactID;
			Query = query;
			Length = length;
		}

		public int WorkspaceArtifactID { get; set; }

		public Query Query { get; set; }

		public int Length { get; set; }
	}
}
