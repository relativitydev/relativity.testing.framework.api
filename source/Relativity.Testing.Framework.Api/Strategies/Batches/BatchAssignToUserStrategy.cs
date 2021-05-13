using Relativity.Testing.Framework.Api.Services;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchAssignToUserStrategy : IBatchAssignToUserStrategy
	{
		private readonly IRestService _restService;

		public BatchAssignToUserStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void AssignToUser(int workspaceId, int batchId, int userId)
		{
			var dto = new
			{
				batchCheckout = new
				{
					UserId = userId
				}
			};

			_restService.Post($"Relativity.Services.Review.Batching.IBatchingModule/workspaces/{workspaceId}/batches/{batchId}/assignment", dto);
		}
	}
}
