using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ClientDeleteByIdStrategyV1 : DeleteByIdStrategy<Client>
	{
		private readonly IRestService _restService;

		public ClientDeleteByIdStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int id)
		{
			_restService.Delete(
				$"/Relativity.rest/api/relativity-identity/v1/workspaces/-1/clients/{id}");
		}
	}
}
