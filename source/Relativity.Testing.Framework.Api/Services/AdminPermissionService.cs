using System;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class AdminPermissionService : IAdminPermissionService
	{
		private readonly IGetStrategy<GroupSelector> _getStrategy;

		private readonly IAdminAddRemoveGroupsStrategy _adminAddRemoveGroupsStrategy;

		private readonly IAdminAddToGroupsStrategy _adminAddToGroupsStrategy;

		private readonly IAdminGetGroupPermissionsStrategy _adminGetGroupPermissionsStrategy;

		private readonly IAdminSetGroupPermissionsStrategy _adminSetGroupPermissionsStrategy;

		private readonly IAdminChangeGroupPermissionsStrategy _adminChangeGroupPermissionsStrategy;

		public AdminPermissionService(
			IGetStrategy<GroupSelector> getStrategy,
			IAdminAddRemoveGroupsStrategy adminAddRemoveGroupsStrategy,
			IAdminAddToGroupsStrategy adminAddToGroupsStrategy,
			IAdminGetGroupPermissionsStrategy adminGetGroupPermissionsStrategy,
			IAdminSetGroupPermissionsStrategy adminSetGroupPermissionsStrategy,
			IAdminChangeGroupPermissionsStrategy adminChangeGroupPermissionsStrategy)
		{
			_getStrategy = getStrategy;
			_adminAddRemoveGroupsStrategy = adminAddRemoveGroupsStrategy;
			_adminAddToGroupsStrategy = adminAddToGroupsStrategy;
			_adminGetGroupPermissionsStrategy = adminGetGroupPermissionsStrategy;
			_adminSetGroupPermissionsStrategy = adminSetGroupPermissionsStrategy;
			_adminChangeGroupPermissionsStrategy = adminChangeGroupPermissionsStrategy;
		}

		public GroupSelector GetAdminGroupSelector()
			=> _getStrategy.Get();

		public void AddRemoveGroups(GroupSelector selector)
			=> _adminAddRemoveGroupsStrategy.AddRemoveGroups(selector);

		public void AddToGroups(params int[] groupIds)
			=> _adminAddToGroupsStrategy.AddToGroups(groupIds);

		public void AddToGroups(params string[] groupNames)
			=> _adminAddToGroupsStrategy.AddToGroups(groupNames);

		public GroupPermissions GetAdminGroupPermissions(int groupId)
			=> _adminGetGroupPermissionsStrategy.Get(groupId);

		public GroupPermissions GetAdminGroupPermissions(string groupName)
			=> _adminGetGroupPermissionsStrategy.Get(groupName);

		public void SetAdminGroupPermissions(GroupPermissions groupPermissions)
			=> _adminSetGroupPermissionsStrategy.Set(groupPermissions);

		public void SetAdminGroupPermissions(int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
			=> _adminChangeGroupPermissionsStrategy.Set(groupId, groupPermissionsChangesetSetter);

		public void SetAdminGroupPermissions(string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
			=> _adminChangeGroupPermissionsStrategy.Set(groupName, groupPermissionsChangesetSetter);

		public void SetAdminGroupPermissions(int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
			=> _adminChangeGroupPermissionsStrategy.Set(groupId, groupPermissionsChangeset);

		public void SetAdminGroupPermissions(string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
			=> _adminChangeGroupPermissionsStrategy.Set(groupName, groupPermissionsChangeset);
	}
}
