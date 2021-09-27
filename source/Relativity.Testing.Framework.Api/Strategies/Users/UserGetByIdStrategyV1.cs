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

		public UserGetByIdStrategyV1(IRestService restService, IGetByIdStrategy<Client> clientGetByIdStrategy)
		{
			_restService = restService;
			_clientGetByIdStrategy = clientGetByIdStrategy;
		}

		public User Get(int id)
		{
			var userDto = _restService.Get<UserDtoV1>($"Relativity-Identity/v1/users/{id}");
			var client = _clientGetByIdStrategy.Get(userDto.Client.Value.ArtifactID);

			return userDto.MapToUser(client);
		}
	}
}
