using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting list eligible to be a layout owner.
	/// </summary>
	internal interface ILayoutGetEligibleOwnersStrategy
	{
		/// <summary>
		/// Gets the list eligible to be a layout owner by the specified workspace ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The list eligible to be a layout owner.</returns>
		List<NamedArtifact> GetEligibleOwners(int workspaceId);
	}
}
