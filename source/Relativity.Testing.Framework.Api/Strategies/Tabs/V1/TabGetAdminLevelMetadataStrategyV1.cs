using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class TabGetAdminLevelMetadataStrategyV1 : ITabGetAdminLevelMetadataStrategy
	{
		private readonly IRestService _restService;

		public TabGetAdminLevelMetadataStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public Meta Get()
		{
			return _restService.Get<Meta>("relativity-data-visualization/v1/workspaces/-1/tabs/meta");
		}
	}
}
