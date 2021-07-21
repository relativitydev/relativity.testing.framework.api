using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class TabGetByNameStrategyPrePrairieSmoke : IGetWorkspaceEntityByNameStrategy<Tab>
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public TabGetByNameStrategyPrePrairieSmoke(IRestService restService, IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public Tab Get(int workspaceId, string entityName)
		{
			Tab tab = _objectService.Query<Tab>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.Name, entityName)
				.FirstOrDefault();

			if (tab == null)
			{
				return null;
			}

			TabResponsePrePrairieSmoke result = _restService.Get<TabResponsePrePrairieSmoke>($"Relativity.Rest/API/Relativity.Tabs/workspace/{workspaceId}/tabs/{tab.ArtifactID}");

			tab = result.ToTab();

			return tab;
		}
	}
}
