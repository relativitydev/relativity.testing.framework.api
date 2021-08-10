using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies.Tabs
{
	[VersionRange(">=12.1")]
	internal class TabGetAvailableObjectTypesStrategyV1 : ITabGetAvailableObjectTypesByWorkspaceIDStrategy
	{
		private readonly IRestService _restService;

		public TabGetAvailableObjectTypesStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public List<ObjectType> Get(int id)
		{
			return _restService.Get<List<ObjectType>>($"relativity-data-visualization/V1/workspaces/{id}/tabs/eligible-object-types");
		}
	}
}
