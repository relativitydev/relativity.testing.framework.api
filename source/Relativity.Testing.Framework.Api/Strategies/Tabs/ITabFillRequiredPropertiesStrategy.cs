using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface ITabFillRequiredPropertiesStrategy
	{
		/// <summary>
		/// Fills the required properties on a tab request that can't be randomly generated.
		/// </summary>
		/// <param name="workspaceID">The ID of the workspace.</param>
		/// <param name="entity">The <see cref="Tab"/>object to fill the required properties for.</param>
		/// <returns>The entities.</returns>
		Tab FillRequiredProperties(int workspaceID, Tab entity);
	}
}
