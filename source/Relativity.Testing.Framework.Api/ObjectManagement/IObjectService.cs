using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents the service that interacts with object manager API.
	/// </summary>
	public interface IObjectService
	{
		/// <summary>
		/// Creates the specified workspace entity.
		/// </summary>
		/// <typeparam name="TObject">The type of the entity.</typeparam>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>Returns the <typeparamref name="TObject"/> entity.</returns>
		TObject Create<TObject>(int workspaceId, TObject entity)
			where TObject : Artifact;

		/// <summary>
		/// Creates the specified workspace entities.
		/// </summary>
		/// <typeparam name="TObject">The type of the entity.</typeparam>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entities">The array of entities to create.</param>
		/// <returns>Returns a list of ArtifactIDs for the created entities.</returns>
		List<int> Create<TObject>(int workspaceId, IEnumerable<TObject> entities)
			where TObject : Artifact;

		/// <summary>
		/// Creates the specified workspace entity.
		/// </summary>
		/// <typeparam name="TObject">The type of the entity.</typeparam>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>Returns the task to to create the <typeparamref name="TObject"/> entity.</returns>
		Task<TObject> CreateAsync<TObject>(int workspaceId, TObject entity)
			where TObject : Artifact;

		/// <summary>
		/// Creates the specified workspace entities.
		/// </summary>
		/// <typeparam name="TObject">The type of the entity.</typeparam>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entities">The array of entities to create.</param>
		/// <returns>Returns the task to create the <typeparamref name="TObject"/> entities.</returns>
		Task<List<int>> CreateAsync<TObject>(int workspaceId, IEnumerable<TObject> entities)
			where TObject : Artifact;

		/// <summary>
		/// Updates the specified fields on a list of Documents or Relativity Dynamic Objects (RDOs) that match a set of identifiers by setting the fields to the same value.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="massRequestByObjectIdentifiers">A request to update multiple Documents or Relativity Dynamic Objects (RDOs) that have the specified identifiers.</param>
		/// <remarks>
		///     <para>A <see cref="MassUpdateByObjectIdentifiersRequest"/> object specifies the identifiers used to select a list of objects with the same type for updating.</para>
		///     <para>It also includes the same values for all object fields that are to be updated.</para>
		///     <para>In the Relativity UI, this update operation is equivalent to the user selecting the Checked or These option in the mass operations bar on a list page.</para>
		///     <para>Process halts at first failure with no rollback.</para>
		/// </remarks>
		void Update(int workspaceId, MassUpdateByObjectIdentifiersRequest massRequestByObjectIdentifiers);

		/// <summary>
		/// Updates the specified workspace entity.
		/// </summary>
		/// <typeparam name="TObject">The type of the entity.</typeparam>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entity">The entity to update.</param>
		void Update<TObject>(int workspaceId, TObject entity)
			where TObject : Artifact;

		/// <summary>
		/// Updates the specified fields on a list of Documents or Relativity Dynamic Objects (RDOs) that match a set of identifiers by setting the fields to the same value.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="massRequestByObjectIdentifiers">A request to update multiple Documents or Relativity Dynamic Objects (RDOs) that have the specified identifiers.</param>
		/// <remarks>
		///     <para>A <see cref="MassUpdateByObjectIdentifiersRequest"/> object specifies the identifiers used to select a list of objects with the same type for updating.</para>
		///     <para>It also includes the same values for all object fields that are to be updated.</para>
		///     <para>In the Relativity UI, this update operation is equivalent to the user selecting the Checked or These option in the mass operations bar on a list page.</para>
		///     <para>Process halts at first failure with no rollback.</para>
		/// </remarks>
		/// <returns>Returns the task to update the entities.</returns>
		Task UpdateAsync(int workspaceId, MassUpdateByObjectIdentifiersRequest massRequestByObjectIdentifiers);

		/// <summary>
		/// Updates the specified workspace entity.
		/// </summary>
		/// <typeparam name="TObject">The type of the entity.</typeparam>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entity">The entity to update.</param>
		/// <returns>Returns the task to update the entity.</returns>
		Task UpdateAsync<TObject>(int workspaceId, TObject entity)
			where TObject : Artifact;

		/// <summary>
		/// Deletes the workspace entity specified by the ArtifactIDs of the workspace and entity.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entityId">The ArtifactID of the entity to delete.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Deletes the workspace entities specified by the ArtifactIDs of the workspace and entities.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entityIds">The collection of ArtifactIDs of the entities to delete.</param>
		void Delete(int workspaceId, IEnumerable<int> entityIds);

		/// <summary>
		/// Deletes the workspace entity specified by the ArtifactIDs of the workspace and entity.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entityId">The ArtifactID of the entity to delete.</param>
		/// <returns>The task to delete the entity.</returns>
		Task DeleteAsync(int workspaceId, int entityId);

		/// <summary>
		/// Deletes the workspace entities specified by the ArtifactIDs of the workspace and entities.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace.</param>
		/// <param name="entityIds">The collection of ArtifactIDs of the entities to delete.</param>
		/// <returns>The task to delete the entities.</returns>
		Task DeleteAsync(int workspaceId, IEnumerable<int> entityIds);

		/// <summary>
		/// Gets all entities of the <typeparamref name="TObject"/> type.
		/// </summary>
		/// <typeparam name="TObject">The type of the entities.</typeparam>
		/// <returns>The collection of <typeparamref name="TObject"/> entities.</returns>
		TObject[] GetAll<TObject>();

		/// <summary>
		/// Gets all entities of the <typeparamref name="TObject"/> type matching the specified property filter.
		/// </summary>
		/// <typeparam name="TObject">The type of the entities.</typeparam>
		/// <param name="wherePropertySelector">The filter property selector.</param>
		/// <param name="whereValue">The filter property value.</param>
		/// <returns>The collection of <typeparamref name="TObject"/> entities that match the filter.</returns>
		TObject[] GetAll<TObject>(Expression<Func<TObject, object>> wherePropertySelector, object whereValue);

		/// <summary>
		/// Gets all entities of the <typeparamref name="TObject"/> type.
		/// </summary>
		/// <typeparam name="TObject">The type of the entities.</typeparam>
		/// <returns>The task to get all entities of the <typeparamref name="TObject"/> type.</returns>
		Task<TObject[]> GetAllAsync<TObject>();

		/// <summary>
		/// Gets all entities of the <typeparamref name="TObject"/> type matching the specified property filter.
		/// </summary>
		/// <typeparam name="TObject">The type of the entities.</typeparam>
		/// <param name="wherePropertySelector">The filter property selector.</param>
		/// <param name="whereValue">The filter property value.</param>
		/// <returns>The task to get the collection of <typeparamref name="TObject"/> entities that match the filter.</returns>
		Task<TObject[]> GetAllAsync<TObject>(Expression<Func<TObject, object>> wherePropertySelector, object whereValue);

		/// <summary>
		/// Gets a query to enumerate <typeparamref name="TObject"/> objects.
		/// </summary>
		/// <typeparam name="TObject">The type of the entity.</typeparam>
		/// <returns>The <see cref="ObjectQuery{TObject}"/> operator.</returns>
		ObjectQuery<TObject> Query<TObject>();

		/// <summary>
		/// Gets an asynchronous query to enumerate <typeparamref name="TObject"/> objects.
		/// </summary>
		/// <typeparam name="TObject">The type of the entity.</typeparam>
		/// <returns>The query <see cref="ObjectQueryAsync{TObject}"/> operator.</returns>
		ObjectQueryAsync<TObject> AsyncQuery<TObject>();
	}
}
