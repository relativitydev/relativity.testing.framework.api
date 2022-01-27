using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies.Users.PreOsier
{
	[ApplicationVersionRange("A0B32359-6540-425C-934C-12C0FD684809", "<14.0.0")]
	internal class UserAddToGroupStrategyPreOsier : UserAddToGroupStrategy
	{
		private const string _endpoint = "Relativity.Services.GroupUserManager.IGroupUserModule/Group User Manager/AddUsersToGroupAsync";

		public UserAddToGroupStrategyPreOsier(
			IRestService restService,
			IEnsureExistsByIdStrategy<User> userEnsureExistsByIdStrategy,
			IEnsureExistsByIdStrategy<Group> groupEnsureExistsByIdStrategy,
			IWaitUserAddedToGroupStrategy waitUserAddedToGroupStrategy)
			: base(restService, userEnsureExistsByIdStrategy, groupEnsureExistsByIdStrategy, waitUserAddedToGroupStrategy)
		{
		}

		protected override void DoAddToGroup(int userId, int groupId)
		{
			var dto = new
			{
				userIds = new[] { userId },
				groupId
			};

			RestService.Post(_endpoint, dto);

			WaitUserAddedToGroupStrategy.Wait(-1, groupId, userId);
		}
	}
}
