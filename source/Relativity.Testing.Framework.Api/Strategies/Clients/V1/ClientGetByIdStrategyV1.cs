using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ClientGetByIdStrategyV1 : IGetByIdStrategy<Client>
	{
		private readonly IRestService _restService;

		public ClientGetByIdStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public Client Get(int id)
		{
			var client = _restService.Get<Client>($"/Relativity.rest/api/relativity-identity/v1/workspaces/-1/clients/{id}");

			return client;
		}
	}
}
