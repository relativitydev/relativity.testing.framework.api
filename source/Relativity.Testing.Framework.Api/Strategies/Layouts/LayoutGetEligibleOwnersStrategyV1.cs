using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0")]
	internal class LayoutGetEligibleOwnersStrategyV1 : ILayoutGetEligibleOwnersStrategy
	{
		private readonly IRestService _restService;

		public LayoutGetEligibleOwnersStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public List<NamedArtifact> GetEligibleOwners(int workspaceId)
		{
			return _restService.Get<List<NamedArtifact>>($"Relativity.Layouts/workspace/{workspaceId}/layouts/eligible-owners");
		}
	}
}
