using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ViewUpdateStrategy : IUpdateWorkspaceEntityStrategy<View>
	{
		private readonly IRestService _restService;

		public ViewUpdateStrategy(IRestService restService)
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
