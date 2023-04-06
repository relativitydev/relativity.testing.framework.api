using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal abstract class UserRemoveFromGroupStrategy : IUserRemoveFromGroupStrategy
	{
		protected UserRemoveFromGroupStrategy(
			IRestService restService,
			IEnsureExistsByIdStrategy<User> userEnsureExistsByIdStrategy,
			IEnsureExistsByIdStrategy<Group> groupEnsureExistsByIdStrategy)
		{
			RestService = restService;
			UserEnsureExistsByIdStrategy = userEnsureExistsByIdStrategy;
			GroupEnsureExistsByIdStrategy = groupEnsureExistsByIdStrategy;
		}

		protected IRestService RestService { get; }

		protected IEnsureExistsByIdStrategy<User> UserEnsureExistsByIdStrategy { get; }

		protected IEnsureExistsByIdStrategy<Group> GroupEnsureExistsByIdStrategy { get; }

		public void RemoveFromGroup(int userId, int groupId)
		{
			UserEnsureExistsByIdStrategy.EnsureExists(userId);
			GroupEnsureExistsByIdStrategy.EnsureExists(groupId);
			DoRemoveFromGroup(userId, groupId);
		}

		protected abstract void DoRemoveFromGroup(int userId, int groupId);
	}
}
