using System.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class MatterGetByIdStrategyV1 : IMatterGetByIdStrategy
	{
		private readonly IRestService _restService;
		private readonly IMatterGetEligibleStatusesStrategy _matterGetEligibleStatusesStrategy;
		private readonly IMatterGetEligibleClientsStrategy _matterGetEligibleClientsStrategy;
		private readonly IArtifactIdValidator _artifactIdValidator;

		public MatterGetByIdStrategyV1(
			IRestService restService,
			IMatterGetEligibleStatusesStrategy matterGetEligibleStatusesStrategy,
			IMatterGetEligibleClientsStrategy matterGetEligibleClientsStrategy,
			IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_matterGetEligibleStatusesStrategy = matterGetEligibleStatusesStrategy;
			_matterGetEligibleClientsStrategy = matterGetEligibleClientsStrategy;
			_artifactIdValidator = artifactIdValidator;
		}

		public Matter Get(int id)
		{
			_artifactIdValidator.Validate(id, "Matter");

			MatterDTOV1 matterDTO = _restService.Get<MatterDTOV1>(
				$"relativity-environment/v1/workspaces/-1/matters/{id}");
			string statusName = GetStatusNameById(matterDTO.Status.Value.ArtifactID);
			ArtifactIdNamePair client = GetClientById(matterDTO.Client.Value.ArtifactID);

			Matter mappedMatter = matterDTO.DoMappingFromDTO(client, statusName);
			return mappedMatter;
		}

		public Matter GetWithExtendedMetadata(int id)
		{
			_artifactIdValidator.Validate(id, "Matter");

			MatterExtendedMetadataDTOV1 matterDTO = _restService.Get<MatterExtendedMetadataDTOV1>(
				$"relativity-environment/v1/workspaces/-1/matters/{id}/true/true");
			string statusName = GetStatusNameById(matterDTO.Status.Value.ArtifactID);
			ArtifactIdNamePair client = GetClientById(matterDTO.Client.Value.ArtifactID);

			Matter mappedMatter = matterDTO.DoMappingFromExtendedMetadataDTO(client, statusName);
			return mappedMatter;
		}

		private string GetStatusNameById(int statusID)
		{
			return _matterGetEligibleStatusesStrategy.GetAllAsync().Result
				.FirstOrDefault(status => status.ArtifactID == statusID).Name;
		}

		private ArtifactIdNamePair GetClientById(int statusID)
		{
			return _matterGetEligibleClientsStrategy.GetAllAsync().Result
				.FirstOrDefault(client => client.ArtifactID == statusID);
		}
	}
}
