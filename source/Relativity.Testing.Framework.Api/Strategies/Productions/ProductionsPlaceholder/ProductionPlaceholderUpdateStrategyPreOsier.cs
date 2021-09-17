using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ProductionPlaceholderUpdateStrategyPreOsier : IUpdateWorkspaceEntityStrategy<ProductionPlaceholder>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder> _getWorkspaceEntityByIdStrategy;

		public ProductionPlaceholderUpdateStrategyPreOsier(IRestService restService, IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public ProductionPlaceholder Update(int workspaceId, ProductionPlaceholder entity)
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

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
		}
	}
}
