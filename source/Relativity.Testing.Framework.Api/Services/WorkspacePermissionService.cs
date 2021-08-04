using System;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class WorkspacePermissionService : IWorkspacePermissionService
	{
		private readonly IGetByWorkspaceIdStrategy<GroupSelector> _groupSelectorGetByWorkspaceIdStrategy;

		private readonly IWorkspaceAddRemoveGroupsStrategy _workspaceAddRemoveGroupsStrategy;

		private readonly IWorkspaceAddToGroupsStrategy _workspaceAddToGroupsStrategy;

		private readonly IWorkspaceGetGroupPermissionsStrategy _workspaceGetGroupPermissionsStrategy;

		private readonly IWorkspaceSetGroupPermissionsStrategy _workspaceSetGroupPermissionsStrategy;

		private readonly IWorkspaceChangeGroupPermissionsStrategy _workspaceChangeGroupPermissionsStrategy;

		public WorkspacePermissionService(
			IGetByWorkspaceIdStrategy<GroupSelector> groupSelectorGetByWorkspaceIdStrategy,
			IWorkspaceAddRemoveGroupsStrategy workspaceAddRemoveGroupsStrategy,
			IWorkspaceAddToGroupsStrategy workspaceAddToGroupsStrategy,
			IWorkspaceGetGroupPermissionsStrategy workspaceGetGroupPermissionsStrategy,
			IWorkspaceSetGroupPermissionsStrategy workspaceSetGroupPermissionsStrategy,
			IWorkspaceChangeGroupPermissionsStrategy workspaceChangeGroupPermissionsStrategy)
		{
			_groupSelectorGetByWorkspaceIdStrategy = groupSelectorGetByWorkspaceIdStrategy;
			_workspaceAddRemoveGroupsStrategy = workspaceAddRemoveGroupsStrategy;
			_workspaceAddToGroupsStrategy = workspaceAddToGroupsStrategy;
			_workspaceGetGroupPermissionsStrategy = workspaceGetGroupPermissionsStrategy;
			_workspaceSetGroupPermissionsStrategy = workspaceSetGroupPermissionsStrategy;
			_workspaceChangeGroupPermissionsStrategy = workspaceChangeGroupPermissionsStrategy;
		}

		public GroupSelector GetWorkspaceGroupSelector(int workspaceId)
			=> _groupSelectorGetByWorkspaceIdStrategy.Get(workspaceId);

		public void AddRemoveWorkspaceGroups(int workspaceId, GroupSelector groupSelector)
			=> _workspaceAddRemoveGroupsStrategy.AddRemoveWorkspaceGroupsAsync(workspaceId, groupSelector).GetAwaiter().GetResult();

		public void AddWorkspaceToGroup(int workspaceId, int groupId)
			=> _workspaceAddToGroupsStrategy.AddWorkspaceToGroupsAsync(workspaceId, groupId).GetAwaiter().GetResult();

		public void AddWorkspaceToGroup(int workspaceId, string groupName)
			=> _workspaceAddToGroupsStrategy.AddWorkspaceToGroupsAsync(workspaceId, groupName).GetAwaiter().GetResult();

		public void AddWorkspaceToGroups(int workspaceId, params int[] groupIds)
			=> _workspaceAddToGroupsStrategy.AddWorkspaceToGroupsAsync(workspaceId, groupIds).GetAwaiter().GetResult();

		public void AddWorkspaceToGroups(int workspaceId, params string[] groupNames)
			=> _workspaceAddToGroupsStrategy.AddWorkspaceToGroupsAsync(workspaceId, groupNames).GetAwaiter().GetResult();

		public GroupPermissions GetWorkspaceGroupPermissions(int workspaceId, int groupId)
			=> _workspaceGetGroupPermissionsStrategy.GetAsync(workspaceId, groupId).GetAwaiter().GetResult();

		public GroupPermissions GetWorkspaceGroupPermissions(int workspaceId, string groupName)
			=> _workspaceGetGroupPermissionsStrategy.GetAsync(workspaceId, groupName).GetAwaiter().GetResult();

		public void SetWorkspaceGroupPermissions(int workspaceId, GroupPermissions groupPermissions)
			=> _workspaceSetGroupPermissionsStrategy.SetAsync(workspaceId, groupPermissions).GetAwaiter().GetResult();

		public void SetWorkspaceGroupPermissions(int workspaceId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
			=> _workspaceChangeGroupPermissionsStrategy.SetAsync(workspaceId, groupId, groupPermissionsChangeset).GetAwaiter().GetResult();

		public void SetWorkspaceGroupPermissions(int workspaceId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
			=> _workspaceChangeGroupPermissionsStrategy.SetAsync(workspaceId, groupName, groupPermissionsChangeset).GetAwaiter().GetResult();

		public void SetWorkspaceGroupPermissions(int workspaceId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
			=> _workspaceChangeGroupPermissionsStrategy.SetAsync(workspaceId, groupId, groupPermissionsChangesetSetter).GetAwaiter().GetResult();

		public void SetWorkspaceGroupPermissions(int workspaceId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
			=> _workspaceChangeGroupPermissionsStrategy.SetAsync(workspaceId, groupName, groupPermissionsChangesetSetter).GetAwaiter().GetResult();
	}
}
