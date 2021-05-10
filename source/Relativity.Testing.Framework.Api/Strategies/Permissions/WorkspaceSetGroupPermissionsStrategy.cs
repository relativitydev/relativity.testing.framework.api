using System;
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

		public void Set(int workspaceId, GroupPermissions groupPermissions)
		{
			if (groupPermissions is null)
				throw new ArgumentNullException(nameof(groupPermissions));

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				groupPermissions
			};

			lock (GroupSelectorLocker.Locker)
			{
				_restService.Post(
				"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/SetWorkspaceGroupPermissionsAsync",
				dto);
			}
		}
	}
}
