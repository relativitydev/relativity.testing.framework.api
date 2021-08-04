﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceAddToGroupsStrategy : IWorkspaceAddToGroupsStrategy
	{
		private readonly IGetByWorkspaceIdStrategy<GroupSelector> _groupSelectorGetByWorkspaceIdStrategy;
		private readonly IAdminAddToGroupsStrategy _adminAddToGroupsStrategy;
		private readonly IWorkspaceAddRemoveGroupsStrategy _workspaceAddRemoveGroupsStrategy;

		public WorkspaceAddToGroupsStrategy(
			IGetByWorkspaceIdStrategy<GroupSelector> groupSelectorGetByWorkspaceIdStrategy,
			IAdminAddToGroupsStrategy adminAddToGroupsStrategy,
			IWorkspaceAddRemoveGroupsStrategy workspaceAddRemoveGroupsStrategy)
		{
			_groupSelectorGetByWorkspaceIdStrategy = groupSelectorGetByWorkspaceIdStrategy;
			_adminAddToGroupsStrategy = adminAddToGroupsStrategy;
			_workspaceAddRemoveGroupsStrategy = workspaceAddRemoveGroupsStrategy;
		}

		public async Task AddWorkspaceToGroupsAsync(int workspaceId, params int[] groupIds)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				GroupSelector groupSelector = _groupSelectorGetByWorkspaceIdStrategy.Get(workspaceId);

				foreach (int groupId in groupIds)
				{
					AddGroup(groupSelector, groupId);
				}

				await _workspaceAddRemoveGroupsStrategy.AddRemoveWorkspaceGroupsAsync(workspaceId, groupSelector).ConfigureAwait(false);
			}
		}

		public async Task AddWorkspaceToGroupsAsync(int workspaceId, params string[] groupNames)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				GroupSelector groupSelector = _groupSelectorGetByWorkspaceIdStrategy.Get(workspaceId);

				foreach (string groupName in groupNames)
				{
					AddGroup(groupSelector, groupName);
				}

				await _workspaceAddRemoveGroupsStrategy.AddRemoveWorkspaceGroupsAsync(workspaceId, groupSelector).ConfigureAwait(false);
			}
		}

		private void AddGroup(GroupSelector groupSelector, int groupId)
		{
			NamedArtifact groupToEnable = groupSelector.DisabledGroups.FirstOrDefault(x => x.ArtifactID == groupId);

			if (groupToEnable == null)
			{
				if (!groupSelector.EnabledGroups.Any(x => x.ArtifactID == groupId))
				{
					throw new InvalidOperationException($"Failed to find a group with Artifact ID={groupId} for workspace. " +
						$"This might be connected with mapping problem of ArtifactIds for groups in EnabledGroups of group selector for the workspace. " +
						$"Please try to use method that is using group Names instead of Ids to avoid this issue.");
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
