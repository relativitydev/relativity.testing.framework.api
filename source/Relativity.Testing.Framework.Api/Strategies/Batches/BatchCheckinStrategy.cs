using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class BatchCheckinStrategy : IBatchCheckinStrategy
	{
		private readonly IRestService _restService;

		public BatchCheckinStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void Checkin(int workspaceId, int batchId, bool isCompleted)
		{
			_restService.Post($"relativity-review/v1/workspaces/{workspaceId}/batches/{batchId}/checkin?completed={isCompleted}");
		}
	}
}
