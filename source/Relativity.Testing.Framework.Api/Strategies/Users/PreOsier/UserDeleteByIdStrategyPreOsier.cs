using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class UserDeleteByIdStrategyPreOsier : DeleteByIdStrategy<User>
	{
		private readonly IRestService _restService;

		private readonly IEnsureExistsByIdStrategy<User> _ensureExistsByIdStrategy;

		public UserDeleteByIdStrategyPreOsier(IRestService restService, IEnsureExistsByIdStrategy<User> ensureExistsByIdStrategy)
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
