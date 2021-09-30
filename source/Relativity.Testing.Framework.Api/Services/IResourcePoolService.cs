using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the resource pool API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IResourcePoolService _resourcePoolService = relativityFacade.Resolve&lt;IResourcePoolService&gt;();
	/// </code>
	/// </example>
	public interface IResourcePoolService
	{
		/// <summary>
		/// Creates the specified <see cref="ResourcePool"/>.
		/// </summary>
		/// <param name="entity">The <see cref="ResourcePool"/> entity to create.</param>
		/// <returns>The <see cref="ResourcePool"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Client client = relativityFacade.Resolve&lt;IClientService&gt;().Get("Some Existing Client Name");
		/// var resourcePoolToCreate = new ResourcePool
		/// {
		/// 	Name = "Test Resource Pool",
		/// 	Client = client
		/// };
		/// ResourcePool createdResourcePool = _resourcePoolService.Create(resourcePoolToCreate);
		/// </code>
		/// </example>
		ResourcePool Create(ResourcePool entity);

		/// <summary>
		/// Gets all <see cref="ResourcePool"/>s.
		/// </summary>
		/// <returns>The collection of <see cref="ResourcePool"/> entities.</returns>
		/// <example>
		/// <code>
		/// ResourcePool[] resourcePools = _resourcePoolService.GetAll();
		/// </code>
		/// </example>
		ResourcePool[] GetAll();

		/// <summary>
		/// Gets the <see cref="ResourcePool"/> by the specified ArtifactID.
		/// </summary>
		/// <param name="id">The ArtifactID of the <see cref="ResourcePool"/>.</param>
		/// <returns>The <see cref="ResourcePool"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int existingResourcePoolArtifactID = 10154345;
		/// ResourcePool resourcePool = _resourcePoolService.Get(existingResourcePoolArtifactID);
		/// </code>
		/// </example>
		ResourcePool Get(int id);

		/// <summary>
		/// Gets the <see cref="ResourcePool"/> by the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The <see cref="ResourcePool"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// string existingResourcePoolName = "Test Resource Pool";
		/// ResourcePool resourcePool = _resourcePoolService.Get(existingResourcePoolName);
		/// </code>
		/// </example>
		ResourcePool Get(string name);

		/// <summary>
		/// Updates the specified <see cref="ResourcePool"/>.
		/// </summary>
		/// <param name="entity">The <see cref="ResourcePool"/> entity to update.</param>
		/// <example>
		/// <code>
		/// ResourcePool resourcePoolToUpdate = _resourcePoolService.Get("Some Existing Resource Pool");
		/// resourcePoolToUpdate.Name = "Updated Resource Pool Name";
		/// resourcePoolToUpdate.Notes = "Some Note";
		/// _resourcePoolService.Update(resourcePoolToUpdate);
		/// </code>
		/// </example>
		void Update(ResourcePool entity);

		/// <summary>
		/// Deletes the specified <see cref="ResourcePool"/> by the ArtifactID.
		/// </summary>
		/// <param name="entityId">The ArtifactID of the <see cref="ResourcePool"/>.</param>
		/// <example>
		/// <code>
		/// int existingResourcePoolArtifactID = 10154345;
		/// _resourcePoolService.Delete(existingResourcePoolArtifactID);
		/// </code>
		/// </example>
		void Delete(int entityId);

		/// <summary>
		/// Gets a query to enumerate resources of a given type that are associated with the <see cref="ResourcePool"/>.
		/// </summary>
		/// <param name="resourcePoolId">The ArtifactID of the <see cref="ResourcePool"/>.</param>
		/// <param name="resourceType">A representation of the resource type.
		/// By default equals 'agent-worker-servers'.</param>
		/// <returns>The <see cref="ResourcePoolQuery{ResourceServer}"/> object.</returns>
		/// <example> This example shows how to query AgentWorkerServers resources in order to one with the specified ArtifactID.
		/// <code>
		/// int existingResourcePoolArtifactID = 10154345;
		/// int existingAgentArtifactID = 1017895;
		/// ResourceServer agent = _resourcePoolService.QueryResources(existingResourcePoolArtifactID)
		/// 	.Where(agent => agent.ArtifactID, existingAgentArtifactID)
		/// 	.FirstOrDefault();
		/// </code>
		/// </example>
		/// <example> This example shows how to query for all SqlServers resources for given Resource Pool.
		/// <code>
		/// int existingResourcePoolArtifactID = 10154345;
		/// ResourcePoolQuery&lt;ResourceServer&gt; sqlServers = _resourcePoolService.QueryResources(existingResourcePoolArtifactID, ResourceType.SqlServers);
		/// </code>
		/// </example>
		ResourcePoolQuery<ResourceServer> QueryResources(int resourcePoolId, ResourceType resourceType = ResourceType.AgentWorkerServers);

		/// <summary>
		/// Gets a query to enumerate not yet associated resources of a given type that are eligible to be added to a given <see cref="ResourcePool"/>.
		/// </summary>
		/// <param name="resourcePoolId">The ArtifactID of the <see cref="ResourcePool"/>.</param>
		/// <param name="resourceType">A representation of the resource type.
		/// By default equals 'agent-worker-servers'.</param>
		/// <returns>The <see cref="ResourcePoolQuery{ResourceServer}"/> object.</returns>
		/// <example> This example shows how to query for all eligible AnalyticsServers resources for given Resource Pool.
		/// <code>
		/// int existingResourcePoolArtifactID = 10154345;
		/// ResourcePoolQuery&lt;ResourceServer&gt; eligibleAnalyticsServers = _resourcePoolService.QueryEligibleToAddResources(existingResourcePoolArtifactID, ResourceType.AnalyticsServers);
		/// </code>
		/// </example>
		ResourcePoolQuery<ResourceServer> QueryEligibleToAddResources(int resourcePoolId, ResourceType resourceType = ResourceType.AgentWorkerServers);

		/// <summary>
		/// Gets a query for all clients that may be set as the client for a <see cref="ResourcePool"/>.
		/// </summary>
		/// <returns>The <see cref="ResourcePoolQuery{Client}"/> object.</returns>
		/// <example>
		/// <code>
		/// List&lt;Client&gt; eligibleClients = _resourcePoolService.Query().ToList();
		/// </code>
		/// </example>
		ResourcePoolQuery<Client> Query();

		/// <summary>
		/// Adds a resources of a given type to a given <see cref="ResourcePool"/>.
		/// </summary>
		/// <param name="resourcePoolId">The ArtifactID of the <see cref="ResourcePool"/>.</param>
		/// <param name="resourceType">A representation of the resource type.</param>
		/// <param name="resources">A list of identifiers for resources to add to the <see cref="ResourcePool"/>.</param>
		/// <example> This example shows how to add all eligible Agent Worker Servers to the Resource Pool.
		/// <code>
		///  int existingResourcePoolArtifactID = 10154345;
		/// List&lt;Artifact&gt; agentsToAdd = _resourcePoolService.QueryEligibleToAddResources(existingResourcePoolArtifactID).Cast&lt;Artifact&gt;().ToList();
		/// _resourcePoolService.AddResources(existingResourcePoolArtifactID, ResourceType.AgentWorkerServers, agentsToAdd);
		/// </code>
		/// </example>
		void AddResources(int resourcePoolId, ResourceType resourceType, List<Artifact> resources);

		/// <summary>
		/// Removes a resources of a given type from a given <see cref="ResourcePool"/>.
		/// </summary>
		/// <param name="resourcePoolId">The ArtifactID of the <see cref="ResourcePool"/>.</param>
		/// <param name="resourceType">A representation of the resource type.</param>
		/// <param name="resources">A list of identifiers for resources to remove from the <see cref="ResourcePool"/>.</param>
		/// <example> This example shows how to remove all Agent Worker Servers that have 'Test' in their Name from the Resource Pool.
		/// <code>
		///  int existingResourcePoolArtifactID = 10154345;
		/// List&lt;Artifact&gt; agentsToRemove = _resourcePoolService.QueryResources(existingResourcePoolArtifactID)
		/// 	.Where(agent => agent.Name.Contains("Test"))
		/// 	.Cast&lt;Artifact&gt;().ToList();
		/// _resourcePoolService.RemoveResources(existingResourcePoolArtifactID, ResourceType.AgentWorkerServers, agentsToRemove);
		/// </code>
		/// </example>
		void RemoveResources(int resourcePoolId, ResourceType resourceType, List<Artifact> resources);
	}
}
