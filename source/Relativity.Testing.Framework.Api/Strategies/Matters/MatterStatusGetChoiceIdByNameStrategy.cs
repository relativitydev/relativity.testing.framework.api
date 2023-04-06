using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterStatusGetChoiceIdByNameStrategy : CachedGetChoiceIdByNameStrategyBase, IMatterStatusGetChoiceIdByNameStrategy
	{
		private readonly IMatterGetEligibleStatusesStrategy _matterGetEligibleStatusesStrategy;

		public MatterStatusGetChoiceIdByNameStrategy(IMatterGetEligibleStatusesStrategy matterGetEligibleStatusesStrategy)
		{
			_matterGetEligibleStatusesStrategy = matterGetEligibleStatusesStrategy;
		}

		protected override ArtifactIdNamePair[] GetAll()
		{
			return _matterGetEligibleStatusesStrategy.GetAll();
		}
	}
}
