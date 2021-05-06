using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ItemAddRemoveGroupsStrategy : IItemAddRemoveGroupsStrategy
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<ItemLevelSecurity> _getByWorkspaceItemByIdStrategy;
		private readonly IItemLevelSecuritySetStrategy _itemLevelSecuritySetStrategy;

		public ItemAddRemoveGroupsStrategy(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<ItemLevelSecurity> getByWorkspaceItemByIdStrategy,
			IItemLevelSecuritySetStrategy itemLevelSecuritySetStrategy)
		{
			_restService = restService;
			_getByWorkspaceItemByIdStrategy = getByWorkspaceItemByIdStrategy;
			_itemLevelSecuritySetStrategy = itemLevelSecuritySetStrategy;
		}

		public void AddRemoveItemGroups(int workspaceId, int itemId, GroupSelector selector, bool enableLevelSecurity = true)
		{
			if (enableLevelSecurity)
			{
				_itemLevelSecuritySetStrategy.Set(workspaceId, itemId, true);
				selector.LastModified = _getByWorkspaceItemByIdStrategy.Get(workspaceId, itemId).LastModified;
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				artifactID = itemId,
				groupSelector = selector
			};
			lock (GroupSelectorLocker.Locker)
			{
				_restService.Post(
					"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/AddRemoveItemGroupsAsync",
					dto);
			}
		}
	}
}
