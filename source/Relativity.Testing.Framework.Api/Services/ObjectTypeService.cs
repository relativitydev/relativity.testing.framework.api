using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	public class ObjectTypeService : IObjectTypeService
	{
		private readonly ICreateWorkspaceEntityStrategy<ObjectType> _createStrategy;
		private readonly IRequireWorkspaceEntityStrategy<ObjectType> _requireWorkspaceEntityStrategy;
		private readonly IDeleteWorkspaceEntityByIdStrategy<ObjectType> _deleteWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByIdStrategy<ObjectType> _getWorkspaceEntityByIdStrategy;
		private readonly IGetWorkspaceEntityByNameStrategy<ObjectType> _getWorkspaceEntityByNameStrategy;
		private readonly IUpdateWorkspaceEntityStrategy<ObjectType> _updateWorkspaceEntityStrategy;
		private readonly IGetDependencyListForWorkspaceEntityStrategy<ObjectType> _getDependencyListForWorkspaceEntityStrategy;
		private readonly IGetAvailableParentObjectTypesStrategy _getAvailableParentObjectTypesStrategy;

		public ObjectTypeService(
			ICreateWorkspaceEntityStrategy<ObjectType> createStrategy,
			IRequireWorkspaceEntityStrategy<ObjectType> requireWorkspaceEntityStrategy,
			IDeleteWorkspaceEntityByIdStrategy<ObjectType> deleteWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByIdStrategy<ObjectType> getWorkspaceEntityByIdStrategy,
			IGetWorkspaceEntityByNameStrategy<ObjectType> getWorkspaceEntityByNameStrategy,
			IUpdateWorkspaceEntityStrategy<ObjectType> updateWorkspaceEntityStrategy,
			IGetDependencyListForWorkspaceEntityStrategy<ObjectType> getDependencyListForWorkspaceEntityStrategy,
			IGetAvailableParentObjectTypesStrategy getAvailableParentObjectTypesStrategy)
		{
			_createStrategy = createStrategy;
			_requireWorkspaceEntityStrategy = requireWorkspaceEntityStrategy;
			_deleteWorkspaceEntityByIdStrategy = deleteWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByIdStrategy = getWorkspaceEntityByIdStrategy;
			_getWorkspaceEntityByNameStrategy = getWorkspaceEntityByNameStrategy;
			_updateWorkspaceEntityStrategy = updateWorkspaceEntityStrategy;
			_getDependencyListForWorkspaceEntityStrategy = getDependencyListForWorkspaceEntityStrategy;
			_getAvailableParentObjectTypesStrategy = getAvailableParentObjectTypesStrategy;
		}

		public ObjectType Create(int workspaceId, ObjectType entity)
			=> _createStrategy.Create(workspaceId, entity);

		public ObjectType Require(int workspaceId, ObjectType entity)
			=> _requireWorkspaceEntityStrategy.Require(workspaceId, entity);

		public void Delete(int workspaceId, int entityId)
			=> _deleteWorkspaceEntityByIdStrategy.Delete(workspaceId, entityId);

		public ObjectType Get(int workspaceId, int entityId)
			=> _getWorkspaceEntityByIdStrategy.Get(workspaceId, entityId);

		public ObjectType Get(int workspaceId, string entityName)
			=> _getWorkspaceEntityByNameStrategy.Get(workspaceId, entityName);

		public void Update(int workspaceId, ObjectType entity)
			=> _updateWorkspaceEntityStrategy.Update(workspaceId, entity);

		public List<Dependency> GetDependencies(int workspaceId, int entityId)
			=> _getDependencyListForWorkspaceEntityStrategy.GetDependencies(workspaceId, entityId);

		public List<ObjectType> GetAvailableParentObjectTypes(int workspaceId)
			=> _getAvailableParentObjectTypesStrategy.GetAvailableParentObjectTypes(workspaceId);
	}
}
