using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class MatterCreateStrategyV1 : CreateStrategy<Matter>
	{
		private readonly IRestService _restService;

		private readonly IMatterStatusGetChoiceIdByNameStrategy _matterStatusGetChoiceIdByNameStrategy;

		private readonly IRequireStrategy<Client> _clientRequireStrategy;

		public MatterCreateStrategyV1(
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
			int statusId = GetStatusId(entity.Status);

			var dto = new MatterCreateRequestV1(entity, statusId);

			entity.ArtifactID = _restService.Post<int>("relativity-environment/v1/workspaces/-1/matters", dto);

			return entity;
		}

		private int GetStatusId(string status)
		{
			return _matterStatusGetChoiceIdByNameStrategy.GetId(status);
		}
	}
}
