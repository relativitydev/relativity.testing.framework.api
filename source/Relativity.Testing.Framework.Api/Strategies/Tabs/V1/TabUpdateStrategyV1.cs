using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class TabUpdateStrategyV1 : IUpdateWorkspaceEntityStrategy<Tab>
	{
		private readonly IRestService _restService;

		public TabUpdateStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, Tab entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			TabDtoV1 tabDto = new TabDtoV1
			{
				TabRequest = TabRequestV1.FromTab(workspaceId, entity)
			};

			_restService.Put($"relativity-data-visualization/v1/workspaces/{workspaceId}/tabs/{tabDto.TabRequest.ArtifactID}", tabDto);
		}
	}
}
