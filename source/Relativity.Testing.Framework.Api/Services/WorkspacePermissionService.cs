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
			=> _workspaceAddRemoveGroupsStrategy.AddRemoveWorkspaceGroups(workspaceId, groupSelector);

		public void AddWorkspaceToGroup(int workspaceId, int groupId)
			=> _workspaceAddToGroupsStrategy.AddWorkspaceToGroups(workspaceId, groupId);

		public void AddWorkspaceToGroup(int workspaceId, string groupName)
			=> _workspaceAddToGroupsStrategy.AddWorkspaceToGroups(workspaceId, groupName);

		public void AddWorkspaceToGroups(int workspaceId, params int[] groupIds)
			=> _workspaceAddToGroupsStrategy.AddWorkspaceToGroups(workspaceId, groupIds);

		public void AddWorkspaceToGroups(int workspaceId, params string[] groupNames)
			=> _workspaceAddToGroupsStrategy.AddWorkspaceToGroups(workspaceId, groupNames);

		public GroupPermissions GetWorkspaceGroupPermissions(int workspaceId, int groupId)
			=> _workspaceGetGroupPermissionsStrategy.Get(workspaceId, groupId);

		public GroupPermissions GetWorkspaceGroupPermissions(int workspaceId, string groupName)
			=> _workspaceGetGroupPermissionsStrategy.Get(workspaceId, groupName);

		public void SetWorkspaceGroupPermissions(int workspaceId, GroupPermissions groupPermissions)
			=> _workspaceSetGroupPermissionsStrategy.Set(workspaceId, groupPermissions);

		public void SetWorkspaceGroupPermissions(int workspaceId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
			=> _workspaceChangeGroupPermissionsStrategy.Set(workspaceId, groupId, groupPermissionsChangeset);

		public void SetWorkspaceGroupPermissions(int workspaceId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
			=> _workspaceChangeGroupPermissionsStrategy.Set(workspaceId, groupName, groupPermissionsChangeset);

		public void SetWorkspaceGroupPermissions(int workspaceId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
			=> _workspaceChangeGroupPermissionsStrategy.Set(workspaceId, groupId, groupPermissionsChangesetSetter);

		public void SetWorkspaceGroupPermissions(int workspaceId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
			=> _workspaceChangeGroupPermissionsStrategy.Set(workspaceId, groupName, groupPermissionsChangesetSetter);
	}
}
