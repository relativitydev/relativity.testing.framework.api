using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class MatterCreateStrategyPreOsier : CreateStrategy<Matter>
	{
		private readonly IRestService _restService;
		private readonly IMatterStatusGetChoiceIdByNameStrategy _matterStatusGetChoiceIdByNameStrategy;
		private readonly IRequireStrategy<Client> _clientRequireStrategy;
		private readonly IMatterGetByIdStrategy _getByIdStrategy;

		public MatterCreateStrategyPreOsier(
			IRestService restService,
			IMatterStatusGetChoiceIdByNameStrategy matterStatusGetChoiceIdByNameStrategy,
			IRequireStrategy<Client> clientRequireStrategy,
			IMatterGetByIdStrategy getByIdStrategy)
		{
			_restService = restService;
			_matterStatusGetChoiceIdByNameStrategy = matterStatusGetChoiceIdByNameStrategy;
			_clientRequireStrategy = clientRequireStrategy;
			_getByIdStrategy = getByIdStrategy;
		}

		protected override Matter DoCreate(Matter entity)
		{
			entity.Client = _clientRequireStrategy.Require(entity.Client);
			int statusId = GetStatusId(entity.Status);

			object dto = new MatterCreateRequestPreOsier(entity, statusId);

			var artifactID = _restService.Post<int>("Relativity.Services.Matter.IMatterModule/Matter%20Manager/CreateSingleAsync", dto);

			return _getByIdStrategy.Get(artifactID);
		}

		private int GetStatusId(string status)
		{
			return _matterStatusGetChoiceIdByNameStrategy.GetId(status);
		}
	}
}
