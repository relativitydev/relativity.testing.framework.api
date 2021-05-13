using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserDeleteByIdStrategy : DeleteByIdStrategy<User>
	{
		private readonly IRestService _restService;

		private readonly IEnsureExistsByIdStrategy<User> _ensureExistsByIdStrategy;

		public UserDeleteByIdStrategy(IRestService restService, IEnsureExistsByIdStrategy<User> ensureExistsByIdStrategy)
		{
			_restService = restService;
			_ensureExistsByIdStrategy = ensureExistsByIdStrategy;
		}

		protected override void DoDelete(int id)
		{
			_ensureExistsByIdStrategy.EnsureExists(id);

			_restService.Delete($"Relativity.Users/workspace/-1/Users/{id}");
		}
	}
}
