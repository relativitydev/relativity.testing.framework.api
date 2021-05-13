using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the layout API service.
	/// </summary>
	public interface ILayoutService
	{
		/// <summary>
		/// Creates the specified Layout.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Layout Create(int workspaceId, Layout entity);

		/// <summary>
		/// Requires the specified layout.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> have a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		Layout Require(int workspaceId, Layout entity);

		/// <summary>
		/// Deletes the layout by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the layout.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the layout by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the layout.</param>
		/// <returns>>The <see cref="Layout"/> entity.</returns>
		Layout Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the list eligible to be a layout owner by the specified workspace ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The list eligible to be a layout owner.</returns>
		List<NamedArtifact> GetEligibleOwners(int workspaceId);

		/// <summary>
		/// Updates the specified layout.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		void Update(int workspaceId, Layout entity);

		/// <summary>
		/// Builds the layout.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The layout to build. If ArtifactId is provided, it will be used, otherwise it will be looked up by name. If neither are provided, this will put us in an exceptional state.</param>
		/// <param name="categoryFields">The fields to add to a category.</param>
		void AddFields(int workspaceId, Layout entity, List<CategoryField> categoryFields);

		/// <summary>
		/// Gets the categories in a layout.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The layout to get the categories for. If ArtifactId is provided, it will be used, otherwise it will be looked up by name. If neither are provided, this will put us in an exceptional state.</param>
		void GetCategories(int workspaceId, Layout entity);
	}
}
