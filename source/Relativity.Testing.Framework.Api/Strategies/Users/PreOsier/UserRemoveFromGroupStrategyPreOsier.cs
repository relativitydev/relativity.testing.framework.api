using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies.Users.PreOsier
{
	[ApplicationVersionRange("A0B32359-6540-425C-934C-12C0FD684809", "<15.6.0")]
	internal class UserRemoveFromGroupStrategyPreOsier : UserRemoveFromGroupStrategy
	{
		private const string _endpoint = "Relativity.Services.GroupUserManager.IGroupUserModule/Group User Manager/RemoveUsersFromGroupAsync";

		public UserRemoveFromGroupStrategyPreOsier(
			IRestService restService,
			IEnsureExistsByIdStrategy<User> userEnsureExistsByIdStrategy,
			IEnsureExistsByIdStrategy<Group> groupEnsureExistsByIdStrategy,
			IWaitUserRemoveFromGroupStrategy waitUserRemoveFromGroup)
			: base(restService, userEnsureExistsByIdStrategy, groupEnsureExistsByIdStrategy, waitUserRemoveFromGroup)
		{
		}

		protected override void DoRemoveFromGroup(int userId, int groupId)
		{
			var dto = new
			{
				userIds = new[] { userId },
				groupId
			};

			RestService.Post(_endpoint, dto);
		}
	}
}
