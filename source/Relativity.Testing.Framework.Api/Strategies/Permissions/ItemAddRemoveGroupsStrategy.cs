using System.Threading.Tasks;
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

		public async Task AddRemoveItemGroupsAsync(int workspaceId, int itemId, GroupSelector selector, bool enableLevelSecurity = true)
		{
			if (enableLevelSecurity)
			{
				await _itemLevelSecuritySetStrategy.SetAsync(workspaceId, itemId, true).ConfigureAwait(false);
				selector.LastModified = _getByWorkspaceItemByIdStrategy.Get(workspaceId, itemId).LastModified;
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				artifactID = itemId,
				groupSelector = selector
			};

			await _restService.PostAsync(
				"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/AddRemoveItemGroupsAsync", dto)
				.ConfigureAwait(false);
		}
	}
}
