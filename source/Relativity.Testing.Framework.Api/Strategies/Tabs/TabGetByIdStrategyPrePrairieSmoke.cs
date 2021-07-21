using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class TabGetByIdStrategyPrePrairieSmoke : IGetWorkspaceEntityByIdStrategy<Tab>
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public TabGetByIdStrategyPrePrairieSmoke(IRestService restService, IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public Tab Get(int workspaceId, int entityId)
		{
			bool isExist = _objectService.Query<Tab>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.ArtifactID, entityId)
				.Any();

			if (!isExist)
			{
				return null;
			}

			TabResponsePrePrairieSmoke result = _restService.Get<TabResponsePrePrairieSmoke>($"Relativity.Rest/API/Relativity.Tabs/workspace/{workspaceId}/tabs/{entityId}");

			Tab tab = result.ToTab();

			return tab;
		}
	}
}
