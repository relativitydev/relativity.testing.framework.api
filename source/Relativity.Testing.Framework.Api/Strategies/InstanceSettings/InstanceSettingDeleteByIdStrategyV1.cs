using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class InstanceSettingDeleteByIdStrategyV1 : DeleteByIdStrategy<InstanceSetting>
	{
		private readonly IRestService _restService;

		public InstanceSettingDeleteByIdStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int id)
		{
			_restService.Delete($"/Relativity.REST/api/relativity-environment/V1/workspaces/-1/instance-settings/{id}");
		}
	}
}
