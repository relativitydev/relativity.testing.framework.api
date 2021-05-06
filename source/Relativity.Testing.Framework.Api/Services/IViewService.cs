using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represent the API view service.
	/// </summary>
	public interface IViewService
	{
		/// <summary>
		///  Creates the specified view.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to create the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The <see cref="View"/> entity or <see langword="null"/>.</returns>
		View Create(int workspaceId, View entity);

		/// <summary>
		/// Gets all views in workspace.
		/// Returns not all fields for faster response.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <returns>The collection of <see cref="View"/> entities.</returns>
		View[] GetAll(int workspaceId);

		/// <summary>
		/// Gets the view by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The Artifact ID of the view.</param>
		/// <returns>The <see cref="View"/> entity or <see langword="null"/>.</returns>
		View Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the view by the specified name.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the view.</param>
		/// <returns>The <see cref="View"/> entity or <see langword="null"/>.</returns>
		View Get(int workspaceId, string entityName);

		/// <summary>
		/// Requires the specified view.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> has a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <returns>The <see cref="View"/> entity or <see langword="null"/>.</returns>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require view.</param>
		/// <param name="entity">The entity to require.</param>
		View Require(int workspaceId, View entity);

		/// <summary>
		/// Determines whether the view with the specified case artifact ID exists.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to check existing of view,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The Artifact ID of the view.</param>
		/// <returns><see langword="true"/> if a view exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified view.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the view.</param>
		/// <param name="entity">The entity to update.</param>
		void Update(int workspaceId, View entity);
	}
}
