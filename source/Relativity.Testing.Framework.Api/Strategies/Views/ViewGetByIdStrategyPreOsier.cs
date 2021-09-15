using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ViewGetByIdStrategyPreOsier : IGetWorkspaceEntityByIdStrategy<View>
	{
		private readonly IRestService _restService;
		private readonly IExistsWorkspaceEntityByIdStrategy<View> _existsWorkspaceEntityByIdStrategy;

		public ViewGetByIdStrategyPreOsier(
			IRestService restService,
			IExistsWorkspaceEntityByIdStrategy<View> existsWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_existsWorkspaceEntityByIdStrategy = existsWorkspaceEntityByIdStrategy;
		}

		public View Get(int workspaceId, int entityId)
		{
			if (!_existsWorkspaceEntityByIdStrategy.Exists(workspaceId, entityId))
			{
				return null;
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				viewArtifactID = entityId
			};

			return _restService.Post<View>("Relativity.Services.View.IViewModule/View%20Manager/ReadSingleAsync", dto);
		}
	}
}
