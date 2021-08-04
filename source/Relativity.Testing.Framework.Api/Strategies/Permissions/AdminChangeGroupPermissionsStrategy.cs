using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AdminChangeGroupPermissionsStrategy : IAdminChangeGroupPermissionsStrategy
	{
		private readonly IAdminAddToGroupsStrategy _adminAddToGroupsStrategy;

		private readonly IAdminGetGroupPermissionsStrategy _adminGetGroupPermissionsStrategy;

		private readonly IAdminSetGroupPermissionsStrategy _adminSetGroupPermissionsStrategy;

		public AdminChangeGroupPermissionsStrategy(
			IAdminAddToGroupsStrategy adminAddToGroupsStrategy,
			IAdminGetGroupPermissionsStrategy adminGetGroupPermissionsStrategy,
			IAdminSetGroupPermissionsStrategy adminSetGroupPermissionsStrategy)
		{
			_adminAddToGroupsStrategy = adminAddToGroupsStrategy;
			_adminGetGroupPermissionsStrategy = adminGetGroupPermissionsStrategy;
			_adminSetGroupPermissionsStrategy = adminSetGroupPermissionsStrategy;
		}

		public Task SetAsync(int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			return ActionSetAsync(groupId, groupPermissionsChangesetSetter);
		}

		public Task SetAsync(int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			return ActionSetAsync(groupId, groupPermissionsChangeset);
		}

		public Task SetAsync(string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			return ActionSetAsync(groupName, groupPermissionsChangesetSetter);
		}

		public Task SetAsync(string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			return ActionSetAsync(groupName, groupPermissionsChangeset);
		}

		private async Task ActionSetAsync(int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			await SetAsync(groupId, groupPermissionsChangeset).ConfigureAwait(false);
		}

		private async Task ActionSetAsync(int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			await _adminAddToGroupsStrategy.AddToGroupsAsync(groupId).ConfigureAwait(false);

			GroupPermissions groupPermissions = await _adminGetGroupPermissionsStrategy.GetAsync(groupId).ConfigureAwait(false);

			groupPermissionsChangeset.Execute(groupPermissions);

			await _adminSetGroupPermissionsStrategy.SetAsync(groupPermissions).ConfigureAwait(false);
		}

		private async Task ActionSetAsync(string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			await SetAsync(groupName, groupPermissionsChangeset).ConfigureAwait(false);
		}

		private async Task ActionSetAsync(string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			await _adminAddToGroupsStrategy.AddToGroupsAsync(groupName).ConfigureAwait(false);

			int groupId = (await _adminGetGroupPermissionsStrategy.GetAsync(groupName).ConfigureAwait(false)).GroupID;

			GroupPermissions groupPermissions = await _adminGetGroupPermissionsStrategy.GetAsync(groupId).ConfigureAwait(false);

			groupPermissionsChangeset.Execute(groupPermissions);

			await _adminSetGroupPermissionsStrategy.SetAsync(groupPermissions).ConfigureAwait(false);
		}
	}
}
