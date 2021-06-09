using System;
using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetCreateStrategy : ICreateBatchSetStrategy
	{
		private readonly IRestService _restService;

		public BatchSetCreateStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public BatchSet Create(int workspaceId, BatchSet entity, UserCredentials userCredentials = null)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

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

			return _restService.Post<BatchSet>($"Relativity.Services.Review.Batching.IBatchingModule/workspaces/{workspaceId}/batching/CreateBatchSet", dto, userCredentials: userCredentials);
		}
	}
}
