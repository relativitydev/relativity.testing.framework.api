using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class MatterGetEligibleStatusesStrategyV1 : IMatterGetEligibleStatusesStrategy
	{
		private readonly IRestService _restService;

		public MatterGetEligibleStatusesStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public ArtifactIdNamePair[] GetAll()
		{
			return GetAllAsync().Result;
		}

		public async Task<ArtifactIdNamePair[]> GetAllAsync()
		{
			return await _restService.GetAsync<ArtifactIdNamePair[]>(
				"relativity-environment/v1/workspaces/-1/matters/eligible-statuses").ConfigureAwait(false);
		}
	}
}
