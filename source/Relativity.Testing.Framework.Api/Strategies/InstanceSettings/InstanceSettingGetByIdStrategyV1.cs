using System.Net.Http;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies.InstanceSettings;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class InstanceSettingGetByIdStrategyV1 : IGetByIdStrategy<InstanceSetting>
	{
		private readonly IRestService _restService;

		public InstanceSettingGetByIdStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public InstanceSetting Get(int id)
		{
			try
			{
				var instanceSettingDTO = _restService.Get<InstanceSettingDTO>($"/Relativity.REST/api/relativity-environment/v1/workspaces/-1/instance-settings/{id}");
				return InstanceSettingDTOMapper.DoMapping(instanceSettingDTO);
			}
			catch (HttpRequestException ex) when (ex.Message.Contains("Relativity.Services.Exceptions.NotFoundException"))
			{
				return null;
			}
		}
	}
}
