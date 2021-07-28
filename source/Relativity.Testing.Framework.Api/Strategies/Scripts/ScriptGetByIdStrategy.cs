using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ScriptGetByIdStrategy : IGetWorkspaceEntityByIdStrategy<Script>
	{
		private readonly IRestService _restService;

		private readonly IObjectService _objectService;

		public ScriptGetByIdStrategy(
			IRestService restService,
			IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public Script Get(int workspaceId, int entityId)
		{
			bool isExist = _objectService.Query<Script>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.ArtifactID, entityId)
				.Any();

			if (!isExist)
			{
				return null;
			}

			var result = _restService.Get<JObject>($"Relativity.Scripts/workspace/{workspaceId}/Scripts/{entityId}");

			////result["RelativityApplications"] = result["RelativityApplications"]["ViewableItems"];

			return result.ToObject<Script>();
		}
	}
}
