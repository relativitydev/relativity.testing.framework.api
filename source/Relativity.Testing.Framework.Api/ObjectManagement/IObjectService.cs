using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entity">The entity to update.</param>
		/// <returns>Returns the <typeparamref name="TObject"/> object.</returns>
		TObject Create<TObject>(int workspaceId, TObject entity)
			where TObject : Artifact;

		/// <summary>
		/// Creates the specified workspace entities.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entities">The array of entities.</param>
		/// <returns>Returns the <typeparamref name="TObject"/> object.</returns>
		List<int> Create<TObject>(int workspaceId, IEnumerable<TObject> entities)
			where TObject : Artifact;

		/// <summary>
		/// Updates specified fields on a list of Documents or Relativity Dynamic Objects (RDOs) that match a set of identifiers by setting the fields to the same value.
		/// </summary>
		/// <param name="workspaceId">Workspace ID of the workspace containing the artifacts to be updated.</param>
		/// <param name="massRequestByObjectIdentifiers">A request to update multiple Documents or Relativity Dynamic Objects (RDOs) that have the specified identifiers.</param>
		/// <remarks>A <see cref="MassUpdateByObjectIdentifiersRequest"/> object specifies the identifiers used to select a list of objects with the same type for updating. It also includes the same
		/// values for all object fields that are to be updated. In the Relativity UI, this update operation is equivalent to the user selecting the Checked or These option in
		/// the mass operations bar on a list page.
		/// Process halts at first failure with no rollback.</remarks>
		void Update(int workspaceId, MassUpdateByObjectIdentifiersRequest massRequestByObjectIdentifiers);

		/// <summary>
		/// Updates the specified workspace entity.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entity">The entity to update.</param>
		void Update<TObject>(int workspaceId, TObject entity)
			where TObject : Artifact;

		/// <summary>
		/// Updates the specified workspace entity fields.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The ArtifactID of the entity to update.</param>
		/// <param name="fieldValues">List of Field Reference and Value for updating entity. </param>
		/// <example> This shows how to update the values of the 'Extracted Text' and 'Group Identifier' fields on the Document.
		/// <code>
		/// int workspaceArtifactID = 1017850;
		/// int existingDocumentToUpdateArtifactID = 1039664;
		/// var fieldsToUpdate = new List&lt;FieldRefValuePair&gt;
		/// {
		/// 	new FieldRefValuePair
		/// 	{
		/// 		Field = new FieldRef
		/// 		{
		/// 			Name = "Group Identifier"
		/// 		},
		/// 		Value = "Updated Group Identifier"
		/// 	},
		/// 	new FieldRefValuePair
		/// 	{
		/// 		Field = new FieldRef
		/// 		{
		/// 			Name = "Extracted Text"
		/// 		},
		/// 		Value = "Updated Extracted Text"
		/// 	}
		/// };
		/// _objectService.Update(workspaceArtifactID, existingDocumentToUpdateArtifactID, fieldsToUpdate);
		/// </code>
		/// </example>
		/// <example> This shows how to update the values of the fields with known ArtifactIDs on the Document.
		/// <code>
		/// int workspaceArtifactID = 1017850;
		/// int existingDocumentToUpdateArtifactID = 1039664;
		/// int existingDocumentFieldArtifactID = 5345435;
		/// int existingDocumentBoolFieldArtifactID = 478324;
		/// var fieldsToUpdate = new List&lt;FieldRefValuePair&gt;
		/// {
		/// 	new FieldRefValuePair
		/// 	{
		/// 		Field = new FieldRef
		/// 		{
		/// 			ArtifactID = existingDocumentStringFieldArtifactID
		/// 		},
		/// 		Value = "Some updated value of text field"
		/// 	},
		/// 	new FieldRefValuePair
		/// 	{
		/// 		Field = new FieldRef
		/// 		{
		/// 			ArtifactID = existingDocumentBoolFieldArtifactID
		/// 		},
		/// 		Value = bool
		/// 	}
		/// };
		/// _objectService.Update(workspaceArtifactID, existingDocumentToUpdateArtifactID, fieldsToUpdate);
		/// </code>
		/// </example>
		void Update(int workspaceId, int entityId, IList<FieldRefValuePair> fieldValues);

		/// <summary>
		/// Deletes the workspace entity by the specified IDs of workspace and entity.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityId">The entity ID.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Deletes the workspace entities by the specified IDs of workspace and entities.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="entityIds">The entity IDs.</param>
		void Delete(int workspaceId, IEnumerable<int> entityIds);

		/// <summary>
		/// Gets all objects of <typeparamref name="TObject"/> type.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <returns>The collection of <typeparamref name="TObject"/> objects.</returns>
		TObject[] GetAll<TObject>();

		/// <summary>
		/// Gets all objects of <typeparamref name="TObject"/> type matching specified property filter.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <param name="wherePropertySelector">The filter property selector.</param>
		/// <param name="whereValue">The filter property value.</param>
		/// <returns>The collection of <typeparamref name="TObject"/> objects.</returns>
		TObject[] GetAll<TObject>(Expression<Func<TObject, object>> wherePropertySelector, object whereValue);

		/// <summary>
		/// Gets a query to enumerate <typeparamref name="TObject"/> objects.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <returns>The <see cref="ObjectQuery{TObject}"/> object.</returns>
		ObjectQuery<TObject> Query<TObject>();
	}
}
