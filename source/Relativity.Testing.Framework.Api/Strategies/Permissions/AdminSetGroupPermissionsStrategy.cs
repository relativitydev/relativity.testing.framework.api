using System;
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

		public void Set(GroupPermissions groupPermissions)
		{
			if (groupPermissions is null)
				throw new ArgumentNullException(nameof(groupPermissions));

			var dto = new
			{
				groupPermissions
			};
			lock (GroupSelectorLocker.Locker)
			{
				_restService.Post(
					"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/SetAdminGroupPermissionsAsync",
					dto);
			}
		}
	}
}
