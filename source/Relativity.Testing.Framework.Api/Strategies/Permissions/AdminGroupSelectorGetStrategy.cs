using System.Threading.Tasks;
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

		public async Task<GroupSelector> GetAsync()
		{
			return await _restService.PostAsync<GroupSelector>(
				"Relativity.Services.Permission.IPermissionModule/Permission Manager/GetAdminGroupSelectorAsync")
			.ConfigureAwait(false);
		}
	}
}
