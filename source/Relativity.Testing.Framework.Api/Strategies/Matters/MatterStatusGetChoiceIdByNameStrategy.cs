using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterStatusGetChoiceIdByNameStrategy : CachedGetChoiceIdByNameStrategyBase, IMatterStatusGetChoiceIdByNameStrategy
	{
		private readonly IRestService _restService;

		public MatterStatusGetChoiceIdByNameStrategy(IRestService restService)
		{
			_restService = restService;
		}

		protected override ArtifactIdNamePair[] GetAll()
		{
			return _restService.Post<ArtifactIdNamePair[]>(
				"Relativity.Services.Matter.IMatterModule/Matter%20Manager/GetStatusChoicesForMatterAsync");
		}
	}
}
