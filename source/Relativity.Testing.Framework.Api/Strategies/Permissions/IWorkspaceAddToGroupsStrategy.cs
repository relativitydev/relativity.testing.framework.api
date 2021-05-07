namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of adding the workspace to the group.
	/// </summary>
	internal interface IWorkspaceAddToGroupsStrategy
	{
		/// <summary>
		/// Adds the workspace to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace IDs.</param>
		/// <param name="groupIds">The collection of group IDs.</param>
		void AddWorkspaceToGroups(int workspaceId, params int[] groupIds);

		/// <summary>
		/// Adds the workspace to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace IDs.</param>
		/// <param name="groupNames">The collection of group Names.</param>
		void AddWorkspaceToGroups(int workspaceId, params string[] groupNames);
	}
}
