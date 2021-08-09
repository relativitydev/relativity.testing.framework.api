using System;
using System.Threading.Tasks;
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

#pragma warning disable S4136 // Method overloads should be grouped together
		public GroupSelector GetAdminGroupSelector()
		{
			return GetAdminGroupSelectorAsync().GetAwaiter().GetResult();
		}

		public async Task<GroupSelector> GetAdminGroupSelectorAsync()
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				return await _getStrategy.GetAsync().ConfigureAwait(false);
			}
		}

		public void AddRemoveGroups(GroupSelector selector)
		{
			AddRemoveGroupsAsync(selector).GetAwaiter().GetResult();
		}

		public async Task AddRemoveGroupsAsync(GroupSelector selector)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _adminAddRemoveGroupsStrategy.AddRemoveGroupsAsync(selector).ConfigureAwait(false);
			}
		}

		public void AddToGroups(params int[] groupIds)
		{
			AddToGroupsAsync(groupIds).GetAwaiter().GetResult();
		}

		public async Task AddToGroupsAsync(params int[] groupIds)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _adminAddToGroupsStrategy.AddToGroupsAsync(groupIds).ConfigureAwait(false);
			}
		}

		public void AddToGroups(params string[] groupNames)
		{
			AddToGroupsAsync(groupNames).GetAwaiter().GetResult();
		}

		public async Task AddToGroupsAsync(params string[] groupNames)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _adminAddToGroupsStrategy.AddToGroupsAsync(groupNames).ConfigureAwait(false);
			}
		}

		public GroupPermissions GetAdminGroupPermissions(int groupId)
		{
			return GetAdminGroupPermissionsAsync(groupId).GetAwaiter().GetResult();
		}

		public GroupPermissions GetAdminGroupPermissions(string groupName)
		{
			return GetAdminGroupPermissionsAsync(groupName).GetAwaiter().GetResult();
		}

		public async Task<GroupPermissions> GetAdminGroupPermissionsAsync(int groupId)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				return await _adminGetGroupPermissionsStrategy.GetAsync(groupId).ConfigureAwait(false);
			}
		}

		public async Task<GroupPermissions> GetAdminGroupPermissionsAsync(string groupName)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				return await _adminGetGroupPermissionsStrategy.GetAsync(groupName).ConfigureAwait(false);
			}
		}

		public void SetAdminGroupPermissions(GroupPermissions groupPermissions)
		{
			SetAdminGroupPermissionsAsync(groupPermissions).GetAwaiter().GetResult();
		}

		public async Task SetAdminGroupPermissionsAsync(GroupPermissions groupPermissions)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _adminSetGroupPermissionsStrategy.SetAsync(groupPermissions).ConfigureAwait(false);
			}
		}

		public void SetAdminGroupPermissions(int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			SetAdminGroupPermissionsAsync(groupId, groupPermissionsChangesetSetter).GetAwaiter().GetResult();
		}

		public async Task SetAdminGroupPermissionsAsync(int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _adminChangeGroupPermissionsStrategy.SetAsync(groupId, groupPermissionsChangesetSetter).ConfigureAwait(false);
			}
		}

		public void SetAdminGroupPermissions(string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			SetAdminGroupPermissionsAsync(groupName, groupPermissionsChangesetSetter).GetAwaiter().GetResult();
		}

		public async Task SetAdminGroupPermissionsAsync(string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _adminChangeGroupPermissionsStrategy.SetAsync(groupName, groupPermissionsChangesetSetter).ConfigureAwait(false);
			}
		}

		public void SetAdminGroupPermissions(int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			SetAdminGroupPermissionsAsync(groupId, groupPermissionsChangeset).GetAwaiter().GetResult();
		}

		public async Task SetAdminGroupPermissionsAsync(int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _adminChangeGroupPermissionsStrategy.SetAsync(groupId, groupPermissionsChangeset).ConfigureAwait(false);
			}
		}

		public void SetAdminGroupPermissions(string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			SetAdminGroupPermissionsAsync(groupName, groupPermissionsChangeset).GetAwaiter().GetResult();
		}

		public async Task SetAdminGroupPermissionsAsync(string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _adminChangeGroupPermissionsStrategy.SetAsync(groupName, groupPermissionsChangeset).ConfigureAwait(false);
			}
		}
#pragma warning restore S4136 // Method overloads should be grouped together
	}
}
