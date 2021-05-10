using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class TabGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<Tab>
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public TabGetByIdStrategy(IRestService restService, IObjectService objectService)
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

			var result = _restService.Get<JObject>($"Relativity.Rest/API/Relativity.Tabs/workspace/{workspaceId}/tabs/{entityId}");

			result["RelativityApplications"] = result["RelativityApplications"]["ViewableItems"];
			result["Parent"] = result["Parent"]["Value"];

			return result.ToObject<Tab>();
		}
	}
}
