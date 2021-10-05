using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the workspace permission API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IWorkspacePermissionService _workspacePermissionService = relativityFacade.Resolve&lt;IWorkspacePermissionService&gt;();
	/// </code>
	/// </example>
	public interface IWorkspacePermissionService
	{
		/// <summary>
		/// Gets the group selector of specified workspace.
		/// </summary>
		/// <param name="workspaceId">The case artifact ID of workspace.</param>
		/// <returns>The group selector entity.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		///
		/// GroupSelector groupSelector =  _workspacePermissionService.GetWorkspaceGroupSelector(workspaceID);
		/// </code>
		/// </example>
		GroupSelector GetWorkspaceGroupSelector(int workspaceId);

		/// <summary>
		/// Add and remove groups to and from admin permissions.
		/// </summary>
		/// <param name="workspaceId">The case artifact ID of workspace.</param>
		/// <param name="groupSelector">The selector of enabled/disabled groups for an entity.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		///
		/// GroupSelector selector =  _workspacePermissionService.GetWorkspaceGroupSelector(workspaceID);
		///
		/// var disabledGroup = selector.DisabledGroups.Last();
		/// selector.DisabledGroups.RemoveAll(x => x.ArtifactID == disabledGroup.ArtifactID);
		/// selector.EnabledGroups.Add(disabledGroup);
		///
		/// _workspacePermissionService.AddRemoveWorkspaceGroups(workspaceID, selector);
		/// </code>
		/// </example>
		void AddRemoveWorkspaceGroups(int workspaceId, GroupSelector groupSelector);

		/// <summary>
		/// Adds the workspace to the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int groupID = 654321;
		///
		/// _workspacePermissionService.AddWorkspaceToGroup(workspaceID, groupID);
		/// </code>
		/// </example>
		void AddWorkspaceToGroup(int workspaceId, int groupId);

		/// <summary>
		/// Adds the workspace to the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// string groupName = "Group Name";
		///
		/// _workspacePermissionService.AddWorkspaceToGroup(workspaceID, groupName);
		/// </code>
		/// </example>
		void AddWorkspaceToGroup(int workspaceId, string groupName);

		/// <summary>
		/// Adds the workspace to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupIds">The group IDs.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int[] groups = new[] {987654, 654321};
		///
		/// _workspacePermissionService.AddWorkspaceToGroups(workspaceID, groups);
		/// </code>
		/// </example>
		void AddWorkspaceToGroups(int workspaceId, params int[] groupIds);

		/// <summary>
		/// Adds the workspace to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupNames">The group Names.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// string[] groups = new[] {"First Group", "Second Group"};
		///
		/// _workspacePermissionService.AddWorkspaceToGroups(workspaceID, groups);
		/// </code>
		/// </example>
		void AddWorkspaceToGroups(int workspaceId, params string[] groupNames);

		/// <summary>
		/// Gets the the group permissions for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/> or <see langword="null"/> if the workspace is not added to the group.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int groupID = 654321;
		///
		/// GroupPermissions permissions = _workspacePermissionService.GetWorkspaceGroupPermissions(workspaceID, groupID);
		/// </code>
		/// </example>
		GroupPermissions GetWorkspaceGroupPermissions(int workspaceId, int groupId);

		/// <summary>
		/// Gets the the group permissions for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/> or <see langword="null"/> if the workspace is not added to the group.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// string groupName = "Group Name";
		///
		/// GroupPermissions permissions = _workspacePermissionService.GetWorkspaceGroupPermissions(workspaceID, groupName);
		/// </code>
		/// </example>
		GroupPermissions GetWorkspaceGroupPermissions(int workspaceId, string groupName);

		/// <summary>
		/// Sets the group permissions for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupPermissions">The group permissions.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// string groupName = "Group Name";
		///
		/// GroupPermissions permissions = _workspacePermissionService.GetWorkspaceGroupPermissions(workspaceID, groupName);
		/// permissions.ObjectPermissions[0].ViewSelected = true;
		///
		/// _workspacePermissionService.SetWorkspaceGroupPermissions(workspaceID, permissions);
		/// </code>
		/// </example>
		void SetWorkspaceGroupPermissions(int workspaceId, GroupPermissions groupPermissions);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset setter.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int groupID = 654321;
		/// string permissionName = "Object Rule";
		///
		/// _workspacePermissionService.SetWorkspaceGroupPermissions(workspaceID, groupID, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View);
		/// </code>
		/// </example>
		void SetWorkspaceGroupPermissions(int workspaceId, int groupId, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset setter.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// string groupName = "Group Name";
		/// string permissionName = "Object Rule";
		///
		/// _workspacePermissionService.SetWorkspaceGroupPermissions(workspaceID, groupName, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View));
		/// </code>
		/// </example>
		void SetWorkspaceGroupPermissions(int workspaceId, string groupName, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int groupID = 654321;
		/// string permissionName = "Object Rule";
		///
		/// var groupPermissionsChangeset = new GroupPermissionsChangeset();
		/// groupPermissionsChangeset.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View);
		///
		/// _workspacePermissionService.SetWorkspaceGroupPermissions(workspaceID, groupID, groupPermissionsChangeset);
		/// </code>
		/// </example>
		void SetWorkspaceGroupPermissions(int workspaceId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// string groupName = "Group Name";
		///
		/// var groupPermissionsChangeset = new GroupPermissionsChangeset();
		/// groupPermissionsChangeset.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View);
		///
		/// _workspacePermissionService.SetWorkspaceGroupPermissions(workspaceID, groupName, groupPermissionsChangeset);
		/// </code>
		/// </example>
		void SetWorkspaceGroupPermissions(int workspaceId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset);
	}
}
