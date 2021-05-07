using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the matter API service.
	/// </summary>
	public interface IMatterService
	{
		/// <summary>
		/// Creates the specified matter.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Matter Create(Matter entity);

		/// <summary>
		/// Requires the specified matter.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> have a value, gets entity by name and client ID and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		Matter Require(Matter entity);

		/// <summary>
		/// Deletes the matter by ID.
		/// </summary>
		/// <param name="id">The artifact ID of the matter.</param>
		void Delete(int id);

		/// <summary>
		/// Gets the matter by the specified ID.
		/// </summary>
		/// <param name="id">The artifact ID of the matter.</param>
		/// <returns>The <see cref="Matter"/> entity or <see langword="null"/>.</returns>
		Matter Get(int id);

		/// <summary>
		/// Gets the matter by the specified name and client ID.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="clientId">The client ID.</param>
		/// <returns>The <see cref="Matter"/> entity or <see langword="null"/>.</returns>
		Matter Get(string name, int clientId);

		/// <summary>
		/// Updates the specified matter.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		void Update(Matter entity);
	}
}
