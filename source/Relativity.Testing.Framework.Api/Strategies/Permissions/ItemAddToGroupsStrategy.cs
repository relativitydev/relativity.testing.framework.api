using System;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task AddItemToGroupsAsync(int workspaceId, int itemId, params string[] groupNames)
		{
			GroupSelector groupSelector = await _groupSelectorGetStrategy.GetAsync(workspaceId, itemId).ConfigureAwait(false);

			foreach (var groupName in groupNames)
			{
				AddGroup(groupSelector, groupName);
			}

			await _itemAddRemoveGroupsStrategy.AddRemoveItemGroupsAsync(workspaceId, itemId, groupSelector).ConfigureAwait(false);
		}

		public async Task AddItemToGroupsAsync(int workspaceId, int itemId, params int[] groupIds)
		{
			GroupSelector groupSelector = await _groupSelectorGetStrategy.GetAsync(workspaceId, itemId).ConfigureAwait(false);

			foreach (var groupId in groupIds)
			{
				AddGroup(groupSelector, groupId);
			}

			await _itemAddRemoveGroupsStrategy.AddRemoveItemGroupsAsync(workspaceId, itemId, groupSelector).ConfigureAwait(false);
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
