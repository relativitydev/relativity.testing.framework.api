using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ResourcePoolService : IResourcePoolService
	{
		private readonly ICreateStrategy<ResourcePool> _createStrategy;
		private readonly IGetAllStrategy<ResourcePool> _getAllStrategy;
		private readonly IGetByIdStrategy<ResourcePool> _getByIdStrategy;
		private readonly IGetByNameStrategy<ResourcePool> _getByNameStrategy;
		private readonly IUpdateStrategy<ResourcePool> _updateStrategy;
		private readonly IDeleteByIdStrategy<ResourcePool> _deleteByIdStrategy;
		private readonly IQueryResourcesStrategy _queryResourcesStrategy;
		private readonly IQueryEligibleToAddResourcesStrategy _queryEligibleToAddResourcesStrategy;
		private readonly IQueryEligibleToAddClientsStrategy _queryEligibleToAddClientsStrategy;
		private readonly IResourcePoolAddResourceStrategy _resourcePoolAddResourceStrategy;
		private readonly IResourcePoolRemoveResourceStrategy _resourcePoolRemoveResourceStrategy;

		public ResourcePoolService(
			ICreateStrategy<ResourcePool> createStrategy,
			IGetAllStrategy<ResourcePool> getAllStrategy,
			IGetByIdStrategy<ResourcePool> getByIdStrategy,
			IGetByNameStrategy<ResourcePool> getByNameStrategy,
			IUpdateStrategy<ResourcePool> updateStrategy,
			IDeleteByIdStrategy<ResourcePool> deleteByIdStrategy,
			IQueryResourcesStrategy queryResourcesStrategy,
			IQueryEligibleToAddResourcesStrategy queryEligibleToAddResourcesStrategy,
			IQueryEligibleToAddClientsStrategy queryEligibleToAddClientsStrategy,
			IResourcePoolAddResourceStrategy resourcePoolAddResourceStrategy,
			IResourcePoolRemoveResourceStrategy resourcePoolRemoveResourceStrategy)
		{
			_createStrategy = createStrategy;
			_getAllStrategy = getAllStrategy;
			_getByIdStrategy = getByIdStrategy;
			_getByNameStrategy = getByNameStrategy;
			_updateStrategy = updateStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_queryResourcesStrategy = queryResourcesStrategy;
			_queryEligibleToAddResourcesStrategy = queryEligibleToAddResourcesStrategy;
			_queryEligibleToAddClientsStrategy = queryEligibleToAddClientsStrategy;
			_resourcePoolAddResourceStrategy = resourcePoolAddResourceStrategy;
			_resourcePoolRemoveResourceStrategy = resourcePoolRemoveResourceStrategy;
		}

		public ResourcePool Create(ResourcePool entity)
			=> _createStrategy.Create(entity);

		public ResourcePool[] GetAll()
			=> _getAllStrategy.GetAll();

		public ResourcePool Get(int id)
			=> _getByIdStrategy.Get(id);

		public ResourcePool Get(string name)
			=> _getByNameStrategy.Get(name);

		public ResourcePool Update(ResourcePool entity)
			=> _updateStrategy.Update(entity);

		public void Delete(int entityId)
			=> _deleteByIdStrategy.Delete(entityId);

		public ResourcePoolQuery<ResourceServer> QueryResources(int resourcePoolId, ResourceType resourceType = ResourceType.AgentWorkerServers)
			=> _queryResourcesStrategy.Query(resourcePoolId, resourceType);

		public ResourcePoolQuery<ResourceServer> QueryEligibleToAddResources(int resourcePoolId, ResourceType resourceType = ResourceType.AgentWorkerServers)
			=> _queryEligibleToAddResourcesStrategy.Query(resourcePoolId, resourceType);

		public ResourcePoolQuery<Client> Query()
			=> _queryEligibleToAddClientsStrategy.Query();

		public void AddResources(int resourcePoolId, ResourceType resourceType, List<Artifact> resources)
			=> _resourcePoolAddResourceStrategy.Add(resourcePoolId, resourceType, resources);

		public void RemoveResources(int resourcePoolId, ResourceType resourceType, List<Artifact> resources)
			=> _resourcePoolRemoveResourceStrategy.Remove(resourcePoolId, resourceType, resources);
	}
}
