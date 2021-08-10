using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the item permission API service.
	/// </summary>
	public interface IItemPermissionService
	{
		/// <summary>
		/// Gets the item group selector by workspace and item ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <returns>The item group selector.</returns>
		GroupSelector GetItemGroupSelector(int workspaceId, int itemId);

		/// <summary>
		/// Add and remove groups to and from item permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The case artifact ID of workspace.</param>
		/// <param name="groupSelector">The selector of enabled/disabled groups for an entity.</param>
		/// <param name="enableLevelSecurity">The value which indicating whether it should enable level security.
		/// By default true.</param>
		void AddRemoveItemGroups(int workspaceId, int itemId, GroupSelector groupSelector, bool enableLevelSecurity = true);

		/// <summary>
		/// Adds the item to the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupName">The group name.</param>
		void AddItemToGroup(int workspaceId, int itemId, string groupName);

		/// <summary>
		/// Adds the item to the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupId">The group ID.</param>
		void AddItemToGroup(int workspaceId, int itemId, int groupId);

		/// <summary>
		/// Adds the item to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupNames">The group names.</param>
		void AddItemToGroups(int workspaceId, int itemId, params string[] groupNames);

		/// <summary>
		/// Adds the item to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupIds">The group IDs.</param>
		void AddItemToGroups(int workspaceId, int itemId, params int[] groupIds);

		/// <summary>
		/// Removes the item from the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupName">The group name.</param>
		void RemoveItemFromGroup(int workspaceId, int itemId, string groupName);

		/// <summary>
		/// Removes the item from the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupId">The group id.</param>
		void RemoveItemFromGroup(int workspaceId, int itemId, int groupId);

		/// <summary>
		/// Removes the item from the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupNames">The group names.</param>
		void RemoveItemFromGroups(int workspaceId, int itemId, params string[] groupNames);

		/// <summary>
		/// Removes the item from the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupIds">The group ids.</param>
		void RemoveItemFromGroups(int workspaceId, int itemId, params int[] groupIds);

		/// <summary>
		/// Gets the the item group permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/> or <see langword="null"/>.</returns>
		GroupPermissions GetItemGroupPermissions(int workspaceId, int itemId, int groupId);

		/// <summary>
		/// Sets the item group permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupPermissions">The group permissions.</param>
		void SetItemGroupPermissions(int workspaceId, int itemId, GroupPermissions groupPermissions);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset setter.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupName">The group name.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		void SetItemGroupPermissions(int workspaceId, int itemId, string groupName, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset setter.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		void SetItemGroupPermissions(int workspaceId, int itemId, int groupId, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupName">The group name.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		void SetItemGroupPermissions(int workspaceId, int itemId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		void SetItemGroupPermissions(int workspaceId, int itemId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset);

		/// <summary>
		/// Gets the the group permissions for the item.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <returns>An instance of <see cref="ItemLevelSecurity"/>.</returns>
		ItemLevelSecurity GetItemLevelSecurity(int workspaceId, int itemId);

		/// <summary>
		/// Sets the level security for the specified item.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item id.</param>
		/// <param name="isEnabled">The value which indicating whether edit this item turned on.</param>
		void SetItemLevelSecurity(int workspaceId, int itemId, bool isEnabled);
	}
}
