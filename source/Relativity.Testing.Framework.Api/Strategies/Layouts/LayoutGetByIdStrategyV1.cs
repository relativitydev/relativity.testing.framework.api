using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0")]
	internal class LayoutGetByIdStrategyV1 : IGetWorkspaceEntityByIdStrategy<Layout>
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public LayoutGetByIdStrategyV1(IRestService restService, IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public Layout Get(int workspaceId, int entityId)
		{
			var isExist = _objectService.Query<Layout>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.ArtifactID, entityId)
				.Any();

			if (!isExist)
			{
				return null;
			}

			var result = _restService.Get<JObject>($"Relativity.Rest/API/Relativity.Layouts/workspace/{workspaceId}/layouts/{entityId}");

			result["ObjectType"] = result["ObjectType"]["Value"];
			result["RelativityApplications"] = result["RelativityApplications"]["ViewableItems"];

			return result.ToObject<Layout>();
		}
	}
}
