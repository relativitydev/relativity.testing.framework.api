using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class BatchCheckoutStrategy : IBatchCheckoutStrategy
	{
		private readonly IRestService _restService;

		public BatchCheckoutStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void Checkout(int workspaceId, int batchId, int userId)
		{
			var dto = new
			{
				request = new
				{
					UserID = userId
				}
			};

			_restService.Post($"relativity-review/v1/workspaces/{workspaceId}/batches/{batchId}/checkout", dto);
		}
	}
}
