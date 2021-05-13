using System;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ItemChangeGroupPermissionsStrategy : IItemChangeGroupPermissionsStrategy
	{
		private readonly IObjectService _objectService;

		private readonly IItemAddToGroupsStrategy _itemAddToGroupsStrategy;

		private readonly IItemGetGroupPermissionsStrategy _itemGetGroupPermissionsStrategy;

		private readonly IItemSetGroupPermissionsStrategy _itemSetGroupPermissionsStrategy;

		private readonly IItemLevelSecuritySetStrategy _itemLevelSecuritySetStrategy;

		public ItemChangeGroupPermissionsStrategy(
			IObjectService objectService,
			IItemAddToGroupsStrategy itemAddToGroupsStrategy,
			IItemGetGroupPermissionsStrategy itemGetGroupPermissionsStrategy,
			IItemSetGroupPermissionsStrategy itemSetGroupPermissionsStrategy,
			IItemLevelSecuritySetStrategy itemLevelSecuritySetStrategy)
		{
			_objectService = objectService;
			_itemAddToGroupsStrategy = itemAddToGroupsStrategy;
			_itemGetGroupPermissionsStrategy = itemGetGroupPermissionsStrategy;
			_itemSetGroupPermissionsStrategy = itemSetGroupPermissionsStrategy;
			_itemLevelSecuritySetStrategy = itemLevelSecuritySetStrategy;
		}

		public void Set(int workspaceId, int itemId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			Set(workspaceId, itemId, groupName, groupPermissionsChangeset);
		}

		public void Set(int workspaceId, int itemId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			Set(workspaceId, itemId, groupId, groupPermissionsChangeset);
		}

		public void Set(int workspaceId, int itemId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			_itemLevelSecuritySetStrategy.Set(workspaceId, itemId, true);
			_itemAddToGroupsStrategy.AddItemToGroups(workspaceId, itemId, groupName);

			int groupId = _objectService.Query<Group>().
				FetchOnlyArtifactID().
				Where(x => x.Name, groupName).
				First().ArtifactID;

			GroupPermissions groupPermissions = _itemGetGroupPermissionsStrategy.Get(workspaceId, itemId, groupId);

			groupPermissionsChangeset.Execute(groupPermissions);

			_itemSetGroupPermissionsStrategy.Set(workspaceId, itemId, groupPermissions);
		}

		public void Set(int workspaceId, int itemId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			_itemLevelSecuritySetStrategy.Set(workspaceId, itemId, true);
			_itemAddToGroupsStrategy.AddItemToGroups(workspaceId, itemId, groupId);

			GroupPermissions groupPermissions = _itemGetGroupPermissionsStrategy.Get(workspaceId, itemId, groupId);

			groupPermissionsChangeset.Execute(groupPermissions);

			_itemSetGroupPermissionsStrategy.Set(workspaceId, itemId, groupPermissions);
		}
	}
}
