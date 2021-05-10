using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ItemRemoveFromGroupsStrategy : IItemRemoveFromGroupsStrategy
	{
		private readonly IItemGroupSelectorGetStrategy _groupSelectorGetStrategy;

		private readonly IItemAddRemoveGroupsStrategy _itemAddRemoveGroupsStrategy;

		private readonly IObjectService _objectService;

		public ItemRemoveFromGroupsStrategy(
			IItemGroupSelectorGetStrategy groupSelectorGetStrategy,
			IItemAddRemoveGroupsStrategy itemAddRemoveGroupsStrategy,
			IObjectService objectService)
		{
			_groupSelectorGetStrategy = groupSelectorGetStrategy;
			_itemAddRemoveGroupsStrategy = itemAddRemoveGroupsStrategy;
			_objectService = objectService;
		}

		public void RemoveItemFromGroups(int workspaceId, int itemId, params string[] groupNames)
		{
			GroupSelector groupSelector = _groupSelectorGetStrategy.Get(workspaceId, itemId);

			foreach (var groupName in groupNames)
			{
				RemoveGroup(groupSelector, groupName);
			}

			_itemAddRemoveGroupsStrategy.AddRemoveItemGroups(workspaceId, itemId, groupSelector);
		}

		public void RemoveItemFromGroups(int workspaceId, int itemId, params int[] groupIds)
		{
			GroupSelector groupSelector = _groupSelectorGetStrategy.Get(workspaceId, itemId);

			foreach (var groupId in groupIds)
			{
				RemoveGroup(groupSelector, groupId);
			}

			_itemAddRemoveGroupsStrategy.AddRemoveItemGroups(workspaceId, itemId, groupSelector);
		}

		private void RemoveGroup(GroupSelector groupSelector, string groupName)
		{
			NamedArtifact groupToDisable = groupSelector.EnabledGroups.FirstOrDefault(x => x.Name == groupName);
			if (groupToDisable == null)
			{
				if (!groupSelector.DisabledGroups.Any(x => x.Name == groupName))
				{
					throw new InvalidOperationException($"Failed to find a group with {groupName} name for item.");
				}
			}
			else
			{
				groupSelector.EnabledGroups.Remove(groupToDisable);
				groupSelector.DisabledGroups.Add(groupToDisable);
			}
		}

		private void RemoveGroup(GroupSelector groupSelector, int groupId)
		{
			string groupName = _objectService.Query<Group>().
				Fetch("Name").
				Where(x => x.ArtifactID, groupId).
				First().Name;
			NamedArtifact groupToDisable = groupSelector.EnabledGroups.FirstOrDefault(x => x.Name == groupName);
			if (groupToDisable == null)
			{
				if (!groupSelector.DisabledGroups.Any(x => x.Name == groupName))
				{
					throw new InvalidOperationException($"Failed to find a group with {groupId} ID for item.");
				}
			}
			else
			{
				groupSelector.EnabledGroups.Remove(groupToDisable);
				groupSelector.DisabledGroups.Add(groupToDisable);
			}
		}
	}
}
