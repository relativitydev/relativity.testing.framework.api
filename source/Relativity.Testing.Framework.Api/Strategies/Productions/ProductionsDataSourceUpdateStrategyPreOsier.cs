using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ProductionsDataSourceUpdateStrategyPreOsier : IUpdateProductionsDataSourceStrategy
	{
		private readonly IRestService _restService;

		public ProductionsDataSourceUpdateStrategyPreOsier(IRestService restService)
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
				workspaceArtifactID = workspaceId,
				productionID = productionId,
				dataSource = entity
			};

			_restService.Post("Relativity.Productions.Services.IProductionModule/Production%20Data%20Source%20Manager/UpdateSingleAsync", dto);
		}
	}
}
