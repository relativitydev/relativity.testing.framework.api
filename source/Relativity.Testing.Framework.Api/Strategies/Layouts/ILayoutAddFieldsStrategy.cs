using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of adding fields to a category in a layout.
	/// </summary>
	public interface ILayoutAddFieldsStrategy
	{
		/// <summary>
		/// Adds fields to a category in a layout. The first category found in the layout will be used.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The layout to build. If ArtifactId is provided, it will be used, otherwise it will be looked up by name. If neither are provided, this will put us in an exceptional state.</param>
		/// <param name="categoryFields">A list of fields to add to the category. Use Field.FieldToCategoryField to convert Fields to CategoryFields.</param>
		void AddFields(int workspaceId, Layout entity, List<CategoryField> categoryFields);
	}
}
