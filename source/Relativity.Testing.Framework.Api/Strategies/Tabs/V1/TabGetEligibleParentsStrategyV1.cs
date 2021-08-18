using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class TabGetEligibleParentsStrategyV1 : ITabGetEligibleParentsStrategy
	{
		private readonly IRestService _restService;

		public TabGetEligibleParentsStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public List<TabEligibleParent> Get(int workspaceId)
		{
			return _restService.Get<List<TabEligibleParent>>($"relativity-data-visualization/V1/workspaces/{workspaceId}/tabs/eligible-parents");
		}
	}
}
