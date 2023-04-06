using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class InstanceSettingDeleteByIdStrategyPrePrairieSmoke : DeleteByIdStrategy<InstanceSetting>
	{
		private readonly IRestService _restService;

		public InstanceSettingDeleteByIdStrategyPrePrairieSmoke(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int id)
		{
			_restService.Delete($"Relativity.InstanceSettings/workspace/-1/instancesettings/{id}");
		}
	}
}
