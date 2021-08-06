﻿using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0 <12.1")]
	internal class LayoutUpdateStrategyNinebark : IUpdateWorkspaceEntityStrategy<Layout>
	{
		private readonly IRestService _restService;

		public LayoutUpdateStrategyNinebark(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(int workspaceId, Layout entity)
		{
			var dto = new
			{
				layoutRequest = ConvertLayout(entity)
			};

			_restService.Put($"Relativity.Layouts/workspace/{workspaceId}/layouts/{entity.ArtifactID}", dto);
		}

		private JObject ConvertLayout(Layout entity)
		{
			var tab = JObject.FromObject(entity);

			tab["ObjectType"] = JToken.FromObject(new Securable<NamedArtifact>(JObject.FromObject(entity.ObjectType).ToObject<NamedArtifact>()));

			return tab;
		}
	}
}
