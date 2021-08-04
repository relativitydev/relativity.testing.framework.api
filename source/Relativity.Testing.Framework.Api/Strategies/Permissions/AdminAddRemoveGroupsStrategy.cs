using System.Threading.Tasks;
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

		public async Task AddRemoveGroupsAsync(GroupSelector selector)
		{
			var dto = new
			{
				groupSelector = selector
			};

			using (await GroupSelectorLocker.Locker.LockAsync().ConfigureAwait(false))
			{
				await _restService.PostAsync("Relativity.Services.Permission.IPermissionModule/Permission%20Manager/AddRemoveAdminGroupsAsync", dto)
					.ConfigureAwait(false);
			}
		}
	}
}
