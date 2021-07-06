using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies.InstanceSettings;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class InstanceSettingUpdateStrategyV1 : InstanceSettingUpdateAbstractStrategy
	{
		private readonly IRestService _restService;

		public InstanceSettingUpdateStrategyV1(IRestService restService, IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy)
			: base(instanceSettingGetByNameAndSectionStrategy)
		{
			_restService = restService;
		}

		protected override void DoUpdate(InstanceSetting entity)
		{
			var dto = new
			{
				instanceSetting = InstanceSettingDTOMapper.DoMappingToDTO(entity)
			};

			_restService.Put(
				"/Relativity.REST/api/relativity-environment/v1/workspaces/-1/instance-settings",
				dto);
		}
	}
}
