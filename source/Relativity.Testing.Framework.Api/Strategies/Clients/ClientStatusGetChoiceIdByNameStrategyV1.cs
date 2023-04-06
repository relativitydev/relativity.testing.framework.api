using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ClientStatusGetChoiceIdByNameStrategyV1 : CachedGetChoiceIdByNameStrategyBase,   IClientStatusGetChoiceIdByNameStrategy
	{
		private readonly IRestService _restService;

		public ClientStatusGetChoiceIdByNameStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		protected override ArtifactIdNamePair[] GetAll()
		{
			return _restService.Get<ArtifactIdNamePair[]>(
				"/Relativity.rest/api/relativity-identity/v1/workspaces/-1/clients/eligible-statuses");
		}
	}
}
