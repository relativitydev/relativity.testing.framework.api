using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ScriptQueryActionJobResultsStrategy : IScriptQueryActionJobResultsStrategy
	{
		private readonly IRestService _restService;

		public ScriptQueryActionJobResultsStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public ActionResultsQueryResponse QueryActionJobResults(int workspaceId, Guid runJobId, int actionIndex, ActionQueryRequest actionQueryRequest = null, int start = 0, int length = 100)
		{
			if (actionQueryRequest == null)
			{
				actionQueryRequest = new ActionQueryRequest();
			}

			var dto = new
			{
				actionQueryRequest,
				start,
				length
			};

			return _restService.Post<ActionResultsQueryResponse>($"Relativity.Scripts/workspace/{workspaceId}/Scripts/runs/{runJobId}/actions/{actionIndex}/query-results", dto);
		}
	}
}
