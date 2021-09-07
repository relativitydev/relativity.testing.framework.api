using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ProductionPlaceholderUpdateStrategyV1 : IUpdateWorkspaceEntityStrategy<ProductionPlaceholder>
	{
		private readonly IRestService _restService;

		public ProductionPlaceholderUpdateStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, ProductionPlaceholder entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var url = GetUrl(workspaceId);
			var dto = GetDto(entity);

			_restService.Put(url, dto);
		}

		private string GetUrl(int workspaceId)
		{
			return $"relativity-productions/v1/workspaces/{workspaceId}/production-placeholders";
		}

		private object GetDto(ProductionPlaceholder entity)
		{
			return new
			{
				placeholder = entity
			};
		}
	}
}
