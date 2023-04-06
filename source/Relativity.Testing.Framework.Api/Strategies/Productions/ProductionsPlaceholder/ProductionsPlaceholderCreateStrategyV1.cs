using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ProductionsPlaceholderCreateStrategyV1 : CreateWorkspaceEntityStrategy<ProductionPlaceholder>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder> _getWorkspaceEntityByIdStrategy;

		public ProductionsPlaceholderCreateStrategyV1(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		protected override ProductionPlaceholder DoCreate(int workspaceId, ProductionPlaceholder entity)
		{
			entity.FillRequiredProperties();

			var url = GetUrl(workspaceId);
			var dto = GetDto(entity);

			entity.ArtifactID = _restService.Post<int>(url, dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
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
