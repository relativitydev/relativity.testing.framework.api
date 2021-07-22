using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class TabCreateStrategyPrePrairieSmoke : CreateWorkspaceEntityStrategy<Tab>
	{
		private readonly IRestService _restService;
		private readonly ITabFillRequiredPropertiesStrategy _tabFillRequiredPropertiesStrategy;

		public TabCreateStrategyPrePrairieSmoke(
			IRestService restService,
			ITabFillRequiredPropertiesStrategy tabFillRequiredPropertiesStrategy)
		{
			_restService = restService;
			_tabFillRequiredPropertiesStrategy = tabFillRequiredPropertiesStrategy;
		}

		protected override Tab DoCreate(int workspaceId, Tab entity)
		{
			entity = _tabFillRequiredPropertiesStrategy.FillRequiredProperties(workspaceId, entity);

			TabDtoPrePrairieSmoke tabDto = new TabDtoPrePrairieSmoke
			{
				Tab = TabRequestPrePrairieSmoke.FromTab(workspaceId, entity)
			};

			int artifactId = _restService.Post<int>($"Relativity.Tabs/workspace/{workspaceId}/tabs", tabDto);
			entity.ArtifactID = artifactId;

			return entity;
		}
	}
}
