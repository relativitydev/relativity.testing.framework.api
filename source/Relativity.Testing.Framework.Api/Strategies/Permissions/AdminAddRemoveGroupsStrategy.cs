using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AdminAddRemoveGroupsStrategy : IAdminAddRemoveGroupsStrategy
	{
		private readonly IRestService _restService;

		public AdminAddRemoveGroupsStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void AddRemoveGroups(GroupSelector selector)
		{
			var dto = new
			{
				groupSelector = selector
			};

			lock (GroupSelectorLocker.Locker)
			{
				_restService.Post(
					"Relativity.Services.Permission.IPermissionModule/Permission%20Manager/AddRemoveAdminGroupsAsync",
					dto);
			}
		}
	}
}
