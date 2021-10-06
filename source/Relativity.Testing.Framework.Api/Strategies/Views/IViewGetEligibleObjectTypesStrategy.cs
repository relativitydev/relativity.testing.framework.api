using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting a list of object types in a specific workspace.
	/// You can select an object type from this list that is used for populating the ObjectType field for the View object.
	/// </summary>
	internal interface IViewGetEligibleObjectTypesStrategy
	{
		/// <summary>
		/// Gets the list of of object types in a specific workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The list of [NamedArtifact](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html) objects.</returns>
		List<NamedArtifact> GetEligibleObjectTypes(int workspaceId);
	}
}
