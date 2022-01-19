using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserAddToGroupStrategy : IUserAddToGroupStrategy
	{
		private readonly IRestService _restService;

		private readonly IEnsureExistsByIdStrategy<User> _userEnsureExistsByIdStrategy;

		private readonly IEnsureExistsByIdStrategy<Group> _groupEnsureExistsByIdStrategy;

		private readonly IWaitUserAddedToGroupStrategy _waitUserAddedToGroupStrategy;

		public UserAddToGroupStrategy(
			IRestService restService,
			IEnsureExistsByIdStrategy<User> userEnsureExistsByIdStrategy,
			IEnsureExistsByIdStrategy<Group> groupEnsureExistsByIdStrategy,
			IWaitUserAddedToGroupStrategy waitUserAddedToGroupStrategy)
		{
			_restService = restService;
			_userEnsureExistsByIdStrategy = userEnsureExistsByIdStrategy;
			_groupEnsureExistsByIdStrategy = groupEnsureExistsByIdStrategy;
			_waitUserAddedToGroupStrategy = waitUserAddedToGroupStrategy;
		}

		public void AddToGroup(int userId, int groupId)
		{
			_userEnsureExistsByIdStrategy.EnsureExists(userId);
			_groupEnsureExistsByIdStrategy.EnsureExists(groupId);

			var dto = new
			{
				users = new[] { new { ArtifactID = userId } }
			};

			_restService.Post(string.Format("Relativity-Identity/v1/groups/{0}/members", groupId), dto);

			_waitUserAddedToGroupStrategy.Wait(-1, groupId, userId);
		}
	}
}
