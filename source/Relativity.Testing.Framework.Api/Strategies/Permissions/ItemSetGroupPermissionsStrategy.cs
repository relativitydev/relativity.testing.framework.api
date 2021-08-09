using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ItemSetGroupPermissionsStrategy : IItemSetGroupPermissionsStrategy
	{
		private readonly IRestService _restService;

		public ItemSetGroupPermissionsStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public Task SetAsync(int workspaceId, int itemId, GroupPermissions groupPermissions)
		{
			if (groupPermissions is null)
			{
				throw new ArgumentNullException(nameof(groupPermissions));
			}

			return ActionSetAsync(workspaceId, itemId, groupPermissions);
		}

		private async Task ActionSetAsync(int workspaceId, int itemId, GroupPermissions groupPermissions)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				ArtifactID = itemId,
				groupPermissions
			};

			await _restService.PostAsync(
				"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/SetItemGroupPermissionsAsync",
				dto).ConfigureAwait(false);
		}
	}
}
