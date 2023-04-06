using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class BatchSetPurgeBatchesStrategyPrePrairieSmoke : IPurgeBatchesStrategy
	{
		private readonly IRestService _restService;

		public BatchSetPurgeBatchesStrategyPrePrairieSmoke(
			IRestService restService)
		{
			_restService = restService;
		}

		public BatchProcessResult PurgeBatches(int workspaceId, int entityId, UserCredentials userCredentials = null)
		{
			var dto = new
			{
				workspaceID = workspaceId,
				batchSetArtifactID = entityId
			};

			return _restService.Post<BatchProcessResult>("Relativity.Services.Review.Batching.IBatchingModule/BatchSetManager/PurgeBatchesAsync", dto, userCredentials: userCredentials);
		}
	}
}
