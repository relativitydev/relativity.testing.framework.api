using System;
using System.Linq;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AdminAddToGroupsStrategy : IAdminAddToGroupsStrategy
	{
		private readonly IGetStrategy<GroupSelector> _getStrategy;

		private readonly IAdminAddRemoveGroupsStrategy _adminAddRemoveGroupsStrategy;

		public AdminAddToGroupsStrategy(
			IGetStrategy<GroupSelector> getStrategy,
			IAdminAddRemoveGroupsStrategy adminAddRemoveGroupsStrategy)
		{
			_getStrategy = getStrategy;
			_adminAddRemoveGroupsStrategy = adminAddRemoveGroupsStrategy;
		}

		public async Task AddToGroupsAsync(params int[] groupIds)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				GroupSelector groupSelector = await _getStrategy.GetAsync().ConfigureAwait(false);

				foreach (int groupId in groupIds)
				{
					AddGroup(groupSelector, groupId);
				}

				await _adminAddRemoveGroupsStrategy.AddRemoveGroupsAsync(groupSelector).ConfigureAwait(false);
			}
		}

		public async Task AddToGroupsAsync(params string[] groupNames)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				GroupSelector groupSelector = await _getStrategy.GetAsync().ConfigureAwait(false);

				foreach (string groupName in groupNames)
				{
					AddGroup(groupSelector, groupName);
				}

				await _adminAddRemoveGroupsStrategy.AddRemoveGroupsAsync(groupSelector).ConfigureAwait(false);
			}
		}

		private void AddGroup(GroupSelector groupSelector, int groupId)
		{
			NamedArtifact groupToEnable = groupSelector.DisabledGroups.FirstOrDefault(x => x.ArtifactID == groupId);

			if (groupToEnable == null)
			{
				if (!groupSelector.EnabledGroups.Any(x => x.ArtifactID == groupId))
				{
					throw new InvalidOperationException($"Failed to find a group with Artifact ID={groupId} for workspace.");
				}
			}
			else
			{
				groupSelector.DisabledGroups.Remove(groupToEnable);
				groupSelector.EnabledGroups.Add(groupToEnable);
			}
		}

		private void AddGroup(GroupSelector groupSelector, string groupName)
		{
			NamedArtifact groupToEnable = groupSelector.DisabledGroups.FirstOrDefault(x => x.Name == groupName);

			if (groupToEnable == null)
			{
				if (!groupSelector.EnabledGroups.Any(x => x.Name == groupName))
				{
					throw new InvalidOperationException($"Failed to find a group with Name={groupName} for workspace.");
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
