using System;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ItemPermissionService : IItemPermissionService
	{
		private readonly IItemGroupSelectorGetStrategy _itemGroupSelectorGetStrategy;

		private readonly IItemAddRemoveGroupsStrategy _itemAddRemoveGroupsStrategy;

		private readonly IItemAddToGroupsStrategy _itemAddToGroupsStrategy;

		private readonly IItemRemoveFromGroupsStrategy _itemRemoveFromGroupsStrategy;

		private readonly IItemGetGroupPermissionsStrategy _itemGetGroupPermissionsStrategy;

		private readonly IItemSetGroupPermissionsStrategy _itemSetGroupPermissionsStrategy;

		private readonly IItemChangeGroupPermissionsStrategy _itemChangeGroupPermissionsStrategy;

		private readonly IGetWorkspaceEntityByIdStrategy<ItemLevelSecurity> _itemLevelSecurityGetById;

		private readonly IItemLevelSecuritySetStrategy _itemLevelSecuritySet;

		public ItemPermissionService(
			IItemGroupSelectorGetStrategy itemGroupSelectorGetStrategy,
			IItemAddRemoveGroupsStrategy itemAddRemoveGroupsStrategy,
			IItemAddToGroupsStrategy itemAddToGroupsStrategy,
			IItemRemoveFromGroupsStrategy itemRemoveFromGroupsStrategy,
			IItemGetGroupPermissionsStrategy itemGetGroupPermissionsStrategy,
			IItemSetGroupPermissionsStrategy itemSetGroupPermissionsStrategy,
			IItemChangeGroupPermissionsStrategy itemChangeGroupPermissionsStrategy,
			IGetWorkspaceEntityByIdStrategy<ItemLevelSecurity> itemLevelSecurityGetById,
			IItemLevelSecuritySetStrategy itemLevelSecuritySet)
		{
			_itemGroupSelectorGetStrategy = itemGroupSelectorGetStrategy;
			_itemAddRemoveGroupsStrategy = itemAddRemoveGroupsStrategy;
			_itemAddToGroupsStrategy = itemAddToGroupsStrategy;
			_itemRemoveFromGroupsStrategy = itemRemoveFromGroupsStrategy;
			_itemGetGroupPermissionsStrategy = itemGetGroupPermissionsStrategy;
			_itemSetGroupPermissionsStrategy = itemSetGroupPermissionsStrategy;
			_itemChangeGroupPermissionsStrategy = itemChangeGroupPermissionsStrategy;
			_itemLevelSecurityGetById = itemLevelSecurityGetById;
			_itemLevelSecuritySet = itemLevelSecuritySet;
		}

		public GroupSelector GetItemGroupSelector(int workspaceId, int itemId)
			=> _itemGroupSelectorGetStrategy.Get(workspaceId, itemId);

		public void AddRemoveItemGroups(int workspaceId, int itemId, GroupSelector groupSelector, bool enableLevelSecurity = true)
			=> _itemAddRemoveGroupsStrategy.AddRemoveItemGroups(workspaceId, itemId, groupSelector, enableLevelSecurity);

		public void AddItemToGroup(int workspaceId, int itemId, string groupName)
			=> _itemAddToGroupsStrategy.AddItemToGroups(workspaceId, itemId, groupName);

		public void AddItemToGroup(int workspaceId, int itemId, int groupId)
			=> _itemAddToGroupsStrategy.AddItemToGroups(workspaceId, itemId, groupId);

		public void AddItemToGroups(int workspaceId, int itemId, params string[] groupNames)
			=> _itemAddToGroupsStrategy.AddItemToGroups(workspaceId, itemId, groupNames);

		public void AddItemToGroups(int workspaceId, int itemId, params int[] groupIds)
			=> _itemAddToGroupsStrategy.AddItemToGroups(workspaceId, itemId, groupIds);

		public void RemoveItemFromGroup(int workspaceId, int itemId, string groupName)
			=> _itemRemoveFromGroupsStrategy.RemoveItemFromGroups(workspaceId, itemId, groupName);

		public void RemoveItemFromGroup(int workspaceId, int itemId, int groupId)
			=> _itemRemoveFromGroupsStrategy.RemoveItemFromGroups(workspaceId, itemId, groupId);

		public void RemoveItemFromGroups(int workspaceId, int itemId, params string[] groupNames)
			=> _itemRemoveFromGroupsStrategy.RemoveItemFromGroups(workspaceId, itemId, groupNames);

		public void RemoveItemFromGroups(int workspaceId, int itemId, params int[] groupIds)
			=> _itemRemoveFromGroupsStrategy.RemoveItemFromGroups(workspaceId, itemId, groupIds);

		public GroupPermissions GetItemGroupPermissions(int workspaceId, int itemId, int groupId)
			=> _itemGetGroupPermissionsStrategy.Get(workspaceId, itemId, groupId);

		public void SetItemGroupPermissions(int workspaceId, int itemId, GroupPermissions groupPermissions)
			=> _itemSetGroupPermissionsStrategy.Set(workspaceId, itemId, groupPermissions);

		public void SetItemGroupPermissions(int workspaceId, int itemId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
			=> _itemChangeGroupPermissionsStrategy.Set(workspaceId, itemId, groupName, groupPermissionsChangesetSetter);

		public void SetItemGroupPermissions(int workspaceId, int itemId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
			=> _itemChangeGroupPermissionsStrategy.Set(workspaceId, itemId, groupId, groupPermissionsChangesetSetter);

		public void SetItemGroupPermissions(int workspaceId, int itemId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
			=> _itemChangeGroupPermissionsStrategy.Set(workspaceId, itemId, groupName, groupPermissionsChangeset);

		public void SetItemGroupPermissions(int workspaceId, int itemId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
			=> _itemChangeGroupPermissionsStrategy.Set(workspaceId, itemId, groupId, groupPermissionsChangeset);

		public ItemLevelSecurity GetItemLevelSecurity(int workspaceId, int itemId)
			=> _itemLevelSecurityGetById.Get(workspaceId, itemId);

		public void SetItemLevelSecurity(int workspaceId, int itemId, bool isEnabled)
			=> _itemLevelSecuritySet.Set(workspaceId, itemId, isEnabled);
	}
}
