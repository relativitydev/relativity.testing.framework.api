using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class MatterGetEligibleClientsStrategyV1 : IMatterGetEligibleClientsStrategy
	{
		private readonly IRestService _restService;

		public MatterGetEligibleClientsStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public ArtifactIdNamePair[] GetAll()
		{
			return _restService.Get<ArtifactIdNamePair[]>("relativity-environment/v1/workspaces/-1/eligible-clients");
		}
	}
}
