using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsRunStrategy : IProductionsRunStrategy
	{
		private readonly IRestService _restService;
		private readonly IProductionsWaitForStatusStrategy _productionsWaitForStatusStrategy;

		public ProductionsRunStrategy(
			IRestService restService,
			IProductionsWaitForStatusStrategy productionsWaitForStatusStrategy)
		{
			_restService = restService;
			_productionsWaitForStatusStrategy = productionsWaitForStatusStrategy;
		}

		public void Run(int workspaceId, int entityId, int timeout = 120)
		{
			var dto = new
			{
				workspaceArtifactID = workspaceId,
				productionArtifactID = entityId,
				suppressWarnings = true,
				overrideConflicts = false
			};

			_restService.Post("Relativity.Productions.Services.IProductionModule/Production%20Manager/RunProductionAsync", dto);

			_productionsWaitForStatusStrategy.WaitForStatus(workspaceId, entityId, ProductionStatus.Produced, timeout);
		}
	}
}
