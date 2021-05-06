using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AdminGroupSelectorGetStrategy : IGetStrategy<GroupSelector>
	{
		private readonly IRestService _restService;

		public AdminGroupSelectorGetStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public GroupSelector Get()
		{
			lock (GroupSelectorLocker.Locker)
			{
				return _restService.Post<GroupSelector>(
					"Relativity.Services.Permission.IPermissionModule/Permission Manager/GetAdminGroupSelectorAsync");
			}
		}
	}
}
