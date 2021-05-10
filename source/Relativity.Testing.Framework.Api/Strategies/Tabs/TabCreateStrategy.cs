using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabCreateStrategy : CreateWorkspaceEntityStrategy<Tab>
	{
		private readonly IRestService _restService;
		private readonly ICreateWorkspaceEntityStrategy<ObjectType> _createWorkspaceEntityStrategy;

		public TabCreateStrategy(
			IRestService restService,
			ICreateWorkspaceEntityStrategy<ObjectType> createWorkspaceEntityStrategy)
		{
			_restService = restService;
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
		}

		protected override Tab DoCreate(int workspaceId, Tab entity)
		{
			entity = FillRequiredProperties(workspaceId, entity);

			var dto = new
			{
				tab = ConvertTab(workspaceId, entity)
			};

			var artifactId = _restService.Post<int>($"Relativity.Rest/API/Relativity.Tabs/workspace/{workspaceId}/tabs", dto);
			entity.ArtifactID = artifactId;
			return entity;
		}

		private JObject ConvertTab(int workspaceId, Tab entity)
		{
			var tab = JObject.FromObject(entity);

			if (entity.Parent == null)
			{
				entity.Parent = workspaceId == -1 ? new Artifact { ArtifactID = 62 } : new Artifact { ArtifactID = 1003663 };
			}

			tab["Parent"] = JToken.FromObject(new Securable<Artifact>(entity.Parent));
			tab["LinkType"] = (int)entity.LinkType;
			tab["ObjectType"] = entity.LinkType == TabLinkType.Object
				? JToken.FromObject(JObject.FromObject(entity.ObjectType).ToObject<NamedArtifact>())
				: null;

			return tab;
		}

		private Tab FillRequiredProperties(int workspaceId, Tab entity)
		{
			if (string.IsNullOrEmpty(entity.Name))
			{
				entity.Name = Randomizer.GetString("AT_");
			}

			if (entity.ObjectType == null &&
				entity.LinkType == TabLinkType.Object)
			{
				entity.ObjectType = _createWorkspaceEntityStrategy.Create(workspaceId, new ObjectType());
			}

			return entity;
		}
	}
}
