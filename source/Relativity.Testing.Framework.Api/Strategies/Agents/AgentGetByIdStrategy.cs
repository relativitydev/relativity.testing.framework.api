using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class AgentGetByIdStrategy : IGetByIdStrategy<Agent>
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public AgentGetByIdStrategy(
			IRestService restService,
			IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public Agent Get(int id)
		{
			var isExist = _objectService.Query<Agent>().
				FetchOnlyArtifactID().
				Where(x => x.ArtifactID, id).
				Any();

			if (!isExist)
			{
				return null;
			}

			var result = _restService.Get<JObject>($"relativity.agents/workspace/-1/agents/{id}");
			result["AgentType"] = result["AgentType"]["Value"];
			result["AgentServer"] = result["AgentServer"]["Value"];
			result.Add("RunInterval", result["Interval"]);
			return result.ToObject<Agent>();
		}
	}
}
