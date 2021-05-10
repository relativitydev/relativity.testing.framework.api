using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientCreateStrategy : CreateStrategy<Client>
	{
		private readonly IRestService _restService;

		private readonly IClientStatusGetChoiceIdByNameStrategy _clientStatusGetChoiceIdByNameStrategy;

		public ClientCreateStrategy(IRestService restService, IClientStatusGetChoiceIdByNameStrategy clientStatusGetChoiceIdByNameStrategy)
		{
			_restService = restService;
			_clientStatusGetChoiceIdByNameStrategy = clientStatusGetChoiceIdByNameStrategy;
		}

		protected override Client DoCreate(Client entity)
		{
			if (entity.Status.ArtifactID == 0)
			{
				entity.Status.ArtifactID = GetStatusId(entity.Status.Name);
			}

			object dto = new
			{
				clientDTO = new
				{
					entity.Name,
					entity.Number,
					entity.Status,
					entity.Keywords,
					entity.Notes
				}
			};

			entity.ArtifactID = _restService.Post<int>(
				"Relativity.Services.Client.IClientModule/Client%20Manager/CreateSingleAsync",
				dto);

			return entity;
		}

		private int GetStatusId(string status)
		{
			return _clientStatusGetChoiceIdByNameStrategy.GetId(status);
		}
	}
}
