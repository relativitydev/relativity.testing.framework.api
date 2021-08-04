using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class WorkspaceSetGroupPermissionsStrategy : IWorkspaceSetGroupPermissionsStrategy
	{
		private readonly IRestService _restService;

		public WorkspaceSetGroupPermissionsStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public Task SetAsync(int workspaceId, GroupPermissions groupPermissions)
		{
			if (groupPermissions is null)
			{
				throw new ArgumentNullException(nameof(groupPermissions));
			}

			return ActionSetAsync(workspaceId, groupPermissions);
		}

		private async Task ActionSetAsync(int workspaceId, GroupPermissions groupPermissions)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				groupPermissions
			};

			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _restService.PostAsync(
				"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/SetWorkspaceGroupPermissionsAsync", dto)
					.ConfigureAwait(false);
			}
		}
	}
}
