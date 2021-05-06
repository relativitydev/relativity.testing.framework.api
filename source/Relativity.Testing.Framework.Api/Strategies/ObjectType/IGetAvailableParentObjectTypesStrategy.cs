using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting a list of all object types available to be a parent object type for a given workspace.
	/// </summary>
	public interface IGetAvailableParentObjectTypesStrategy
	{
		/// <summary>
		/// Gets a list of all object types available to be a parent object type for a given workspace.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace to view all the available parent object types. Use -1 to indicate the admin workspace.</param>
		/// <returns>All oject types available to be parents in a workspace.</returns>
		List<ObjectType> GetAvailableParentObjectTypes(int workspaceId);
	}
}
