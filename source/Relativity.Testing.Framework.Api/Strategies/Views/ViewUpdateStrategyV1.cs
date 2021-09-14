using System;
using Relativity.Testing.Framework.Api.DTO;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ViewUpdateStrategyV1 : IUpdateWorkspaceEntityStrategy<View>
	{
		private readonly IRestService _restService;

		public ViewUpdateStrategyV1(IRestService restService)
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
				viewRequest = ViewDTOMapper.ConvertToDTO(entity)
			};

			_restService.Put($"relativity-data-visualization/V1/workspaces/{workspaceId}/views", dto);
		}
	}
}
