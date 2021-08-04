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
			=> _itemGroupSelectorGetStrategy.GetAsync(workspaceId, itemId).GetAwaiter().GetResult();

		public void AddRemoveItemGroups(int workspaceId, int itemId, GroupSelector groupSelector, bool enableLevelSecurity = true)
			=> _itemAddRemoveGroupsStrategy.AddRemoveItemGroupsAsync(workspaceId, itemId, groupSelector, enableLevelSecurity).GetAwaiter().GetResult();

		public void AddItemToGroup(int workspaceId, int itemId, string groupName)
			=> _itemAddToGroupsStrategy.AddItemToGroupsAsync(workspaceId, itemId, groupName).GetAwaiter().GetResult();

		public void AddItemToGroup(int workspaceId, int itemId, int groupId)
			=> _itemAddToGroupsStrategy.AddItemToGroupsAsync(workspaceId, itemId, groupId).GetAwaiter().GetResult();

		public void AddItemToGroups(int workspaceId, int itemId, params string[] groupNames)
			=> _itemAddToGroupsStrategy.AddItemToGroupsAsync(workspaceId, itemId, groupNames).GetAwaiter().GetResult();

		public void AddItemToGroups(int workspaceId, int itemId, params int[] groupIds)
			=> _itemAddToGroupsStrategy.AddItemToGroupsAsync(workspaceId, itemId, groupIds).GetAwaiter().GetResult();

		public void RemoveItemFromGroup(int workspaceId, int itemId, string groupName)
			=> _itemRemoveFromGroupsStrategy.RemoveItemFromGroupsAsync(workspaceId, itemId, groupName).GetAwaiter().GetResult();

		public void RemoveItemFromGroup(int workspaceId, int itemId, int groupId)
			=> _itemRemoveFromGroupsStrategy.RemoveItemFromGroupsAsync(workspaceId, itemId, groupId).GetAwaiter().GetResult();

		public void RemoveItemFromGroups(int workspaceId, int itemId, params string[] groupNames)
			=> _itemRemoveFromGroupsStrategy.RemoveItemFromGroupsAsync(workspaceId, itemId, groupNames).GetAwaiter().GetResult();

		public void RemoveItemFromGroups(int workspaceId, int itemId, params int[] groupIds)
			=> _itemRemoveFromGroupsStrategy.RemoveItemFromGroupsAsync(workspaceId, itemId, groupIds).GetAwaiter().GetResult();

		public GroupPermissions GetItemGroupPermissions(int workspaceId, int itemId, int groupId)
			=> _itemGetGroupPermissionsStrategy.GetAsync(workspaceId, itemId, groupId).GetAwaiter().GetResult();

		public void SetItemGroupPermissions(int workspaceId, int itemId, GroupPermissions groupPermissions)
			=> _itemSetGroupPermissionsStrategy.SetAsync(workspaceId, itemId, groupPermissions).GetAwaiter().GetResult();

		public void SetItemGroupPermissions(int workspaceId, int itemId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
			=> _itemChangeGroupPermissionsStrategy.SetAsync(workspaceId, itemId, groupName, groupPermissionsChangesetSetter).GetAwaiter().GetResult();

		public void SetItemGroupPermissions(int workspaceId, int itemId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
			=> _itemChangeGroupPermissionsStrategy.SetAsync(workspaceId, itemId, groupId, groupPermissionsChangesetSetter).GetAwaiter().GetResult();

		public void SetItemGroupPermissions(int workspaceId, int itemId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
			=> _itemChangeGroupPermissionsStrategy.SetAsync(workspaceId, itemId, groupName, groupPermissionsChangeset).GetAwaiter().GetResult();

		public void SetItemGroupPermissions(int workspaceId, int itemId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
			=> _itemChangeGroupPermissionsStrategy.SetAsync(workspaceId, itemId, groupId, groupPermissionsChangeset).GetAwaiter().GetResult();

		public ItemLevelSecurity GetItemLevelSecurity(int workspaceId, int itemId)
			=> _itemLevelSecurityGetById.Get(workspaceId, itemId);

		public void SetItemLevelSecurity(int workspaceId, int itemId, bool isEnabled)
			=> _itemLevelSecuritySet.SetAsync(workspaceId, itemId, isEnabled).GetAwaiter().GetResult();
	}
}
