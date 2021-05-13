using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the resource pool API service.
	/// </summary>
	public interface IResourcePoolService
	{
		/// <summary>
		/// Creates the specified resource pool.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The <see cref="ResourcePool"/> entity or <see langword="null"/>.</returns>
		ResourcePool Create(ResourcePool entity);

		/// <summary>
		/// Gets all resource pools.
		/// </summary>
		/// <returns>The collection of <see cref="ResourcePool"/> entities.</returns>
		ResourcePool[] GetAll();

		/// <summary>
		/// Gets the resource pool by the specified ID.
		/// </summary>
		/// <param name="id">The artifact ID of the resource pool.</param>
		/// <returns>The <see cref="ResourcePool"/> entity or <see langword="null"/>.</returns>
		ResourcePool Get(int id);

		/// <summary>
		/// Gets the resource pool by the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The <see cref="ResourcePool"/> entity or <see langword="null"/>.</returns>
		ResourcePool Get(string name);

		/// <summary>
		/// Updates the specified resource pool.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		void Update(ResourcePool entity);

		/// <summary>
		/// Deletes the specified resource pool by the Artifact ID.
		/// </summary>
		/// <param name="entityId">The Artifact ID of the resource pool.</param>
		void Delete(int entityId);

		/// <summary>
		/// Gets a query to enumerate resources of a given type that are associated with the resource pool.
		/// </summary>
		/// <param name="resourcePoolId">The Artifact ID of the resource pool.</param>
		/// <param name="resourceType">A representation of the resource type.
		/// By default equals 'agent-worker-servers'.</param>
		/// <returns>The <see cref="ResourcePoolQuery{ResourceServer}"/> object.</returns>
		ResourcePoolQuery<ResourceServer> QueryResources(int resourcePoolId, ResourceType resourceType = ResourceType.AgentWorkerServers);

		/// <summary>
		/// Gets a query to enumerate not yet associated resources of a given type that are eligible to be added to a given resource pool.
		/// </summary>
		/// <param name="resourcePoolId">The Artifact ID of the resource pool.</param>
		/// <param name="resourceType">A representation of the resource type.
		/// By default equals 'agent-worker-servers'.</param>
		/// <returns>The <see cref="ResourcePoolQuery{ResourceServer}"/> object.</returns>
		ResourcePoolQuery<ResourceServer> QueryEligibleToAddResources(int resourcePoolId, ResourceType resourceType = ResourceType.AgentWorkerServers);

		/// <summary>
		/// Gets a query for all clients that may be set as the client for a resource pool.
		/// </summary>
		/// <returns>The <see cref="ResourcePoolQuery{Client}"/> object.</returns>
		ResourcePoolQuery<Client> Query();

		/// <summary>
		/// Adds a resources of a given type to a given resource pool.
		/// </summary>
		/// <param name="resourcePoolId">The Artifact ID of the resource pool.</param>
		/// <param name="resourceType">A representation of the resource type.</param>
		/// <param name="resources">A list of identifiers for resources to add to the resource pool.</param>
		void AddResources(int resourcePoolId, ResourceType resourceType, List<Artifact> resources);

		/// <summary>
		/// Removes a resources of a given type from a given resource pool.
		/// </summary>
		/// <param name="resourcePoolId">The Artifact ID of the resource pool.</param>
		/// <param name="resourceType">A representation of the resource type.</param>
		/// <param name="resources">A list of identifiers for resources to remove from the resource pool.</param>
		void RemoveResources(int resourcePoolId, ResourceType resourceType, List<Artifact> resources);
	}
}
