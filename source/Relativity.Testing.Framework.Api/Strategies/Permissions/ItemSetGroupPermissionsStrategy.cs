using System;
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

		public void Set(int workspaceId, int itemId, GroupPermissions groupPermissions)
		{
			if (groupPermissions is null)
			{
				throw new ArgumentNullException(nameof(groupPermissions));
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				ArtifactID = itemId,
				groupPermissions
			};
			lock (GroupSelectorLocker.Locker)
			{
				_restService.Post(
					"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/SetItemGroupPermissionsAsync",
					dto);
			}
		}
	}
}
