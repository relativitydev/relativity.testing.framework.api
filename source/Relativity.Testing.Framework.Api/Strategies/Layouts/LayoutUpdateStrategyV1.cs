using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class LayoutUpdateStrategyV1 : IUpdateWorkspaceEntityStrategy<Layout>
	{
		private readonly IRestService _restService;
		private readonly IGetWorkspaceEntityByIdStrategy<Layout> _getWorkspaceEntityByIdStrategy;

		public LayoutUpdateStrategyV1(IRestService restService, IGetWorkspaceEntityByIdStrategy<Layout> getWorkspaceEntityByIdStrategy)
		{
			_restService = restService;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
		}

		public Layout Update(int workspaceId, Layout entity)
		{
			var dto = new
			{
				layoutRequest = ConvertTab(entity)
			};

			_restService.Put($"relativity-data-visualization/v1/workspaces/{workspaceId}/layouts/{entity.ArtifactID}", dto);

			return _getWorkspaceEntityByIdStrategy.Get(workspaceId, entity.ArtifactID);
		}

		private JObject ConvertTab(Layout entity)
		{
			var tab = JObject.FromObject(entity);

			tab["ObjectType"] = JToken.FromObject(new Securable<NamedArtifact>(JObject.FromObject(entity.ObjectType).ToObject<NamedArtifact>()));
			return tab;
		}
	}
}
