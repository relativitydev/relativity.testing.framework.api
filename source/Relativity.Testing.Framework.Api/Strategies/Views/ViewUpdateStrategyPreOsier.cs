using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ViewUpdateStrategyPreOsier : IUpdateWorkspaceEntityStrategy<View>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<View> _getWorkspaceEntityByIdStrategy;

		public ViewUpdateStrategyPreOsier(IRestService restService, IGetWorkspaceEntityByIdStrategy<View> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public View Update(int workspaceId, View entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var dto = new
			{
				workspaceArtifactID = workspaceId,
				viewDTO = entity
			};

			_restService.Post("Relativity.Services.View.IViewModule/View%20Manager/UpdateSingleAsync", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
		}
	}
}
