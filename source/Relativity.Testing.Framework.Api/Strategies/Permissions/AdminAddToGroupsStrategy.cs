using System;
using System.Linq;
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

		public void AddToGroups(params int[] groupIds)
		{
			lock (GroupSelectorLocker.Locker)
			{
				GroupSelector groupSelector = _getStrategy.Get();

				foreach (int groupId in groupIds)
				{
					AddGroup(groupSelector, groupId);
				}

				_adminAddRemoveGroupsStrategy.AddRemoveGroups(groupSelector);
			}
		}

		public void AddToGroups(params string[] groupNames)
		{
			lock (GroupSelectorLocker.Locker)
			{
				GroupSelector groupSelector = _getStrategy.Get();

				foreach (string groupName in groupNames)
				{
					AddGroup(groupSelector, groupName);
				}

				_adminAddRemoveGroupsStrategy.AddRemoveGroups(groupSelector);
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
