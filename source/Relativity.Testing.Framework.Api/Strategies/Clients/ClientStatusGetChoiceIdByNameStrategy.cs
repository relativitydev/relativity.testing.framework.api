using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientStatusGetChoiceIdByNameStrategy : CachedGetChoiceIdByNameStrategyBase, IClientStatusGetChoiceIdByNameStrategy
	{
		private readonly IRestService _restService;

		public ClientStatusGetChoiceIdByNameStrategy(IRestService restService)
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
