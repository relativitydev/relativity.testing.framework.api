using System;
using System.Threading.Tasks;
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

		public Task SetAsync(int workspaceId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			return ActionSetAsync(workspaceId, groupId, groupPermissionsChangesetSetter);
		}

		public Task SetAsync(int workspaceId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			return ActionSetAsync(workspaceId, groupName, groupPermissionsChangesetSetter);
		}

		public Task SetAsync(int workspaceId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			return ActionSetAsync(workspaceId, groupId, groupPermissionsChangeset);
		}

		public Task SetAsync(int workspaceId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			return ActionSetAsync(workspaceId, groupName, groupPermissionsChangeset);
		}

		private async Task ActionSetAsync(int workspaceId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			await SetAsync(workspaceId, groupId, groupPermissionsChangeset).ConfigureAwait(false);
		}

		private async Task ActionSetAsync(int workspaceId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			await SetAsync(workspaceId, groupName, groupPermissionsChangeset).ConfigureAwait(false);
		}

		private async Task ActionSetAsync(int workspaceId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			await _workspaceAddToGroupsStrategy.AddWorkspaceToGroupsAsync(workspaceId, groupId).ConfigureAwait(false);

			GroupPermissions groupPermissions = await _workspaceGetGroupPermissionsStrategy.GetAsync(workspaceId, groupId).ConfigureAwait(false);

			groupPermissionsChangeset.Execute(groupPermissions);

			await _workspaceSetGroupPermissionsStrategy.SetAsync(workspaceId, groupPermissions).ConfigureAwait(false);
		}

		private async Task ActionSetAsync(int workspaceId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			await _workspaceAddToGroupsStrategy.AddWorkspaceToGroupsAsync(workspaceId, groupName).ConfigureAwait(false);

			GroupPermissions groupPermissions = await _workspaceGetGroupPermissionsStrategy.GetAsync(workspaceId, groupName).ConfigureAwait(false);

			groupPermissionsChangeset.Execute(groupPermissions);

			await _workspaceSetGroupPermissionsStrategy.SetAsync(workspaceId, groupPermissions).ConfigureAwait(false);
		}
	}
}
