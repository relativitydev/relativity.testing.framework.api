using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ViewGetEligibleObjectTypesStrategyV1 : IViewGetEligibleObjectTypesStrategy
	{
		private readonly IRestService _restService;

		public ViewGetEligibleObjectTypesStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public List<NamedArtifact> GetEligibleObjectTypes(int workspaceId)
		{
			return _restService.Get<List<NamedArtifact>>($"relativity-data-visualization/V1/workspaces/{workspaceId}/eligible-object-types");
		}
	}
}
