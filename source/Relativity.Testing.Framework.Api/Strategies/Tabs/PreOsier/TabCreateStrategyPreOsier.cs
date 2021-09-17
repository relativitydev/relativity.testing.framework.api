using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class TabCreateStrategyPreOsier : CreateWorkspaceEntityStrategy<Tab>
	{
		private readonly IRestService _restService;
		private readonly ITabFillRequiredPropertiesStrategy _tabFillRequiredPropertiesStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Tab> _getWorkspaceEntityByIdStrategy;

		public TabCreateStrategyPreOsier(
			IRestService restService,
			ITabFillRequiredPropertiesStrategy tabFillRequiredPropertiesStrategy,
			IGetWorkspaceEntityByIdStrategy<Tab> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_tabFillRequiredPropertiesStrategy = tabFillRequiredPropertiesStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		protected override Tab DoCreate(int workspaceId, Tab entity)
		{
			entity = _tabFillRequiredPropertiesStrategy.FillRequiredProperties(workspaceId, entity);

			TabDtoPreOsier tabDto = new TabDtoPreOsier
			{
				Tab = TabRequestPreOsier.FromTab(workspaceId, entity)
			};

			int artifactId = _restService.Post<int>($"Relativity.Tabs/workspace/{workspaceId}/tabs", tabDto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, artifactId);
		}
	}
}
