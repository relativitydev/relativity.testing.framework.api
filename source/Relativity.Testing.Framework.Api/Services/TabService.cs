using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class TabService : ITabService
	{
		private readonly ICreateWorkspaceEntityStrategy<Tab> _createWorkspaceEntityStrategy;
		private readonly IRequireWorkspaceEntityStrategy<Tab> _requireWorkspaceEntityStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<Tab> _deleteWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<Tab> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<Tab> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<Tab> _updateWorkspaceEntityStrategy;
		private readonly ITabGetAvailableObjectTypesByWorkspaceIDStrategy _tabGetAvailableObjectTypesStrategy;
		private readonly ITabGetAdminLevelMetadataStrategy _tabGetAdminLevelMetadataStrategy;
		private readonly ITabGetEligibleParentsStrategy _tabGetEligibleParentsStrategy;
		private readonly ITabGetOrderStrategy _tabGetOrderStrategy;
		private readonly ITabGetAllForNavigationStrategy _tabGetAllForNavigationStrategy;

		public TabService(
			ICreateWorkspaceEntityStrategy<Tab> createWorkspaceEntityStrategy,
			IRequireWorkspaceEntityStrategy<Tab> requireWorkspaceEntityStrategy,
			IDeleteWorkspaceEntityByIdStrategy<Tab> deleteWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByIdStrategy<Tab> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<Tab> getWorkspaceEntityByName,
			IUpdateWorkspaceEntityStrategy<Tab> updateWorkspaceEntityStrategy,
			ITabGetAvailableObjectTypesByWorkspaceIDStrategy tabGetAvailableObjectTypesStrategy,
			ITabGetAdminLevelMetadataStrategy tabGetAdminLevelMetadataStrategy,
			ITabGetEligibleParentsStrategy tabGetEligibleParentsStrategy,
			ITabGetOrderStrategy tabGetOrderStrategy,
			ITabGetAllForNavigationStrategy tabGetAllForNavigationStrategy)
		{
			_createWorkspaceEntityStrategy = createWorkspaceEntityStrategy;
			_requireWorkspaceEntityStrategy = requireWorkspaceEntityStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByName;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
			_tabGetAvailableObjectTypesStrategy = tabGetAvailableObjectTypesStrategy;
			_tabGetAdminLevelMetadataStrategy = tabGetAdminLevelMetadataStrategy;
			_tabGetEligibleParentsStrategy = tabGetEligibleParentsStrategy;
			_tabGetOrderStrategy = tabGetOrderStrategy;
			_tabGetAllForNavigationStrategy = tabGetAllForNavigationStrategy;
		}

		public Tab Create(int workspaceArtifactID, Tab tab)
			=> _createWorkspaceEntityStrategy.Create(workspaceArtifactID, tab);

		public Tab Require(int workspaceArtifactID, Tab tab)
			=> _requireWorkspaceEntityStrategy.Require(workspaceArtifactID, tab);

		public void Delete(int workspaceArtifactID, int tabArtifactId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceArtifactID, tabArtifactId);

		public Tab Get(int workspaceArtifactID, int tabArtifactId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceArtifactID, tabArtifactId);

		public Tab Get(int workspaceArtifactID, string tabName)
			=> _getWorkspaceEntityByNameStrategy.Get(workspaceArtifactID, tabName);

		public void Update(int workspaceArtifactID, Tab tab)
			=> _updateWorkspaceEntityStrategy.Update(workspaceArtifactID, tab);

		public List<ObjectType> GetAvailableObjectTypes(int workspaceArtifactID)
			=> _tabGetAvailableObjectTypesStrategy.Get(workspaceArtifactID);

		public Meta GetAdminLevelMetadata()
			=> _tabGetAdminLevelMetadataStrategy.Get();

		public List<TabEligibleParent> GetEligibleParents(int workspaceArtifactID)
			=> _tabGetEligibleParentsStrategy.Get(workspaceArtifactID);

		public List<Tab> GetTabsOrder(int workspaceArtifactID)
			=> _tabGetOrderStrategy.Get(workspaceArtifactID);

		public List<Tab> GetAllForNavigation(int workspaceArtifactID)
			=> _tabGetAllForNavigationStrategy.Get(workspaceArtifactID);
	}
}
