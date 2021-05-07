using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the group API service.
	/// </summary>
	public interface IGroupService
	{
		/// <summary>
		/// Creates the specified group.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Group Create(Group entity);

		/// <summary>
		/// Requires the specified group.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> have a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		Group Require(Group entity);

		/// <summary>
		/// Deletes the group by ID.
		/// </summary>
		/// <param name="id">The artifact ID of the group.</param>
		void Delete(int id);

		/// <summary>
		/// Gets the group by the specified ID.
		/// </summary>
		/// <param name="id">The artifact ID of the group.</param>
		/// <returns>The <see cref="Group"/> entity or <see langword="null"/>.</returns>
		Group Get(int id);

		/// <summary>
		/// Gets the group by the specified group name.
		/// </summary>
		/// <param name="name">The name of the group.</param>
		/// <returns>The <see cref="Group"/> entity or <see langword="null"/>.</returns>
		Group Get(string name);

		/// <summary>
		/// Gets all groups by the specified names.
		/// </summary>
		/// <param name="names">The collection of group names.</param>
		/// <returns>The collection of <see cref="Group"/> entities.</returns>
		IEnumerable<Group> GetAll(IEnumerable<string> names);

		/// <summary>
		/// Updates the specified group.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		void Update(Group entity);
	}
}
