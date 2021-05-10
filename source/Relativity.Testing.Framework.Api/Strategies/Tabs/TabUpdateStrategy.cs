using System;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabUpdateStrategy : IUpdateWorkspaceEntityStrategy<Tab>
	{
		private readonly IRestService _restService;

		public TabUpdateStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, Tab entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			var dto = new
			{
				tab = ConvertTab(workspaceId, entity)
			};

			_restService.Put($"Relativity.Tabs/workspace/{workspaceId}/tabs", dto);
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
	}
}
