using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserRemoveFromGroupStrategy : IUserRemoveFromGroupStrategy
	{
		private readonly IRestService _restService;

		private readonly IEnsureExistsByIdStrategy<User> _userEnsureExistsByIdStrategy;

		private readonly IEnsureExistsByIdStrategy<Group> _groupEnsureExistsByIdStrategy;

		public UserRemoveFromGroupStrategy(
			IRestService restService,
			IEnsureExistsByIdStrategy<User> userEnsureExistsByIdStrategy,
			IEnsureExistsByIdStrategy<Group> groupEnsureExistsByIdStrategy)
		{
			_restService = restService;
			_userEnsureExistsByIdStrategy = userEnsureExistsByIdStrategy;
			_groupEnsureExistsByIdStrategy = groupEnsureExistsByIdStrategy;
		}

		public void RemoveFromGroup(int userId, int groupId)
		{
			_userEnsureExistsByIdStrategy.EnsureExists(userId);
			_groupEnsureExistsByIdStrategy.EnsureExists(groupId);

			var dto = new
			{
				userIds = new[] { userId },
				groupId
			};

			_restService.Post("Relativity.Services.GroupUserManager.IGroupUserModule/Group User Manager/RemoveUsersFromGroupAsync", dto);
		}
	}
}
