using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ClientGetEligibleStatusesStrategyV1 : IClientGetEligibleStatusesStrategy
	{
		private readonly IRestService _restService;

		public ClientGetEligibleStatusesStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public IList<ArtifactIdNamePair> Get()
		{
			return _restService.Get<IList<ArtifactIdNamePair>>("relativity-identity/v1/workspaces/-1/clients/eligible-statuses");
		}
	}
}
