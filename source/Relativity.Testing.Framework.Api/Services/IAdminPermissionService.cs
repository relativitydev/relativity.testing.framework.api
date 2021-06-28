using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the admin permission API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _adminPermissionService = relativityFacade.Resolve&lt;IAdminPermissionService&gt;();
	/// </code>
	/// </example>
	public interface IAdminPermissionService
	{
		/// <summary>
		/// Gets the admin group selector.
		/// </summary>
		/// <returns>The <see cref="GroupSelector">group selector</see> entity.</returns>
		/// <example>
		/// <code>
		/// GroupSelector groupSelector = _adminPermissionService.GetAdminGroupSelector();
		/// </code>
		/// </example>
		GroupSelector GetAdminGroupSelector();

		/// <summary>
		/// Sets the group seletor (added/removed groups) to and from admin permissions.
		/// </summary>
		/// <param name="selector">The group selector.</param>
		/// <example>
		/// <code>
		/// GroupSelector groupSelector = _adminPermissionService.GetAdminGroupSelector();
		/// _adminPermissionService.AddRemoveGroups(groupSelector);
		/// </code>
		/// </example>
		void AddRemoveGroups(GroupSelector selector);

		/// <summary>
		/// Adds the group to admin permissions.
		/// </summary>
		/// <param name="groupIds">The collection of group IDs.</param>
		/// <example>
		/// <code>
		/// var groupIds = new int[] {123, 456, 789};
		/// _adminPermissionService.AddToGroups(groupIds);
		/// </code>
		/// </example>
		void AddToGroups(params int[] groupIds);

		/// <summary>
		/// Adds the group to admin permissions.
		/// </summary>
		/// <param name="groupNames">The collection of group Names.</param>
		/// <example>
		/// <code>
		/// var groupNames = new string[] {"group1"};
		/// _adminPermissionService.AddToGroups(groupNames);
		/// </code>
		/// </example>
		void AddToGroups(params string[] groupNames);

		/// <summary>
		/// Gets the admin permissions for a group.
		/// </summary>
		/// <param name="groupId">The group ID.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/>.</returns>
		/// <example>
		/// <code>
		/// var groudId = 89743;
		/// GroupPermissions groupPermissions = _adminPermissionService.GetAdminGroupPermissions(groudId);
		/// </code>
		/// </example>
		GroupPermissions GetAdminGroupPermissions(int groupId);

		/// <summary>
		/// Gets the admin permissions for a group.
		/// </summary>
		/// <param name="groupName">The group name.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/>.</returns>
		/// <example>
		/// <code>
		/// var groupName = "Group1";
		/// GroupPermissions groupPermissions = _adminPermissionService.GetAdminGroupPermissions(groupName);
		/// </code>
		/// </example>
		GroupPermissions GetAdminGroupPermissions(string groupName);

		/// <summary>
		/// Sets the admin permissions for a group.
		/// </summary>
		/// <param name="groupPermissions">The admin permissions for a group.</param>
		/// <example>
		/// <code>
		/// int workspaceId = 23123;
		/// int groupId = 93452;
		/// GroupPermissions groupPermissions = Facade.Resolve&lt;IWorkspacePermissionService&gt;().GetWorkspaceGroupPermissions(workspaceId, groupId);
		/// _adminPermissionService.SetAdminGroupPermissions(groupPermissions);
		/// </code>
		/// </example>
		void SetAdminGroupPermissions(GroupPermissions groupPermissions);

		/// <summary>
		/// Sets the admin permissions for a group using changeset setter.
		/// </summary>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		/// <example>
		/// <code>
		/// const string permissionName = "Object Rule";
		/// int groupId = 93452;
		/// _adminPermissionService.SetAdminGroupPermissions(groupId, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.ViewEdit));
		/// </code>
		/// </example>
		void SetAdminGroupPermissions(int groupId, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the admin permissions for a group using changeset.
		/// </summary>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		/// /// <example>
		/// <code>
		/// int groupId = 93452;
		/// GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
		/// _adminPermissionService.SetAdminGroupPermissions(groupId, groupPermissionsChangeset);
		/// </code>
		/// </example>
		void SetAdminGroupPermissions(int groupId, GroupPermissionsChangeset groupPermissionsChangeset);

		/// <summary>
		/// Sets the admin permissions for a group using changeset setter.
		/// </summary>
		/// <param name="groupName">The group name.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		/// <example>
		/// <code>
		/// const string permissionName = "Object Rule";
		/// string groupName = "Group1";
		/// _adminPermissionService.SetAdminGroupPermissions(groupName, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.ViewEdit));
		/// </code>
		/// </example>
		void SetAdminGroupPermissions(string groupName, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the admin permissions for a group using changeset.
		/// </summary>
		/// <param name="groupName">The group name.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		/// <example>
		/// <code>
		/// string groupName = "Group1";
		/// GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
		/// _adminPermissionService.SetAdminGroupPermissions(groupName, groupPermissionsChangeset);
		/// </code>
		/// </example>
		void SetAdminGroupPermissions(string groupName, GroupPermissionsChangeset groupPermissionsChangeset);
	}
}
