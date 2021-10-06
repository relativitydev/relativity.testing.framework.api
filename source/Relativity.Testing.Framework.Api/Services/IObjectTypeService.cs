using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the object type API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IObjectTypeService _objectTypeService = relativityFacade.Resolve&lt;IObjectTypeService&gt;();
	/// </code>
	/// </example>
	public interface IObjectTypeService
	{
		/// <summary>
		/// Creates the specified [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html).
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to add the new [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html),
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) to create.</param>
		/// <returns>The created [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html).</returns>
		/// <example> This example shows how to create new Object Type without providing any additional properties.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// var objectTypeToCreate = new ObjectType();
		/// ObjectType createdObjectType = _objectTypeService.Create(workspaceArtifactID, objectTypeToCreate);
		/// </code>
		/// </example>
		/// <example> This example shows how to create new Object Type with some not required properties set and using "Document" object type template.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// ObjectType template = relativityFacade.Resolve&lt;IGetWorkspaceEntityByNameStrategy&lt;ObjectType&gt;&gt;().Get(workspaceArtifactID, "Document");
		/// var objectTypeToCreate = new ObjectType
		/// {
		///     ParentObjectType = new ObjectType.WrappedObjectType { Value = template },
		///     Name = "Object Type Name",
		///     CopyInstancesOnParentCopy = true,
		///     CopyInstancesOnCaseCreation = true,
		///     EnableSnapshotAuditingOnDelete = true,
		///     PersistentListsEnabled = true,
		///     PivotEnabled = true,
		///     SamplingEnabled = true
		/// }
		/// ObjectType createdObjectType = _objectTypeService.Create(workspaceArtifactID, objectTypeToCreate);
		/// </code>
		/// </example>
		ObjectType Create(int workspaceId, ObjectType entity);

		/// <summary>
		/// Requires the specified [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html).
		/// <list type="number">
		/// <item><description>If [ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="entity"/> has positive value, gets [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) by ID and updates it.</description></item>
		/// <item><description>If [Name](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html#Relativity_Testing_Framework_Models_NamedArtifact_Name) property of <paramref name="entity"/> have a value, gets [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) by name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html).</description></item>
		/// </list>
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to require [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html),
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) to require.</param>
		/// <returns>The [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) required.</returns>
		/// <example> This example shows how to create new Object Type using Require method.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// var objectTypeToCreate = new ObjectType();
		/// ObjectType createdObjectType = _objectTypeService.Require(workspaceArtifactID, objectTypeToCreate);
		/// </code>
		/// </example>
		/// <example> This example shows how to update and get Object Type by providing object type entity that has ArtifactID property filled.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingObjectTypeArtifactID = 1016786;
		/// var objectTypeToUpdate = new ObjectType
		/// {
		///     ArtifactID = existingObjectTypeArtifactID,
		///     Name = "Updated Object Type Name",
		///     SamplingEnabled = false
		/// }
		/// ObjectType updatedObjectType = _objectTypeService.Require(workspaceArtifactID, objectTypeToUpdate);
		/// </code>
		/// </example>
		/// <example> This example shows how to update and get Object Type by providing object type entity that has Name property filled.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// var objectTypeToUpdate = new ObjectType
		/// {
		///     Name = "Existing Object Type Name",
		///     SamplingEnabled = false,
		///     PersistentListsEnabled = true,
		///     CopyInstancesOnParentCopy = true
		/// }
		/// ObjectType updatedObjectType = _objectTypeService.Require(workspaceArtifactID, objectTypeToUpdate);
		/// </code>
		/// </example>
		ObjectType Require(int workspaceId, ObjectType entity);

		/// <summary>
		/// Deletes the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) by ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to delete the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html),
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The ArtifactID of the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html).</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingObjectTypeArtifactID = 1016786;
		/// _objectTypeService.Delete(workspaceArtifactID, existingObjectTypeArtifactID);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html),
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The ArtifactID of the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html).</param>
		/// <returns>The [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingObjectTypeArtifactID = 1016786;
		/// ObjectType objectType = _objectTypeService.Get(workspaceArtifactID, existingObjectTypeArtifactID);
		/// </code>
		/// </example>
		ObjectType Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) by the specified name.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html),
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html).</param>
		/// <returns>The [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingObjectTypeName = "Existing Object Type Name";
		/// ObjectType objectType = _objectTypeService.Get(workspaceArtifactID, existingObjectTypeArtifactID);
		/// </code>
		/// </example>
		ObjectType Get(int workspaceId, string entityName);

		/// <summary>
		/// Updates the specified [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html).
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to update the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html),
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) to update.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingObjectTypeArtifactID = 1016786;
		/// ObjectType objectTypeToUpdate = _objectTypeService.Get(workspaceArtifactID, existingObjectTypeArtifactID);
		/// objectTypeToUpdate.Name = "Updated Object Type Name";
		/// objectTypeToUpdate.PivotEnabled = true;
		/// objectTypeToUpdate.EnableSnapshotAuditingOnDelete = true;
		/// _objectTypeService.Update(workspaceArtifactID, objectTypeToUpdate);
		/// </code>
		/// </example>
		void Update(int workspaceId, ObjectType entity);

		/// <summary>
		/// Gets the list of dependencies.
		/// </summary>
		/// <param name="workspaceId">The workspace ArtifactID.</param>
		/// <param name="entityId">The ArtifactID of the [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html).</param>
		/// <returns>The list of dependencies.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingObjectTypeArtifactID = 1016786;
		/// List&lt;Dependency&gt; dependencies = _objectTypeService.GetDependencies(workspaceArtifactID, existingObjectTypeArtifactID);
		/// </code>
		/// </example>
		List<Dependency> GetDependencies(int workspaceId, int entityId);

		/// <summary>
		/// Gets a list of all [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html)s available to be a parent [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html) for a given workspace.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace to view all the available parent [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html). Use -1 to indicate the admin workspace.</param>
		/// <returns>All [ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ObjectType.html)s available to be parents in a workspace.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// List&lt;ObjectType&gt; availableParentObjectTypes = _objectTypeService.GetDependencies(workspaceArtifactID);
		/// </code>
		/// </example>
		List<ObjectType> GetAvailableParentObjectTypes(int workspaceId);
	}
}
