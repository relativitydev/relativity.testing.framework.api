using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ScriptGetByNameStrategy : IGetWorkspaceEntityByNameStrategy<Script>
	{
		private readonly IRestService _restService;

		private readonly IObjectService _objectService;

		public ScriptGetByNameStrategy(
			IRestService restService,
			IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public Script Get(int workspaceId, string entityName)
		{
			Script script = _objectService.Query<Script>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.Name, entityName)
				.FirstOrDefault();

			if (script != null)
			{
				return null;
			}

			var result = _restService.Get<JObject>($"Relativity.Scripts/workspace/{workspaceId}/Scripts/{script.ArtifactID}");

			result["RelativityApplications"] = result["RelativityApplications"]["ViewableItems"];

			return result.ToObject<Script>();
		}
	}
}
