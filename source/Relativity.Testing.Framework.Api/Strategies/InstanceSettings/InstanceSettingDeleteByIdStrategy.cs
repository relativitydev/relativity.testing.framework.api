using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class InstanceSettingDeleteByIdStrategy : DeleteByIdStrategy<InstanceSetting>
	{
		private readonly IRestService _restService;

		public InstanceSettingDeleteByIdStrategy(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int id)
		{
			_restService.Delete($"Relativity.InstanceSettings/workspace/-1/instancesettings/{id}");
		}
	}
}
