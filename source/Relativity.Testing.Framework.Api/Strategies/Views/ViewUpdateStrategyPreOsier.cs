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

		public ViewUpdateStrategyPreOsier(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, View entity)
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
		}
	}
}
