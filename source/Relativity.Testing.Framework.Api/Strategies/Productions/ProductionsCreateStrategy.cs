using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ProductionsCreateStrategy : CreateWorkspaceEntityStrategy<Production>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<Production> _getWorkspaceEntityByIdStrategy;
		private readonly IWaitCreateWorkspaceEntityStrategy _waitCreateWorkspaceEntityStrategy;

		public ProductionsCreateStrategy(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<Production> getWorkspaceEntityByIdStrategy,
			IWaitCreateWorkspaceEntityStrategy waitCreateWorkspaceEntityStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_waitCreateWorkspaceEntityStrategy = waitCreateWorkspaceEntityStrategy;
		}

		protected override Production DoCreate(int workspaceId, Production entity)
		{
			entity.FillRequiredProperties();

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				Production = entity
			};

			var artifactId = _restService.Post<int>("Relativity.Productions.Services.IProductionModule/Production Manager/CreateSingleAsync", dto);

			_waitCreateWorkspaceEntityStrategy.Wait<Production>(workspaceId, artifactId);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifactId);
		}
	}
}
