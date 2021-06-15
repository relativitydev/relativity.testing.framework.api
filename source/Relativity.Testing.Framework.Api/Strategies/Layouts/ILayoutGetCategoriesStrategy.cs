using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting categories for building a layout.
	/// </summary>
	internal interface ILayoutGetCategoriesStrategy
	{
		/// <summary>
		/// Gets the categories in a layout.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The layout to get categories for. If ArtifactId is provided, it will be used, otherwise it will be looked up by name. If neither are provided, this will put us in an exceptional state.</param>
		/// <returns>The Categories.</returns>
		List<Category> GetCategories(int workspaceId, Layout entity);
	}
}
