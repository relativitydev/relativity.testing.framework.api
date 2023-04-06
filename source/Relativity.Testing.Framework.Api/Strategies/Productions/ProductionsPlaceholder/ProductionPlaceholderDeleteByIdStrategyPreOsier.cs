using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ProductionPlaceholderDeleteByIdStrategyPreOsier : DeleteWorkspaceEntityByIdStrategy<ProductionPlaceholder>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<ProductionPlaceholder> _existsWorkspaceEntityByIdStrategy;

		public ProductionPlaceholderDeleteByIdStrategyPreOsier(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<ProductionPlaceholder> existsWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				placeholderArtifactID = entityId
			};

			_restService.Post($"Relativity.Productions.Services.IProductionModule/Production%20Placeholder%20Manager/DeleteSingleAsync", dto);
		}
	}
}
