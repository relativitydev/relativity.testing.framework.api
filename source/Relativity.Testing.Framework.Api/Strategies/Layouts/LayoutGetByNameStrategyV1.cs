using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0")]
	internal class LayoutGetByNameStrategyV1 : IGetWorkspaceEntityByNameStrategy<Layout>
	{
		private readonly IRestService _restService;
		private readonly IObjectService _objectService;

		public LayoutGetByNameStrategyV1(IRestService restService, IObjectService objectService)
		{
			_restService = restService;
			_objectService = objectService;
		}

		public Layout Get(int workspaceId, string entityName)
		{
			var entity = _objectService.Query<Layout>()
				.For(workspaceId)
				.FetchOnlyArtifactID()
				.Where(x => x.Name, entityName)
				.FirstOrDefault();

			if (entity == null)
			{
				return null;
			}

			var result = _restService.Get<JObject>($"Relativity.Rest/API/Relativity.Layouts/workspace/{workspaceId}/layouts/{entity.ArtifactID}");

			result["ObjectType"] = result["ObjectType"]["Value"];
			result["RelativityApplications"] = result["RelativityApplications"]["ViewableItems"];

			return result.ToObject<Layout>();
		}
	}
}
