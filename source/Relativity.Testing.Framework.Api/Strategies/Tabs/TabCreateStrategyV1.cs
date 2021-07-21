using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class TabCreateStrategyV1 : CreateWorkspaceEntityStrategy<Tab>
	{
		private readonly IRestService _restService;
		private readonly ITabFillRequiredPropertiesStrategy _tabFillRequiredPropertiesStrategy;

		public TabCreateStrategyV1(
			IRestService restService,
			ITabFillRequiredPropertiesStrategy tabFillRequiredPropertiesStrategy)
		{
			_restService = restService;
			_tabFillRequiredPropertiesStrategy = tabFillRequiredPropertiesStrategy;
		}

		protected override Tab DoCreate(int workspaceId, Tab entity)
		{
			entity = _tabFillRequiredPropertiesStrategy.FillRequiredProperties(workspaceId, entity);

			TabDtoV1 tabDto = new TabDtoV1
			{
				TabRequest = TabRequestV1.FromTab(workspaceId, entity)
			};

			TabResponseV1 tabResponse = _restService.Post<TabResponseV1>($"/Relativity.Rest/API/relativity-data-visualization/v1/workspaces/{workspaceId}/tabs/", tabDto);

			entity = tabResponse.ToTab();

			return entity;
		}
	}
}
