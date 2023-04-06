using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IWorkspaceFillRequiredPropertiesStrategy
	{
		/// <summary>
		/// Fills the required properties on a workspace request that can't be randomly generated and resolves any incomplete properties.
		/// </summary>
		/// <param name="entity">The <see cref="Workspace"/> object to fill the required properties for.</param>
		/// <returns>The workspace object.</returns>
		Workspace FillRequiredProperties(Workspace entity);
	}
}
