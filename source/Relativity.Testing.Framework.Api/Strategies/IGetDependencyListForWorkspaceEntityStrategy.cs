using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the list of dependencies.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetDependencyListForWorkspaceEntityStrategy<T>
	{
		/// <summary>
		/// Gets the list of dependencies.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The entity ID.</param>
		/// <returns>The list of dependencies.</returns>
		List<Dependency> GetDependencies(int workspaceId, int entityId);
	}
}
