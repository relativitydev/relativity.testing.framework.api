using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies.InstanceSettings;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class InstanceSettingCreateStrategyV1 : CreateStrategy<InstanceSetting>
	{
		private readonly IRestService _restService;
		private readonly IGetByIdStrategy<InstanceSetting> _getByIdStrategy;

		public InstanceSettingCreateStrategyV1(
			IRestService restService,
			IGetByIdStrategy<InstanceSetting> getByIdStrategy)
		{
			_restService = restService;
			_getByIdStrategy = getByIdStrategy;
		}

		protected override InstanceSetting DoCreate(InstanceSetting entity)
		{
			var dto = new
			{
				instanceSetting = InstanceSettingDTOMapper.DoMappingToDTO(entity)
			};

			var artifactId = _restService.Post<int>(
				$"/Relativity.REST/api/relativity-environment/v1/workspaces/-1/instance-settings",
				dto);

			return _getByIdStrategy.Get(artifactId);
		}
	}
}
