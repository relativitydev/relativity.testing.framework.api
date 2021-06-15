using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class LayoutService : ILayoutService
	{
		private readonly ICreateWorkspaceEntityStrategy<Layout> _createWorkspaceEntityStrategy;
		private readonly IRequireWorkspaceEntityStrategy<Layout> _requireWorkspaceEntityStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<Layout> _deleteWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Layout> _getWorkspaceEntityByIdStrategy;
		private readonly ILayoutGetEligibleOwnersStrategy _layoutGetEligibleOwnersStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Layout> _updateWorkspaceEntityStrategy;
		private readonly ILayoutAddFieldsStrategy _layoutAddFieldsStrategy;
		private readonly ILayoutGetCategoriesStrategy _layoutGetCategoriesStrategy;

		public LayoutService(
			ICreateWorkspaceEntityStrategy<Layout> createWorkspaceEntityStrategy,
			IRequireWorkspaceEntityStrategy<Layout> requireWorkspaceEntityStrategy,
			IDeleteWorkspaceEntityByIdStrategy<Layout> deleteWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByIdStrategy<Layout> getWorkspaceEntityByIdStrategy,
			ILayoutGetEligibleOwnersStrategy layoutGetEligibleOwnersStrategy,
			IUpdateWorkspaceEntityStrategy<Layout> updateWorkspaceEntityStrategy,
			ILayoutAddFieldsStrategy layoutAddFieldsStrategy,
			ILayoutGetCategoriesStrategy layoutGetCategoriesStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_requireWorkspaceEntityStrategy = requireWorkspaceEntityStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_layoutGetEligibleOwnersStrategy = layoutGetEligibleOwnersStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
			_layoutAddFieldsStrategy = layoutAddFieldsStrategy;
			_layoutGetCategoriesStrategy = layoutGetCategoriesStrategy;
		}

		public Layout Create(int workspaceId, Layout entity)
			=> _createWorkspaceEntityStrategy.Create(workspaceId, entity);

		public Layout Require(int workspaceId, Layout entity)
			=> _requireWorkspaceEntityStrategy.Require(workspaceId, entity);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public Layout Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public List<NamedArtifact> GetEligibleOwners(int workspaceId)
			=> _layoutGetEligibleOwnersStrategy.GetEligibleOwners(workspaceId);

		public void Update(int workspaceId, Layout entity)
			=> _updateWorkspaceEntityStrategy.Update(workspaceId, entity);

		public void AddFields(int workspaceId, Layout entity, List<CategoryField> categoryFields)
			=> _layoutAddFieldsStrategy.AddFields(workspaceId, entity, categoryFields);

		public void GetCategories(int workspaceId, Layout entity)
			=> _layoutGetCategoriesStrategy.GetCategories(workspaceId, entity);
	}
}
