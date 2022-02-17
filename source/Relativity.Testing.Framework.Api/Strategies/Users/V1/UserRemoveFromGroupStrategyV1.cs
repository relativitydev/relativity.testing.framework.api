using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[ApplicationVersionRange("A0B32359-6540-425C-934C-12C0FD684809", ">=15.6.0")]
	internal class UserRemoveFromGroupStrategyV1 : UserRemoveFromGroupStrategy
	{
		private const string _endpoint = "Relativity-Identity/v1/groups/{0}/members";

		public UserRemoveFromGroupStrategyV1(
			IRestService restService,
			IEnsureExistsByIdStrategy<User> userEnsureExistsByIdStrategy,
			IEnsureExistsByIdStrategy<Group> groupEnsureExistsByIdStrategy)
			: base(restService, userEnsureExistsByIdStrategy, groupEnsureExistsByIdStrategy)
		{
		}

		protected override void DoRemoveFromGroup(int userId, int groupId)
		{
			var dto = new
			{
				users = new[] { new { ArtifactID = userId } }
			};

			RestService.Delete(string.Format(_endpoint, groupId), dto);
		}
	}
}
