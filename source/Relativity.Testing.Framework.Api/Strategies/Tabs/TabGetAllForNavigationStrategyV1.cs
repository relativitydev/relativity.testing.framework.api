using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class TabGetAllForNavigationStrategyV1 : ITabGetAllForNavigationStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public TabGetAllForNavigationStrategyV1(IRestService restService, IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public List<Tab> Get(int workspaceID)
		{
			_workspaceIdValidator.Validate(workspaceID);

			var response = _restService.Get<List<TabNavigationResponseV1>>($"relativity-data-visualization/V1/workspaces/{workspaceID}/tabs/navigation");

			return response.Select(x => x.ToTab()).ToList();
		}
	}
}
