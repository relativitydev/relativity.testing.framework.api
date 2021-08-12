using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies.Tabs.DTO;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies.Tabs
{
	[VersionRange(">=12.1")]
	internal class TabGetEligibleParentsStrategyV1 : ITabGetEligibleParentsStrategy
	{
		private readonly IRestService _restService;

		public TabGetEligibleParentsStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public List<TabEligibleParentV1> Get(int id)
		{
			return _restService.Get<List<TabEligibleParentV1>>($"relativity-data-visualization/V1/workspaces/{id}/tabs/eligible-parents");
		}
	}
}
