using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the workspace permission API service.
	/// </summary>
	public interface IWorkspacePermissionService
	{
		/// <summary>
		/// Gets the group selector of specified workspace.
		/// </summary>
		/// <param name="workspaceId">The case artifact ID of workspace.</param>
		/// <returns>The group selector entity.</returns>
		GroupSelector GetWorkspaceGroupSelector(int workspaceId);

		/// <summary>
		/// Add and remove groups to and from admin permissions.
		/// </summary>
		/// <param name="workspaceId">The case artifact ID of workspace.</param>
		/// <param name="groupSelector">The selector of enabled/disabled groups for an entity.</param>
		void AddRemoveWorkspaceGroups(int workspaceId, GroupSelector groupSelector);

		/// <summary>
		/// Adds the workspace to the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		void AddWorkspaceToGroup(int workspaceId, int groupId);

		/// <summary>
		/// Adds the workspace to the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		void AddWorkspaceToGroup(int workspaceId, string groupName);

		/// <summary>
		/// Adds the workspace to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupIds">The group IDs.</param>
		void AddWorkspaceToGroups(int workspaceId, params int[] groupIds);

		/// <summary>
		/// Adds the workspace to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupNames">The group Names.</param>
		void AddWorkspaceToGroups(int workspaceId, params string[] groupNames);

		/// <summary>
		/// Gets the the group permissions for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/> or <see langword="null"/> if the workspace is not added to the group.</returns>
		GroupPermissions GetWorkspaceGroupPermissions(int workspaceId, int groupId);

		/// <summary>
		/// Gets the the group permissions for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/> or <see langword="null"/> if the workspace is not added to the group.</returns>
		GroupPermissions GetWorkspaceGroupPermissions(int workspaceId, string groupName);

		/// <summary>
		/// Sets the group permissions for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupPermissions">The group permissions.</param>
		void SetWorkspaceGroupPermissions(int workspaceId, GroupPermissions groupPermissions);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset setter.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		void SetWorkspaceGroupPermissions(int workspaceId, int groupId, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset setter.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		void SetWorkspaceGroupPermissions(int workspaceId, string groupName, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		void SetWorkspaceGroupPermissions(int workspaceId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		void SetWorkspaceGroupPermissions(int workspaceId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset);
	}
}
