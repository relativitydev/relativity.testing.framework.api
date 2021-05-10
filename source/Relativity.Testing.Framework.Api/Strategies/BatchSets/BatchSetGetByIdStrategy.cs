using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<BatchSet>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<BatchSet> _existsWorkspaceEntityByIdStrategy;

		public BatchSetGetByIdStrategy(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<BatchSet> existsWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		public BatchSet Get(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
			{
				return null;
			}

			return _restService.Get<BatchSet>($"Relativity.Services.Review.Batching.IBatchingModule/workspaces/{workspaceId}/batching/ReadFull/{entityId}");
		}
	}
}
