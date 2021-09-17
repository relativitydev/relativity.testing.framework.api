using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.Layouts
{
	internal abstract class LayoutCreateAbstractStrategy : CreateWorkspaceEntityStrategy<Layout>
	{
		private readonly ICreateWorkspaceEntityStrategy<ObjectType> _createWorkspaceEntityStrategy;

		protected LayoutCreateAbstractStrategy(ICreateWorkspaceEntityStrategy<ObjectType> createWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
		}

		protected override Layout DoCreate(int workspaceId, Layout entity)
		{
			entity = FillRequiredProperties(workspaceId, entity);

			var dto = new
			{
				layoutRequest = ConvertTab(entity)
			};

			return SendRequest(workspaceId, dto, entity);
		}

		protected abstract Layout SendRequest(int workspaceId, object dto, Layout entity);

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
