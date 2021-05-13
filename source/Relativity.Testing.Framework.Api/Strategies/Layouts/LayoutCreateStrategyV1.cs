using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.0")]
	internal class LayoutCreateStrategyV1 : CreateWorkspaceEntityStrategy<Layout>
	{
		private readonly IRestService _restService;
		private readonly ICreateWorkspaceEntityStrategy<ObjectType> _createWorkspaceEntityStrategy;

		public LayoutCreateStrategyV1(
			IRestService restService,
			ICreateWorkspaceEntityStrategy<ObjectType> createWorkspaceEntityStrategy)
		{
			_restService = restService;
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
		}

		protected override Layout DoCreate(int workspaceId, Layout entity)
		{
			entity = FillRequiredProperties(workspaceId, entity);

			var dto = new
			{
				layoutRequest = ConvertTab(entity)
			};

			var result = _restService.Post<JObject>($"Relativity.Layouts/workspace/{workspaceId}/layouts", dto);
			result["ObjectType"] = result["ObjectType"]["Value"];
			result["RelativityApplications"] = result["RelativityApplications"]["ViewableItems"];
			return result.ToObject<Layout>();
		}

		private JObject ConvertTab(Layout entity)
		{
			var tab = JObject.FromObject(entity);

			tab["ObjectType"] = JToken.FromObject(new Securable<NamedArtifact>(JObject.FromObject(entity.ObjectType).ToObject<NamedArtifact>()));

			return tab;
		}

		private Layout FillRequiredProperties(int workspaceId, Layout entity)
		{
			if (string.IsNullOrEmpty(entity.Name))
			{
				entity.Name = Randomizer.GetString("AT_");
			}

			if (entity.ObjectType == null)
			{
				entity.ObjectType = _createWorkspaceEntityStrategy.Create(workspaceId, new ObjectType());
			}

			return entity;
		}
	}
}
