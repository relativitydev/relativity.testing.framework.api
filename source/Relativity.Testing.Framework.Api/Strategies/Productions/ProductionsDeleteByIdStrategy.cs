using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsDeleteByIdStrategy : DeleteWorkspaceEntityByIdStrategy<Production>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<Production> _existsWorkspaceEntityByIdStrategy;
		private readonly IWaitDeleteWorkspaceEntityStrategy _waitDeleteWorkspaceEntityStrategy;

		public ProductionsDeleteByIdStrategy(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<Production> existsWorkspaceEntityByIdStrategy,
			IWaitDeleteWorkspaceEntityStrategy waitDeleteWorkspaceEntityStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
			_waitDeleteWorkspaceEntityStrategy = waitDeleteWorkspaceEntityStrategy;
		}

		protected override void DoDelete(int workspaceId, int entityId)
		{
			_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId);

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				productionArtifactID = entityId
			};

			_restService.Post("Relativity.Productions.Services.IProductionModule/Production%20Manager/DeleteSingleAsync", dto);

			_waitDeleteWorkspaceEntityStrategy.Wait<Production>(workspaceId, entityId);
		}
	}
}
