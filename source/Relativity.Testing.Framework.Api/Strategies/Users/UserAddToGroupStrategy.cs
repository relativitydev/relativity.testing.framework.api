using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal abstract class UserAddToGroupStrategy : IUserAddToGroupStrategy
	{
		protected UserAddToGroupStrategy(
			IRestService restService,
			IEnsureExistsByIdStrategy<User> userEnsureExistsByIdStrategy,
			IEnsureExistsByIdStrategy<Group> groupEnsureExistsByIdStrategy,
			IWaitUserAddedToGroupStrategy waitUserAddedToGroupStrategy)
		{
			RestService = restService;
			UserEnsureExistsByIdStrategy = userEnsureExistsByIdStrategy;
			GroupEnsureExistsByIdStrategy = groupEnsureExistsByIdStrategy;
			WaitUserAddedToGroupStrategy = waitUserAddedToGroupStrategy;
		}

		protected IRestService RestService { get; }

		protected IEnsureExistsByIdStrategy<User> UserEnsureExistsByIdStrategy { get; }

		protected IEnsureExistsByIdStrategy<Group> GroupEnsureExistsByIdStrategy { get; }

		protected IWaitUserAddedToGroupStrategy WaitUserAddedToGroupStrategy { get; }

		public void AddToGroup(int userId, int groupId)
		{
			UserEnsureExistsByIdStrategy.EnsureExists(userId);
			GroupEnsureExistsByIdStrategy.EnsureExists(groupId);
			DoAddToGroup(userId, groupId);
		}

		protected abstract void DoAddToGroup(int userId, int groupId);
	}
}
