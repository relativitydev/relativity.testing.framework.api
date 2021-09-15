using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies.InstanceSettings;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class InstanceSettingUpdateStrategyPrePrairieSmoke : InstanceSettingUpdateAbstractStrategy
	{
		private readonly IRestService _restService;

		public InstanceSettingUpdateStrategyPrePrairieSmoke(
			IRestService restService,
			IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy,
			IGetByIdStrategy<InstanceSetting> getByIdStrategy)
		 : base(instanceSettingGetByNameAndSectionStrategy, getByIdStrategy)
		{
			_restService = restService;
		}

		protected override void DoUpdate(InstanceSetting entity)
		{
			var dto = new
			{
				instanceSetting = entity
			};

			_restService.Put(
				"Relativity.InstanceSettings/workspace/-1/instancesettings",
				dto);
		}
	}
}
