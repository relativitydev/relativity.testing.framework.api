using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ViewCreateStrategy : CreateWorkspaceEntityStrategy<View>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<View> _getWorkspaceEntityByIdStrategy;

		public ViewCreateStrategy(
			IRestService restService,
			IGetWorkspaceEntityByIdStrategy<View> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		protected override View DoCreate(int workspaceId, View entity)
		{
			entity.FillRequiredProperties();

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				viewDTO = entity
			};

			var artifactId = _restService.Post<int>("Relativity.Services.View.IViewModule/View%20Manager/CreateSingleAsync", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifactId);
		}
	}
}
