using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the admin permission API service.
	/// </summary>
	public interface IAdminPermissionService
	{
		/// <summary>
		/// Gets the admin group selector.
		/// </summary>
		/// <returns>The group selector entity.</returns>
		GroupSelector GetAdminGroupSelector();

		/// <summary>
		/// Sets the group seletor (added/removed groups) to and from admin permissions.
		/// </summary>
		/// <param name="selector">The group selector.</param>
		void AddRemoveGroups(GroupSelector selector);

		/// <summary>
		/// Adds the group to admin permissions.
		/// </summary>
		/// <param name="groupIds">The collection of group IDs.</param>
		void AddToGroups(params int[] groupIds);

		/// <summary>
		/// Adds the group to admin permissions.
		/// </summary>
		/// <param name="groupNames">The collection of group Names.</param>
		void AddToGroups(params string[] groupNames);

		/// <summary>
		/// Gets the admin permissions for a group.
		/// </summary>
		/// <param name="groupId">The group ID.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/>.</returns>
		GroupPermissions GetAdminGroupPermissions(int groupId);

		/// <summary>
		/// Gets the admin permissions for a group.
		/// </summary>
		/// <param name="groupName">The group name.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/>.</returns>
		GroupPermissions GetAdminGroupPermissions(string groupName);

		/// <summary>
		/// Sets the admin permissions for a group.
		/// </summary>
		/// <param name="groupPermissions">The admin permissions for a group.</param>
		void SetAdminGroupPermissions(GroupPermissions groupPermissions);

		/// <summary>
		/// Sets the admin permissions for a group using changeset setter.
		/// </summary>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		void SetAdminGroupPermissions(int groupId, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the admin permissions for a group using changeset.
		/// </summary>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		void SetAdminGroupPermissions(int groupId, GroupPermissionsChangeset groupPermissionsChangeset);

		/// <summary>
		/// Sets the admin permissions for a group using changeset setter.
		/// </summary>
		/// <param name="groupName">The group name.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		void SetAdminGroupPermissions(string groupName, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the admin permissions for a group using changeset.
		/// </summary>
		/// <param name="groupName">The group name.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		void SetAdminGroupPermissions(string groupName, GroupPermissionsChangeset groupPermissionsChangeset);
	}
}
