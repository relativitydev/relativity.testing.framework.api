using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of removing resource of a given type from a given resource pool.
	/// </summary>
	internal interface IResourcePoolRemoveResourceStrategy
	{
		/// <summary>
		/// Removes a resource of a given type from a given resource pool.
		/// </summary>
		/// <param name="resourcePoolId">The Artifact ID of the resource pool.</param>
		/// <param name="resourceType">A representation of the resource type.</param>
		/// <param name="resources">A list of identifiers for resources to remove from the resource pool.</param>
		void Remove(int resourcePoolId, ResourceType resourceType, List<Artifact> resources);
	}
}
