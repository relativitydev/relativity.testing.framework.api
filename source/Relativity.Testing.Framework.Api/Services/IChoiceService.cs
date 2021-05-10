using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the choice API service.
	/// </summary>
	public interface IChoiceService
	{
		/// <summary>
		/// Creates the specified choice.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new choice,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		Choice Create(int workspaceId, Choice entity);

		/// <summary>
		/// Requires the specified choice.
		/// <list type="number">
		/// <item>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</item>
		/// <item>If <see cref="NamedArtifact.Name"/> and <see cref="Choice.ObjectType"/> and <see cref="Choice.Field"/> name properties of <paramref name="entity"/> have a value, gets entity by name and updates it if it exists.</item>
		/// <item>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</item>
		/// </list>
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require choice,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		Choice Require(int workspaceId, Choice entity);

		/// <summary>
		/// Deletes the choice by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the choice,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the choice.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the choice by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the choice,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the choice.</param>
		/// <returns>>The <see cref="Choice"/> entity or <see langword="null"/>.</returns>
		Choice Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets all choices for particular object field.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="objectTypeName">Name of the object type.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns>The collection of <see cref="Choice"/> entities.</returns>
		IEnumerable<Choice> GetAll(int workspaceId, string objectTypeName, string fieldName);

		/// <summary>
		/// Updates the specified choice.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the object type,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		void Update(int workspaceId, Choice entity);
	}
}
