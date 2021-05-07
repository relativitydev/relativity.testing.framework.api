using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ViewGetAccessStatusStrategy : IGetWorkspaceEntityByIdStrategy<ViewAccessStatus>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<View> _existsWorkspaceEntityByIdStrategy;

		public ViewGetAccessStatusStrategy(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<View> existsWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		public ViewAccessStatus Get(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
				return null;

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				artifactID = entityId
			};

			return _restService.Post<ViewAccessStatus>("Relativity.Services.View.IViewModule/View%20Manager/GetAccessStatusAsync", dto);
		}
	}
}
