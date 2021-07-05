namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceGroupUserDTO : GroupDTO
	{
		public WorkspaceGroupUserDTO(int workspaceId, int groupId)
			: base(groupId)
		{
			WorkspaceArtifactId = workspaceId;
		}

		public int WorkspaceArtifactId { get; set; }
	}
}
