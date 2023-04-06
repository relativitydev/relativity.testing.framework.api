using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class TabGetOrderStrategyV1 : ITabGetOrderStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public TabGetOrderStrategyV1(IRestService restService, IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public List<Tab> Get(int workspaceId)
		{
			_workspaceIdValidator.Validate(workspaceId);

			List<TabResponseV1> response = _restService.Get<List<TabResponseV1>>(
				$"relativity-data-visualization/v1/workspaces/{workspaceId}/tabs/view-order-list");

			var mappedResults = response.Select(tabResponse => tabResponse.ToTab()).ToList();
			return mappedResults;
		}
	}
}
