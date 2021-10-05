using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the item permission API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IItemPermissionService _itemPermissionService = relativityFacade.Resolve&lt;IItemPermissionService&gt;();
	/// </code>
	/// </example>
	public interface IItemPermissionService
	{
		/// <summary>
		/// Gets the item group selector by workspace and item ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <returns>The item group selector.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		///
		/// GroupSelector groupSelector =  _itemPermissionService.GetItemGroupSelector(workspaceID, itemID);
		/// </code>
		/// </example>
		GroupSelector GetItemGroupSelector(int workspaceId, int itemId);

		/// <summary>
		/// Add and remove groups to and from item permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The case artifact ID of workspace.</param>
		/// <param name="groupSelector">The selector of enabled/disabled groups for an entity.</param>
		/// <param name="enableLevelSecurity">The value which indicating whether it should enable level security.
		/// By default true.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// string groupName = "Some group name";
		///
		/// GroupSelector groupSelector =  _itemPermissionService.GetItemGroupSelector(workspaceID, itemID);
		/// var toBeEnabledGroup = selector.DisabledGroups.FirstOrDefault(x => x.Name == groupName);
		///
		/// if (toBeEnabledGroup != null)
		/// {
		/// 	selector.DisabledGroups.RemoveAll(x => x.ArtifactID == toBeEnabledGroup.ArtifactID);
		/// 	selector.EnabledGroups.Add(toBeEnabledGroup);
		///
		/// 	_itemPermissionService.AddRemoveItemGroups(workspaceID, itemID, groupSelector);
		/// }
		/// </code>
		/// </example>
		void AddRemoveItemGroups(int workspaceId, int itemId, GroupSelector groupSelector, bool enableLevelSecurity = true);

		/// <summary>
		/// Adds the item to the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupName">The group name.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// string groupName = "Some group name";
		///
		/// _itemPermissionService.AddItemToGroup(workspaceID, itemID, groupName);
		/// </code>
		/// </example>
		void AddItemToGroup(int workspaceId, int itemId, string groupName);

		/// <summary>
		/// Adds the item to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupNames">The group names.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// string[] groupNames = new string[] { "First group name", "Second gorup name"};
		///
		/// _itemPermissionService.AddItemToGroups(workspaceID, itemID, groupNames);
		/// </code>
		/// </example>
		void AddItemToGroups(int workspaceId, int itemId, params string[] groupNames);

		/// <summary>
		/// Removes the item from the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupName">The group name.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// string groupName = "Some group name";
		///
		/// _itemPermissionService.RemoveItemFromGroup(workspaceID, itemID, groupName);
		/// </code>
		/// </example>
		void RemoveItemFromGroup(int workspaceId, int itemId, string groupName);

		/// <summary>
		/// Removes the item from the group.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupId">The group id.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// int groupID = 765432;
		///
		/// _itemPermissionService.RemoveItemFromGroup(workspaceID, itemID, groupID);
		/// </code>
		/// </example>
		void RemoveItemFromGroup(int workspaceId, int itemId, int groupId);

		/// <summary>
		/// Removes the item from the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupNames">The group names.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// string[] groupNames = new string[] { "First group name", "Second gorup name"};
		///
		/// _itemPermissionService.RemoveItemFromGroups(workspaceID, itemID, groupNames);
		/// </code>
		/// </example>
		void RemoveItemFromGroups(int workspaceId, int itemId, params string[] groupNames);

		/// <summary>
		/// Removes the item from the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupIds">The group ids.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// int[] groupIds = new int[] { 123568, 785429};
		///
		/// _itemPermissionService.RemoveItemFromGroups(workspaceID, itemID, groupIds);
		/// </code>
		/// </example>
		void RemoveItemFromGroups(int workspaceId, int itemId, params int[] groupIds);

		/// <summary>
		/// Gets the the item group permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/> or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// int groupID = 765432;
		///
		/// GroupPermissions groupPermissions = _itemPermissionService.GetItemGroupPermissions(workspaceID, itemID, groupID);
		/// </code>
		/// </example>
		GroupPermissions GetItemGroupPermissions(int workspaceId, int itemId, int groupId);

		/// <summary>
		/// Sets the item group permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupPermissions">The group permissions.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// int groupID = 765432;
		///
		/// GroupPermissions groupPermissions = _itemPermissionService.GetItemGroupPermissions(workspaceID, itemID, groupID);
		/// if (groupPermissions != null)
		/// {
		/// 	groupPermissions.ObjectPermissions[0].EditEditable = false;
		/// 	groupPermissions.ObjectPermissions[0].AddEditable = false;
		/// 	groupPermissions.ObjectPermissions[0].EditSelected = false;
		/// }
		///
		/// _itemPermissionService.SetItemGroupPermissions(workspaceID, itemID, groupPermissions);
		/// </code>
		/// </example>
		void SetItemGroupPermissions(int workspaceId, int itemId, GroupPermissions groupPermissions);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset setter.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupName">The group name.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// string permissionName = "Document";
		/// string groupName = "Some group name";
		///
		/// _itemPermissionService.SetItemGroupPermissions(workspaceID, itemID, groupName, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View));
		/// </code>
		/// </example>
		void SetItemGroupPermissions(int workspaceId, int itemId, string groupName, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset setter.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// string permissionName = "Document";
		/// int groupID = 765432;
		///
		/// _itemPermissionService.SetItemGroupPermissions(workspaceID, itemID, groupID, x => x.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View));
		/// </code>
		/// </example>
		void SetItemGroupPermissions(int workspaceId, int itemId, int groupId, System.Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupName">The group name.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// string permissionName = "Document";
		/// string groupName = "Some group name";
		///
		/// var groupPermissionsChangeset = new GroupPermissionsChangeset();
		/// groupPermissionsChangeset.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View);
		///
		/// _itemPermissionService.SetItemGroupPermissions(workspaceID, itemID, groupName, groupPermissionsChangeset);
		/// </code>
		/// </example>
		void SetItemGroupPermissions(int workspaceId, int itemId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// string permissionName = "Document";
		/// int groupID = 765432;
		///
		/// var groupPermissionsChangeset = new GroupPermissionsChangeset();
		/// groupPermissionsChangeset.ObjectPermissions[permissionName].Set(ObjectPermissionKinds.View);
		///
		/// _itemPermissionService.SetItemGroupPermissions(workspaceID, itemID, groupID, groupPermissionsChangeset);
		/// </code>
		/// </example>
		void SetItemGroupPermissions(int workspaceId, int itemId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset);

		/// <summary>
		/// Gets the the group permissions for the item.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <returns>An instance of <see cref="ItemLevelSecurity"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		///
		/// ItemLevelSecurity itemLevelSecurity = _itemPermissionService.GetItemLevelSecurity(workspaceID, itemID);
		/// </code>
		/// </example>
		ItemLevelSecurity GetItemLevelSecurity(int workspaceId, int itemId);

		/// <summary>
		/// Sets the level security for the specified item.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item id.</param>
		/// <param name="isEnabled">The value which indicating whether edit this item turned on.</param>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int itemID = 654321;
		/// bool isEnabled = true;
		///
		/// _itemPermissionService.SetItemLevelSecurity(workspaceID, itemID, isEnabled);
		/// </code>
		/// </example>
		void SetItemLevelSecurity(int workspaceId, int itemId, bool isEnabled);
	}
}
