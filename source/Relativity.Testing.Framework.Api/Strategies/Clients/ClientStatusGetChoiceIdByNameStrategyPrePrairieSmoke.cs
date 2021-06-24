using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ClientStatusGetChoiceIdByNameStrategyPrePrairieSmoke : CachedGetChoiceIdByNameStrategyBase, IClientStatusGetChoiceIdByNameStrategy
	{
		private readonly IRestService _restService;

		public ClientStatusGetChoiceIdByNameStrategyPrePrairieSmoke(IRestService restService)
		{
			_restService = restService;
		}

		protected override ArtifactIdNamePair[] GetAll()
		{
			return _restService.Post<ArtifactIdNamePair[]>(
				"Relativity.Services.Client.IClientModule/Client%20Manager/GetStatusChoicesForClientAsync");
		}
	}
}
