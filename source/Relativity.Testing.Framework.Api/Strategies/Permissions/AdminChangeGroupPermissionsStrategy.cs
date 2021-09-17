using System;
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

		public void Set(int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			Set(groupId, groupPermissionsChangeset);
		}

		public void Set(int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			_adminAddToGroupsStrategy.AddToGroups(groupId);

			GroupPermissions groupPermissions = _adminGetGroupPermissionsStrategy.Get(groupId);

			groupPermissionsChangeset.Execute(groupPermissions);

			_adminSetGroupPermissionsStrategy.Set(groupPermissions);
		}

		public void Set(string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			Set(groupName, groupPermissionsChangeset);
		}

		public void Set(string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			_adminAddToGroupsStrategy.AddToGroups(groupName);

			int groupId = _adminGetGroupPermissionsStrategy.Get(groupName).GroupID;

			GroupPermissions groupPermissions = _adminGetGroupPermissionsStrategy.Get(groupId);

			groupPermissionsChangeset.Execute(groupPermissions);

			_adminSetGroupPermissionsStrategy.Set(groupPermissions);
		}
	}
}
