using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsStageStrategy : IProductionsStageStrategy
	{
		private readonly IRestService _restService;
		private readonly IProductionsWaitForStatusStrategy _productionsWaitForStatusStrategy;

		public ProductionsStageStrategy(
			IRestService restService,
			IProductionsWaitForStatusStrategy productionsWaitForStatusStrategy)
		{
			_restService = restService;
			_productionsWaitForStatusStrategy = productionsWaitForStatusStrategy;
		}

		public void Stage(int workspaceId, int entityId, int seconds = 60)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				productionArtifactID = entityId
			};

			_restService.Post("Relativity.Productions.Services.IProductionModule/Production%20Manager/StageProductionAsync", dto);

			_productionsWaitForStatusStrategy.WaitForStatus(workspaceId, entityId, ProductionStatus.Staged, seconds);
		}
	}
}
