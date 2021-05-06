using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the client API service.
	/// </summary>
	public interface IClientService
	{
		/// <summary>
		/// Creates the specified client.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Client Create(Client entity);

		/// <summary>
		/// Requires the specified client.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and returns it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> has a value, gets entity by name and returns it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		Client Require(Client entity);

		/// <summary>
		/// Deletes the client by ID.
		/// </summary>
		/// <param name="id">The artifact ID of the client.</param>
		void Delete(int id);

		/// <summary>
		/// Gets the client by the specified ID.
		/// </summary>
		/// <param name="id">The artifact ID of the client.</param>
		/// <returns>The <see cref="Client"/> entity or <see langword="null"/>.</returns>
		Client Get(int id);

		/// <summary>
		/// Gets the client by the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The <see cref="Client"/> entity or <see langword="null"/>.</returns>
		Client Get(string name);
	}
}
