using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceChangeGroupPermissionsStrategy : IWorkspaceChangeGroupPermissionsStrategy
	{
		private readonly IWorkspaceAddToGroupsStrategy _workspaceAddToGroupsStrategy;

		private readonly IWorkspaceGetGroupPermissionsStrategy _workspaceGetGroupPermissionsStrategy;

		private readonly IWorkspaceSetGroupPermissionsStrategy _workspaceSetGroupPermissionsStrategy;

		public WorkspaceChangeGroupPermissionsStrategy(
			IWorkspaceAddToGroupsStrategy workspaceAddToGroupsStrategy,
			IWorkspaceGetGroupPermissionsStrategy workspaceGetGroupPermissionsStrategy,
			IWorkspaceSetGroupPermissionsStrategy workspaceSetGroupPermissionsStrategy)
		{
			_workspaceAddToGroupsStrategy = workspaceAddToGroupsStrategy;
			_workspaceGetGroupPermissionsStrategy = workspaceGetGroupPermissionsStrategy;
			_workspaceSetGroupPermissionsStrategy = workspaceSetGroupPermissionsStrategy;
		}

		public void Set(int workspaceId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			Set(workspaceId, groupId, groupPermissionsChangeset);
		}

		public void Set(int workspaceId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			Set(workspaceId, groupName, groupPermissionsChangeset);
		}

		public void Set(int workspaceId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			_workspaceAddToGroupsStrategy.AddWorkspaceToGroups(workspaceId, groupId);

			GroupPermissions groupPermissions = _workspaceGetGroupPermissionsStrategy.Get(workspaceId, groupId);

			groupPermissionsChangeset.Execute(groupPermissions);

			_workspaceSetGroupPermissionsStrategy.Set(workspaceId, groupPermissions);
		}

		public void Set(int workspaceId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			_workspaceAddToGroupsStrategy.AddWorkspaceToGroups(workspaceId, groupName);

			GroupPermissions groupPermissions = _workspaceGetGroupPermissionsStrategy.Get(workspaceId, groupName);

			groupPermissionsChangeset.Execute(groupPermissions);

			_workspaceSetGroupPermissionsStrategy.Set(workspaceId, groupPermissions);
		}
	}
}
