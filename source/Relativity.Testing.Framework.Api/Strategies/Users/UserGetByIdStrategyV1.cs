using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Extensions;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class UserGetByIdStrategyV1 : IGetByIdStrategy<User>
	{
		private readonly IRestService _restService;
		private readonly IGetByIdStrategy<Client> _clientGetByIdStrategy;
		private readonly IUserGetGroupsStrategy _userGetGroupsStrategy;

		public UserGetByIdStrategyV1(
			IRestService restService,
			IGetByIdStrategy<Client> clientGetByIdStrategy,
			IUserGetGroupsStrategy userGetGroupsStrategy)
		{
			_restService = restService;
			_clientGetByIdStrategy = clientGetByIdStrategy;
			_userGetGroupsStrategy = userGetGroupsStrategy;
		}

		public User Get(int id)
		{
			var userDto = _restService.Get<UserDtoV1>($"Relativity-Identity/v1/users/{id}");
			var client = _clientGetByIdStrategy.Get(userDto.Client.Value.ArtifactID);
			var groups = _userGetGroupsStrategy.GetGroups(id);

			return userDto.MapToUser(client, groups);
		}
	}
}
