using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserGetByEmailStrategy : IUserGetByEmailStrategy
	{
		private readonly IRestService _restService;

		private readonly IGetByIdStrategy<User> _getByIdStrategy;

		public UserGetByEmailStrategy(
			IRestService restService,
			IGetByIdStrategy<User> getByIdStrategy)
		{
			_restService = restService;
			_getByIdStrategy = getByIdStrategy;
		}

		public User Get(string email)
		{
			if (email == null)
			{
				throw new ArgumentNullException(nameof(email));
			}

			var request = new
			{
				start = 1,
				length = 100,
				query = new
				{
					Condition = $"('Email' == '{email}')",
				}
			};

			var response = _restService.Post<JObject>("Relativity.Users/workspace/-1/users/retrieveusersby", request);

			var userInfo = response["DataResults"].FirstOrDefault();

			return userInfo == null ? null : _getByIdStrategy.Get(userInfo.ToObject<User>().ArtifactID);
		}
	}
}
