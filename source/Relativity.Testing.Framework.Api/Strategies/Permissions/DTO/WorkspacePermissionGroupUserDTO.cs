namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspacePermissionGroupUserDTO : PermissionGroupDTO
	{
		public WorkspacePermissionGroupUserDTO(int workspaceId, int groupId)
			: base(groupId)
		{
			WorkspaceArtifactId = workspaceId;
		}

		public int WorkspaceArtifactId { get; set; }
	}
}
