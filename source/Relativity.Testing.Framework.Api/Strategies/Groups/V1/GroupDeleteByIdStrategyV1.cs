using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class GroupDeleteByIdStrategyV1 : DeleteByIdStrategy<Group>
	{
		private readonly IRestService _restService;
		private readonly IEnsureExistsByIdStrategy<Group> _ensureExistsByIdStrategy;

		public GroupDeleteByIdStrategyV1(IRestService restService, IEnsureExistsByIdStrategy<Group> ensureExistsByIdStrategy)
		{
			_restService = restService;
			_ensureExistsByIdStrategy = ensureExistsByIdStrategy;
		}

		protected override void DoDelete(int id)
		{
			_ensureExistsByIdStrategy.EnsureExists(id);

			lock (GroupSelectorLocker.Locker)
			{
				_restService.Delete($"Relativity-Identity/v1/groups/{id}");
			}
		}
	}
}
