using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the tab API service.
	/// </summary>
	public interface ITabService
	{
		/// <summary>
		/// Creates the specified tab.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Tab Create(int workspaceId, Tab entity);

		/// <summary>
		/// Requires the specified tab.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> have a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		Tab Require(int workspaceId, Tab entity);

		/// <summary>
		/// Deletes the tab by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the tab.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the tab by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the tab.</param>
		/// <returns>>The <see cref="Tab"/> entity.</returns>
		Tab Get(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified tab.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the tab,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		void Update(int workspaceId, Tab entity);
	}
}
