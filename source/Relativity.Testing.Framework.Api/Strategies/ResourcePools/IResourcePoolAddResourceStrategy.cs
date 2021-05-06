using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of adding resource of a given type to a given resource pool.
	/// </summary>
	internal interface IResourcePoolAddResourceStrategy
	{
		/// <summary>
		/// Adds a resource of a given type to a given resource pool.
		/// </summary>
		/// <param name="resourcePoolId">The Artifact ID of the resource pool.</param>
		/// <param name="resourceType">A representation of the resource type.</param>
		/// <param name="resources">A list of identifiers for resources to add to the resource pool.</param>
		void Add(int resourcePoolId, ResourceType resourceType, List<Artifact> resources);
	}
}
