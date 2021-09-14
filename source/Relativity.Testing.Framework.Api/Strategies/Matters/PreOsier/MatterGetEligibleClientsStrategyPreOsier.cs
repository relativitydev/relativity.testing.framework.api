using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class MatterGetEligibleClientsStrategyPreOsier : IMatterGetEligibleClientsStrategy
	{
		private readonly IRestService _restService;

		public MatterGetEligibleClientsStrategyPreOsier(IRestService restService)
		{
			_restService = restService;
		}

		public ArtifactIdNamePair[] GetAll()
		{
			return _restService.Post<ArtifactIdNamePair[]>("Relativity.Services.Matter.IMatterModule/Matter%20Manager/GetClientsForMatterAsync");
		}
	}
}
