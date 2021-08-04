using System;
using System.Linq;
using System.Threading.Tasks;
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

		public Task SetAsync(int workspaceId, int itemId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			return ActionSetAsync(workspaceId, itemId, groupName, groupPermissionsChangesetSetter);
		}

		public Task SetAsync(int workspaceId, int itemId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			if (groupPermissionsChangesetSetter is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangesetSetter));
			}

			return ActionSetAsync(workspaceId, itemId, groupId, groupPermissionsChangesetSetter);
		}

		public Task SetAsync(int workspaceId, int itemId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			return ActionSetAsync(workspaceId, itemId, groupName, groupPermissionsChangeset);
		}

		public Task SetAsync(int workspaceId, int itemId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			if (groupPermissionsChangeset is null)
			{
				throw new ArgumentNullException(nameof(groupPermissionsChangeset));
			}

			return ActionSetAsync(workspaceId, itemId, groupId, groupPermissionsChangeset);
		}

		private async Task ActionSetAsync(int workspaceId, int itemId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			await SetAsync(workspaceId, itemId, groupName, groupPermissionsChangeset).ConfigureAwait(false);
		}

		private async Task ActionSetAsync(int workspaceId, int itemId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter)
		{
			GroupPermissionsChangeset groupPermissionsChangeset = new GroupPermissionsChangeset();
			groupPermissionsChangesetSetter.Invoke(groupPermissionsChangeset);

			await SetAsync(workspaceId, itemId, groupId, groupPermissionsChangeset).ConfigureAwait(false);
		}

		private async Task ActionSetAsync(int workspaceId, int itemId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			await _itemLevelSecuritySetStrategy.SetAsync(workspaceId, itemId, true).ConfigureAwait(false);
			await _itemAddToGroupsStrategy.AddItemToGroupsAsync(workspaceId, itemId, groupName).ConfigureAwait(false);

			int groupId = _objectService.Query<Group>().
				FetchOnlyArtifactID().
				Where(x => x.Name, groupName).
				First().ArtifactID;

			GroupPermissions groupPermissions = await _itemGetGroupPermissionsStrategy.GetAsync(workspaceId, itemId, groupId).ConfigureAwait(false);

			groupPermissionsChangeset.Execute(groupPermissions);

			await _itemSetGroupPermissionsStrategy.SetAsync(workspaceId, itemId, groupPermissions).ConfigureAwait(false);
		}

		private async Task ActionSetAsync(int workspaceId, int itemId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset)
		{
			await _itemLevelSecuritySetStrategy.SetAsync(workspaceId, itemId, true).ConfigureAwait(false);
			await _itemAddToGroupsStrategy.AddItemToGroupsAsync(workspaceId, itemId, groupId).ConfigureAwait(false);

			GroupPermissions groupPermissions = await _itemGetGroupPermissionsStrategy.GetAsync(workspaceId, itemId, groupId).ConfigureAwait(false);

			groupPermissionsChangeset.Execute(groupPermissions);

			await _itemSetGroupPermissionsStrategy.SetAsync(workspaceId, itemId, groupPermissions).ConfigureAwait(false);
		}
	}
}
