using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabFillRequiredPropertiesStrategy : ITabFillRequiredPropertiesStrategy
	{
		private readonly ICreateWorkspaceEntityStrategy<ObjectType> _createWorkspaceEntityStrategy;

		public TabFillRequiredPropertiesStrategy(
			ICreateWorkspaceEntityStrategy<ObjectType> createWorkspaceEntityStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
		}

		public Tab FillRequiredProperties(int workspaceID, Tab entity)
		{
			entity.FillRequiredProperties();

			if (entity.ObjectType == null && entity.LinkType == TabLinkType.Object)
			{
				entity.ObjectType = _createWorkspaceEntityStrategy.Create(workspaceID, new ObjectType());
			}

			return entity;
		}
	}
}
