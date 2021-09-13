using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class TabGetOrderStrategyPreOsier : ITabGetOrderStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public TabGetOrderStrategyPreOsier(IRestService restService, IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public List<Tab> Get(int workspaceId)
		{
			_workspaceIdValidator.Validate(workspaceId);

			List<TabOrderResponsePreOsier> response = _restService.Get<List<TabOrderResponsePreOsier>>(
				$"Relativity.Tabs/workspace/{workspaceId}/tabs/vieworderlist");

			var mappedResults = response.Select(tabResponse => tabResponse.ToTab()).ToList();
			return mappedResults;
		}
	}
}
