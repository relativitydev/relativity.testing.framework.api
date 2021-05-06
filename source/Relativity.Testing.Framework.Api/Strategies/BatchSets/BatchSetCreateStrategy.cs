using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetCreateStrategy : CreateWorkspaceEntityStrategy<BatchSet>
	{
		private readonly IRestService _restService;

		public BatchSetCreateStrategy(IRestService restService)
		{
			_restService = restService;
		}

		protected override BatchSet DoCreate(int workspaceId, BatchSet entity)
		{
			var dto = new
			{
				batchSet = new
				{
					entity.Name,
					entity.BatchSize,
					entity.BatchPrefix,
					entity.DataSource,
					entity.BatchUnitField,
					entity.FamilyField,
					entity.ReviewedField,
					entity.AutoBatchSettings
				}
			};

			return _restService.Post<BatchSet>($"Relativity.Services.Review.Batching.IBatchingModule/workspaces/{workspaceId}/batching/CreateBatchSet", dto);
		}
	}
}
