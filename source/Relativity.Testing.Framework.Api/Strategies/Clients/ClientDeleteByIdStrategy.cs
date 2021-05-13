using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientDeleteByIdStrategy : DeleteByIdStrategy<Client>
	{
		private readonly IRestService _restService;

		public ClientDeleteByIdStrategy(IRestService restService)
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
