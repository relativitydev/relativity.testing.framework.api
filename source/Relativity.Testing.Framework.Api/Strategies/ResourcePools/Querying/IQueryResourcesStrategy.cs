using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IQueryResourcesStrategy
	{
		/// <summary>
		/// Gets a query to enumerate resources.
		/// </summary>
		/// <param name="resourcePoolId">The Artifact ID of the resource pool.</param>
		/// <param name="resourceType">A representation of the resource type.</param>
		/// <returns>The <see cref="ResourcePoolQuery{ResourceServer}"/> object.</returns>
		ResourcePoolQuery<ResourceServer> Query(int resourcePoolId, ResourceType resourceType = ResourceType.AgentWorkerServers);
	}
}
