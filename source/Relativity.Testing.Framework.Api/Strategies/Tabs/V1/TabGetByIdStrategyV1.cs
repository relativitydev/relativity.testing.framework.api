using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class TabGetByIdStrategyV1 : IGetWorkspaceEntityByIdStrategy<Tab>
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public TabGetByIdStrategyV1(IRestService restService, IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public Tab Get(int workspaceId, int entityId)
		{
			var isExist = _objectService.Query<Tab>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.ArtifactID, entityId)
				.Any();

			if (!isExist)
			{
				return null;
			}

			TabResponseV1 result = _restService.Get<TabResponseV1>($"relativity-data-visualization/v1/workspaces/{workspaceId}/tabs/{entityId}");

			Tab tab = result.ToTab();

			return tab;
		}
	}
}
