﻿using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ItemLevelSecuritySetStrategy : IItemLevelSecuritySetStrategy
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<ItemLevelSecurity> _getWorkspaceItemByIdStrategy;

		public ItemLevelSecuritySetStrategy(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<ItemLevelSecurity> getWorkspaceItemByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceItemByIdStrategy = getWorkspaceItemByIdStrategy;
		}

		public async Task SetAsync(int workspaceId, int itemId, bool isEnabled)
		{
			var itemLevelSecurity = _getWorkspaceItemByIdStrategy.Get(workspaceId, itemId);

			if (itemLevelSecurity.Enabled != isEnabled)
			{
				itemLevelSecurity.Enabled = isEnabled;

				var dto = new
				{
					workspaceArtifactID = workspaceId,
					itemLevelSecurity
				};

				await _restService.PostAsync(
					"Relativity.Services.Permission.IPermissionModule/Permission Manager/SetItemLevelSecurityAsync", dto)
					.ConfigureAwait(false);
			}
		}
	}
}
