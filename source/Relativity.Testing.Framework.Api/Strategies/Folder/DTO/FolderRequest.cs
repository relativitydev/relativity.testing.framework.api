using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class FolderRequest
	{
		public FolderRequest(int workspaceArtifactID, Folder folder)
		{
			WorkspaceArtifactID = workspaceArtifactID;
			Model = folder;
		}

		public int WorkspaceArtifactID { get; set; }

		public Folder Model { get; set; }
	}
}
