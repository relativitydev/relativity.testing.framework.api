using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserExistsByEmailStrategy : IUserExistsByEmailStrategy
	{
		private readonly IRestService _restService;

		public UserExistsByEmailStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public bool Exists(string email)
		{
			if (email == null)
			{
				throw new ArgumentNullException(nameof(email));
			}

			var result = _restService.Get<JArray>($"Relativity.Users/workspace/-1/users/retrieveall");

			return result.Any(x => (string)x["Email"] == email);
		}
	}
}
