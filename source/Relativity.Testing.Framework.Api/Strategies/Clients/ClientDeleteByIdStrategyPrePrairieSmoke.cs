using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ClientDeleteByIdStrategyPrePrairieSmoke : DeleteByIdStrategy<Client>
	{
		private readonly IRestService _restService;

		public ClientDeleteByIdStrategyPrePrairieSmoke(IRestService restService)
		{
			_restService = restService;
		}

		protected override void DoDelete(int id)
		{
			object dto = new
			{
				clientArtifactID = id
			};

			_restService.Post(
				"Relativity.Services.Client.IClientModule/Client%20Manager/DeleteSingleAsync",
				dto);
		}
	}
}
