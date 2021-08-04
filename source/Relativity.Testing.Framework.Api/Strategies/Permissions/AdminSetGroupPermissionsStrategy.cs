using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AdminSetGroupPermissionsStrategy : IAdminSetGroupPermissionsStrategy
	{
		private readonly IRestService _restService;

		public AdminSetGroupPermissionsStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public Task SetAsync(GroupPermissions groupPermissions)
		{
			if (groupPermissions is null)
			{
				throw new ArgumentNullException(nameof(groupPermissions));
			}

			return ActionSetAsync(groupPermissions);
		}

		private async Task ActionSetAsync(GroupPermissions groupPermissions)
		{
			var dto = new
			{
				groupPermissions
			};

			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _restService.PostAsync(
					"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/SetAdminGroupPermissionsAsync", dto)
					.ConfigureAwait(false);
			}
		}
	}
}
