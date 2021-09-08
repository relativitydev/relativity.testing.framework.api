using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents the service that interacts with object manager API.
	/// </summary>
	/// <example>
	/// <code>
	/// IObjectService _objectService = relativityFacade.Resolve&lt;IObjectService&gt;();
	/// </code>
	/// </example>
	public interface IObjectService
	{
		/// <summary>
		/// Creates the specified entity of <typeparamref name="TObject"/> type.
		/// </summary>
		/// <typeparam name="TObject">The type of the object to create.</typeparam>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to create the object, use -1 for administrator level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>Returns the created <typeparamref name="TObject"/> object.</returns>
		/// <example> This example shows how to create object using a data model with ObjectTypeName attribute and existing object type name.
		/// <code>
		/// [ObjectTypeName("Dashboard)]
		/// internal class TestModel : NamedArtifact
		/// {
		/// 	public string TestProperty { get; set; }
		/// }
		/// ...
		/// int workspaceArtifactID = 1015427;
		/// TestModel objectToCreate = new TestModel
		/// {
		/// 	Name = "Test Model Name",
		/// 	TestProperty = "Test Property Value"
		/// };
		/// TestModel createdObject = _objectService.Create(workspaceArtifactID, objectToCreate);
		/// </code>
		/// </example>
		TObject Create<TObject>(int workspaceId, TObject entity)
			where TObject : Artifact;

		/// <summary>
		/// Creates the specified workspace entities of <typeparamref name="TObject"/> type.
		/// </summary>
		/// <typeparam name="TObject">The type of the objects to create.</typeparam>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to create the object.</param>
		/// <param name="entities">The array of entities to create.</param>
		/// <returns>Returns the list with ArtifactIDs of created entities.</returns>
		/// <example> This example shows how to mass create objects using a data model with ObjectTypeName attribute and existing object type name.
		/// <code>
		/// [ObjectTypeName("Dashboard)]
		/// internal class TestModel : NamedArtifact
		/// {
		/// 	public string TestProperty { get; set; }
		/// }
		/// ...
		/// int workspaceArtifactID = 1015427;
		/// List&lt;TestModel&gt; objectsToCreate = new List&lt;TestModel&gt;
		/// {
		/// 	new TestModel
		/// 	{
		/// 		Name = "Test Model Name",
		/// 		TestProperty = "Test Property Value"
		/// 	},
		/// 	new TestModel
		/// 	{
		/// 		Name = "Other Test Model Name"
		/// 		TestProperty = "Other Test Property Value",
		/// 	},
		/// };
		/// List&lt;TestModel&gt; createdObjects = _objectService.Create(workspaceArtifactID, objectsToCreate);
		/// </code>
		/// </example>
		List<int> Create<TObject>(int workspaceId, IEnumerable<TObject> entities)
			where TObject : Artifact;

		/// <summary>
		/// Updates specified fields on a list of Documents or Relativity Dynamic Objects (RDOs) that match a set of identifiers by setting the fields to the same value.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to update the object, use -1 for administrator level context.</param>
		/// <param name="massRequestByObjectIdentifiers">A request to update multiple Documents or Relativity Dynamic Objects (RDOs) that have the specified identifiers.</param>
		/// <remarks>A <see cref="MassUpdateByObjectIdentifiersRequest"/> object specifies the identifiers used to select a list of objects with the same type for updating. It also includes the same
		/// values for all object fields that are to be updated. In the Relativity UI, this update operation is equivalent to the user selecting the Checked or These option in
		/// the mass operations bar on a list page.
		/// Process halts at first failure with no rollback.</remarks>
		/// <example> This shows how to mass update the value of the field 'Extracted Text' on the Documents.
		/// <code>
		/// int workspaceArtifactID = 1017850;
		/// string extractedTextValueToUpdate = "Updated Extracted Text Value";
		/// List&lt;FieldRefValuePair&gt; fieldValues = new List&lt;FieldRefValuePair&gt;
		/// {
		/// 	new FieldRefValuePair
		/// 	{
		/// 		Field = new FieldRef
		/// 		{
		/// 			Name = "Extracted Text"
		/// 		},
		/// 		Value = extractedTextValueToUpdate
		/// 	}
		/// };
		/// Document[] documentsToUpdate = relativityFacade.Resolve&lt;IDocumentService&gt;().GetAll().Take(2);
		/// MassUpdateByObjectIdentifiersRequest massRequestByObjectIdentifiers = new MassUpdateByObjectIdentifiersRequest
		/// {
		/// 	Objects = documentsToUpdate,
		/// 	FieldValues = fieldValues
		/// };
		/// _objectService.Update(workspaceArtifactID, massRequestByObjectIdentifiers);
		/// </code>
		/// </example>
		void Update(int workspaceId, MassUpdateByObjectIdentifiersRequest massRequestByObjectIdentifiers);

		/// <summary>
		/// Updates the specified workspace entity of <typeparamref name="TObject"/> type.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <param name="workspaceId">The workspace ArtifactID.</param>
		/// <param name="entity">The entity to update.</param>
		/// <example> This shows how to update the value of the SingleChoiceField 'Confidential Designation' on the Document.
		/// <code>
		/// [ObjectTypeName("Document")]
		/// internal class DocumentWithSingleChoice : Document
		/// {
		/// 	public Artifact ConfidentialDesignation { get; set; }
		/// }
		/// ...
		/// int workspaceArtifactID = 1017850;
		/// int existingDocumentArtifactID = 1039664;
		/// DocumentWithSingleChoice documentToUpdate = _objectService.GetAll&lt;DocumentWithSingleChoice&gt;(d => d.ArtifactID, documentArtifactID).FirstOrDefault();
		/// _objectService.Update(workspaceArtifactID, documentToUpdate);
		/// </code>
		/// </example>
		/// <example> This shows how to update the value of the field for which DTO model exists in RTF (Document).
		/// <code>
		/// int workspaceArtifactID = 1017850;
		/// string existingDocumentName = "Some Existing Document Name";
		/// Document documentToUpdate = relativityFacade.Resolve&lt;IDocumentService&gt;().Get(workspaceArtifactID, existingDocumentName);
		/// _objectService.Update(workspaceArtifactID, documentToUpdate);
		/// </code>
		/// </example>
		void Update<TObject>(int workspaceId, TObject entity)
			where TObject : Artifact;

		/// <summary>
		/// Deletes the workspace entity by the specified ArtifactIDs of workspace and entity.
		/// </summary>
		/// <param name="workspaceId">The workspace ArtifactID.</param>
		/// <param name="entityId">The entity ArtifactID.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1017850;
		/// int existingEntityArtifactID = 1039664;
		/// _objectService.Delete(workspaceArtifactID, existingEntityArtifactID);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Deletes the workspace entities by the specified ArtifactIDs of workspace and entities.
		/// </summary>
		/// <param name="workspaceId">The workspace ArtifactID.</param>
		/// <param name="entityIds">The entity ArtifactIDs.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1017850;
		/// var existingEntitiesArtifactIDs = new List&lt;int&gt; { 1039664, 1039689 };
		/// _objectService.Delete(workspaceArtifactID, existingEntitiesArtifactIDs);
		/// </code>
		/// </example>
		void Delete(int workspaceId, IEnumerable<int> entityIds);

		/// <summary>
		/// Gets all objects of <typeparamref name="TObject"/> type.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <returns>The collection of <typeparamref name="TObject"/> objects.</returns>
		/// <example> This example shows how to get all entities of GroupObjectType with Object Type Name "Group".
		/// <code>
		/// [ObjectTypeName("Group")]
		/// internal class GroupObject
		/// {
		/// 	public int ArtifactID { get; set; }
		/// 	public string Name { get; set; }
		/// }
		/// GroupObject[] groups = _objectService.GetAll&lt;GroupObject&gt;();
		/// </code>
		/// </example>
		TObject[] GetAll<TObject>();

		/// <summary>
		/// Gets all objects of <typeparamref name="TObject"/> type matching specified property filter.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <param name="wherePropertySelector">The filter property selector.</param>
		/// <param name="whereValue">The filter property value.</param>
		/// <returns>The collection of <typeparamref name="TObject"/> objects.</returns>
		/// <example> This example shows how to get all entities of GroupObjectType with Object Type Name "Group" which have "System Administrators" name.
		/// <code>
		/// [ObjectTypeName("Group")]
		/// internal class GroupObject
		/// {
		/// 	public int ArtifactID { get; set; }
		/// 	public string Name { get; set; }
		/// }
		/// GroupObject[] groups = _objectService.GetAll&lt;GroupObject&gt;(group => group.Name, "System Administrators");
		/// </code>
		/// </example>
		TObject[] GetAll<TObject>(Expression<Func<TObject, object>> wherePropertySelector, object whereValue);

		/// <summary>
		/// Gets a query to enumerate <typeparamref name="TObject"/> objects.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <returns>The <see cref="ObjectQuery{TObject}"/> object.</returns>
		/// <example> This example shows how to query for entities of Field type for workspace with specified ArtifactID.
		/// <code>
		/// int workspaceArtifactID = 1017850;
		/// List&lt;Field&gt; fields = _objectService.Query&lt;Field&gt;().For(workspaceArtifactID).ToList();
		/// </code>
		/// </example>
		ObjectQuery<TObject> Query<TObject>();
	}
}
