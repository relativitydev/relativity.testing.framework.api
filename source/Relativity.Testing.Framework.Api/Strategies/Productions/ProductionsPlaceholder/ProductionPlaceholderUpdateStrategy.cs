using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionPlaceholderUpdateStrategy : IUpdateWorkspaceEntityStrategy<ProductionPlaceholder>
	{
		private readonly IRestService _restService;

		public ProductionPlaceholderUpdateStrategy(IRestService restService)
		{
		_restService = restService;
		}

		public void Update(int workspaceId, ProductionPlaceholder entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				placeholder = entity
			};

			_restService.Post($"Relativity.Productions.Services.IProductionModule/Production%20Placeholder%20Manager/UpdateSingleAsync", dto);
		}
	}
}
