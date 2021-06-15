using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabGetByNameStrategy : IGetWorkspaceEntityByNameStrategy<Tab>
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public TabGetByNameStrategy(IRestService restService, IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public Tab Get(int workspaceId, string entityName)
		{
			var tab = _objectService.Query<Tab>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.Name, entityName)
				.FirstOrDefault();

			if (tab == null)
			{
				return null;
			}

			var result = _restService.Get<JObject>($"Relativity.Rest/API/Relativity.Tabs/workspace/{workspaceId}/tabs/{tab.ArtifactID}");

			result["RelativityApplications"] = result["RelativityApplications"]["ViewableItems"];
			result["Parent"] = result["Parent"]["Value"];

			return result.ToObject<Tab>();
		}
	}
}
