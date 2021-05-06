using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the workspace API service.
	/// </summary>
	public interface IWorkspaceService
	{
		/// <summary>
		/// Creates the specified workspace.
		/// If properties are not specified in the entity, the first value found for each property will be used.
		/// It is the responsibility of the user writing/running the tests to ensure that they are adhering to all environment related best practices and rules.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Workspace Create(Workspace entity);

		/// <summary>
		/// Creates the specified workspace with generated documents.
		/// If properties are not specified in the entity, the first value found for each property will be used.
		/// It is the responsibility of the user writing/running the tests to ensure that they are adhering to all environment related best practices and rules.
		/// </summary>
		/// <param name="entity">The entity to create.</param>
		/// <param name="numberOfDocuments">Number of documents to create.</param>
		/// <returns>The created entity.</returns>
		Workspace CreateWithDocs(Workspace entity, int numberOfDocuments = 10);

		/// <summary>
		/// Delete the workspace by the specified artifact ID.
		/// </summary>
		/// <param name="id">The case artifact ID of workspace.</param>
		void Delete(int id);

		/// <summary>
		/// Gets the workspace by the specified case artifact ID.
		/// </summary>
		/// <param name="id">The case artifact ID of workspace.</param>
		/// <returns>The <see cref="Workspace"/> entity or <see langword="null"/>.</returns>
		Workspace Get(int id);

		/// <summary>
		/// Gets the workspace by the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The <see cref="Workspace"/> entity or <see langword="null"/>.</returns>
		Workspace Get(string name);

		/// <summary>
		/// Gets all workspaces.
		/// </summary>
		/// <returns>The collection of <see cref="Workspace"/> entities.</returns>
		Workspace[] GetAll();

		/// <summary>
		/// Gets all workspaces by the specified client name.
		/// </summary>
		/// <param name="clientName">The client name.</param>
		/// <returns>The collection of <see cref="Workspace"/> entities.</returns>
		IEnumerable<Workspace> GetAllByClientName(string clientName);

		/// <summary>
		/// Determines whether the workspace with the specified case artifact ID exists.
		/// </summary>
		/// <param name="id">The case artifact ID.</param>
		/// <returns><see langword="true"/> if a workspace exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(int id);
	}
}
