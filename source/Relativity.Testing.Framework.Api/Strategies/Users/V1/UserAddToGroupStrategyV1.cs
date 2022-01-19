﻿using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[ApplicationVersionRange("A0B32359-6540-425C-934C-12C0FD684809", ">=15.4.2")]
	internal class UserAddToGroupStrategyV1 : UserAddToGroupStrategy
	{
		private const string _endpoint = "Relativity-Identity/v1/groups/{0}/members";

		public UserAddToGroupStrategyV1(
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
				users = new[] { new { ArtifactID = userId } }
			};

			RestService.Post(string.Format(_endpoint, groupId), dto);

			WaitUserAddedToGroupStrategy.Wait(-1, groupId, userId);
		}
	}
}
