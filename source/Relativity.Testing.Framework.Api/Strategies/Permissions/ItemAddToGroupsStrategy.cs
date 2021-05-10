using System;
using System.Linq;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ItemAddToGroupsStrategy : IItemAddToGroupsStrategy
	{
		private readonly IItemGroupSelectorGetStrategy _groupSelectorGetStrategy;

		private readonly IItemAddRemoveGroupsStrategy _itemAddRemoveGroupsStrategy;

		public ItemAddToGroupsStrategy(
			IItemGroupSelectorGetStrategy groupSelectorGetStrategy,
			IItemAddRemoveGroupsStrategy itemAddRemoveGroupsStrategy)
		{
			_groupSelectorGetStrategy = groupSelectorGetStrategy;
			_itemAddRemoveGroupsStrategy = itemAddRemoveGroupsStrategy;
		}

		public void AddItemToGroups(int workspaceId, int itemId, params string[] groupNames)
		{
			GroupSelector groupSelector = _groupSelectorGetStrategy.Get(workspaceId, itemId);

			foreach (var groupName in groupNames)
			{
				AddGroup(groupSelector, groupName);
			}

			_itemAddRemoveGroupsStrategy.AddRemoveItemGroups(workspaceId, itemId, groupSelector);
		}

		public void AddItemToGroups(int workspaceId, int itemId, params int[] groupIds)
		{
			GroupSelector groupSelector = _groupSelectorGetStrategy.Get(workspaceId, itemId);

			foreach (var groupId in groupIds)
			{
				AddGroup(groupSelector, groupId);
			}

			_itemAddRemoveGroupsStrategy.AddRemoveItemGroups(workspaceId, itemId, groupSelector);
		}

		private void AddGroup(GroupSelector groupSelector, string groupName)
		{
			NamedArtifact groupToEnable = groupSelector.DisabledGroups.FirstOrDefault(x => x.Name == groupName);

			if (groupToEnable == null)
			{
				if (!groupSelector.EnabledGroups.Any(x => x.Name == groupName))
				{
					throw new InvalidOperationException($"Failed to find a group with '{groupName}' name for item.");
				}
			}
			else
			{
				groupSelector.DisabledGroups.Remove(groupToEnable);
				groupSelector.EnabledGroups.Add(groupToEnable);
			}
		}

		private void AddGroup(GroupSelector groupSelector, int groupId)
		{
			NamedArtifact groupToEnable = groupSelector.DisabledGroups.FirstOrDefault(x => x.ArtifactID == groupId);

			if (groupToEnable == null)
			{
				if (!groupSelector.EnabledGroups.Any(x => x.ArtifactID == groupId))
				{
					throw new InvalidOperationException($"Failed to find a group with '{groupId}' ID for item.");
				}
			}
			else
			{
				groupSelector.DisabledGroups.Remove(groupToEnable);
				groupSelector.EnabledGroups.Add(groupToEnable);
			}
		}
	}
}
