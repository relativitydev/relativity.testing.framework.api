using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ProductionsDataSourceUpdateStrategyV1 : IUpdateProductionsDataSourceStrategy
	{
		private readonly IRestService _restService;

		public ProductionsDataSourceUpdateStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, int productionId, ProductionDataSource entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var dto = new
			{
				dataSource = entity
			};

			_restService.Put($"relativity-productions/V1/workspaces/{workspaceId}/productions/{productionId}/production-data-sources", dto);
		}
	}
}
