using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupDeleteByIdStrategy : DeleteByIdStrategy<Group>
	{
		private readonly IRestService _restService;
		private readonly IEnsureExistsByIdStrategy<Group> _ensureExistsByIdStrategy;

		public GroupDeleteByIdStrategy(IRestService restService, IEnsureExistsByIdStrategy<Group> ensureExistsByIdStrategy)
		{
			_restService = restService;
			_ensureExistsByIdStrategy = ensureExistsByIdStrategy;
		}

		protected override void DoDelete(int id)
		{
			_ensureExistsByIdStrategy.EnsureExists(id);

			using (GroupSelectorLocker.Locker.Lock())
			{
				_restService.Delete($"relativity.groups/workspace/-1/groups/{id}");
			}
		}
	}
}
