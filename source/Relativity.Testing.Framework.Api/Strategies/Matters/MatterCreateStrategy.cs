using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterCreateStrategy : CreateStrategy<Matter>
	{
		private readonly IRestService _restService;

		private readonly IMatterStatusGetChoiceIdByNameStrategy _matterStatusGetChoiceIdByNameStrategy;

		private readonly IRequireStrategy<Client> _clientRequireStrategy;

		public MatterCreateStrategy(
			IRestService restService,
			IMatterStatusGetChoiceIdByNameStrategy matterStatusGetChoiceIdByNameStrategy,
			IRequireStrategy<Client> clientRequireStrategy)
		{
			_restService = restService;
			_matterStatusGetChoiceIdByNameStrategy = matterStatusGetChoiceIdByNameStrategy;
			_clientRequireStrategy = clientRequireStrategy;
		}

		protected override Matter DoCreate(Matter entity)
		{
			entity.Client = _clientRequireStrategy.Require(entity.Client);

			object dto = new
			{
				matterDTO = new
				{
					entity.Name,
					entity.Number,
					Status = new
					{
						ArtifactID = GetStatusId(entity.Status)
					},
					Client = new
					{
						entity.Client.ArtifactID
					},
					entity.Keywords,
					entity.Notes
				}
			};

			entity.ArtifactID = _restService.Post<int>(
				"Relativity.Services.Matter.IMatterModule/Matter%20Manager/CreateSingleAsync",
				dto);

			return entity;
		}

		private int GetStatusId(string status)
		{
			return _matterStatusGetChoiceIdByNameStrategy.GetId(status);
		}
	}
}
