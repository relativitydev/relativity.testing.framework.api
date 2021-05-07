using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsPlaceholderGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<ProductionPlaceholder> _existsWorkspaceEntityByIdStrategy;

		public ProductionsPlaceholderGetByIdStrategy(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<ProductionPlaceholder> existsWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		public ProductionPlaceholder Get(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
			{
				return null;
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				placeholderArtifactID = entityId
			};

			return _restService.Post<ProductionPlaceholder>("Relativity.Productions.Services.IProductionModule/Production%20Placeholder%20Manager/ReadSingleAsync", dto);
		}
	}
}
