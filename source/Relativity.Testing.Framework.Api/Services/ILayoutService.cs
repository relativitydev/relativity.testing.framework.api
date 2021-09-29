using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the layout API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _layoutService = relatvityFacade.Resolve&lt;ILayoutService&gt;();
	/// </code>
	/// </example>
	public interface ILayoutService
	{
		/// <summary>
		/// Creates the specified Layout.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var existingObjectTypeId = 1;
		/// ObjectType objectType =  relatvityFacade.Resolve&lt;IObjectTypeService&gt;()
		/// 	.Resolve(workspaceId, existingObjectTypeId);
		/// var entity = new Layout
		/// {
		/// 	Name = "Sample Layout Name",
		/// 	ObjectType = objectType
		/// };
		/// Layout createdLayout = _layoutService.Create(workspaceId, entity);
		/// </code>
		/// </example>
		Layout Create(int workspaceId, Layout entity);

		/// <summary>
		/// Requires the specified layout.
		/// <list type="number">
		/// <item><description>If [Artifact.ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</description></item>
		/// <item><description>If [NamedArtifact.Name](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html#Relativity_Testing_Framework_Models_NamedArtifact_Name) property of <paramref name="entity"/> have a value, gets entity by name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</description></item>
		/// </list>
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var existingObjectTypeId = 1;
		/// var existigLayoutId = 2;
		/// ObjectType objectType =  relatvityFacade.Resolve&lt;IObjectTypeService&gt;()
		/// 	.Resolve(workspaceId, existingObjectTypeId);
		/// var entityToUpdateById = new Layout
		/// {
		/// 	Name = "Sample Updated Layout Name",
		/// 	ObjectType = objectType,
		/// 	ArtifactID = existigLayoutId
		/// };
		/// Layout layoutUpdatedById = _layoutService.Require(workspaceId, entityToUpdateById);
		/// var entityToUpdateByName = new Layout
		/// {
		/// 	Name = "Sample Existing Layout Name",
		/// 	ObjectType = objectType,
		/// 	AllowCopyFromPrevious = true
		/// };
		/// Layout layoutUpdatedByName = _layoutService.Require(workspaceId, entityToUpdateByName);
		/// var entityToCreate = new Layout
		/// {
		/// 	Name = "Sample Not Existing Layout Name",
		/// 	ObjectType = objectType,
		/// };
		/// Layout createdLayout = _layoutService.Require(workspaceId, entityToCreate);
		/// </code>
		/// </example>
		Layout Require(int workspaceId, Layout entity);

		/// <summary>
		/// Deletes the layout by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the layout.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var existigLayoutId = 1;
		/// _layoutService.Delete(workspaceId, existigLayoutId);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the layout by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the layout.</param>
		/// <returns>The <see cref="Layout"/> entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var existigLayoutId = 1;
		/// Layout layout = _layoutService.Get(workspaceId, existigLayoutId);
		/// </code>
		/// </example>
		Layout Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the list eligible to be a layout owner by the specified workspace ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <returns>The list eligible to be a layout owner.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// List&lt;NamedArtifact&gt; eligibleOwners = _layoutService.GetEligibleOwners(workspaceId);
		/// </code>
		/// </example>
		List<NamedArtifact> GetEligibleOwners(int workspaceId);

		/// <summary>
		/// Updates the specified layout.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var existingLayoutId = 1;
		/// Layout layoutToUpdate = _layoutService.Get(workspaceId, existingLayoutId);
		/// layoutToUpdate.Name = "Some Updated Layout Name";
		/// _layoutService.Update(workspaceId, layoutToUpdate);
		/// </code>
		/// </example>
		void Update(int workspaceId, Layout entity);

		/// <summary>
		/// Builds the layout.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The layout to build. If ArtifactId is provided, it will be used, otherwise it will be looked up by name. If neither are provided, this will put us in an exceptional state.</param>
		/// <param name="categoryFields">The fields to add to a category.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var existingObjectTypeId = 1;
		/// var existingFieldId = 2;
		/// var existingFieldTypeId = 3;
		/// Layout layout = _layoutService.Get(workspaceId, existingLayoutId);
		/// var fieldsToAdd = new List&lt;CategoryField&gt;
		/// {
		/// 	new CategoryField
		/// 	{
		/// 		DisplayName = "Some Field Name",
		/// 		FieldArtifactID = existingFieldId,
		/// 		FieldDisplayType = FieldDisplayType.Decimal,
		/// 		FieldTypeID = existingFieldTypeId,
		/// 		IsRequired = true
		/// 	}
		/// };
		/// _layoutService.AddFields(workspaceId, layout, fieldsToAdd);
		/// </code>
		/// </example>
		void AddFields(int workspaceId, Layout entity, List<CategoryField> categoryFields);

		/// <summary>
		/// Gets the categories in a layout.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the layout,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The layout to get the categories for. If ArtifactId is provided, it will be used, otherwise it will be looked up by name. If neither are provided, this will put us in an exceptional state.</param>
		/// <returns>The list of categories in layout.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var existingLayoutId = 1;
		/// Layout layout = _layoutService.Get(workspaceId, existingLayoutId);
		/// List&lt;Category&gt; categories = _layoutService.GetCategories(workspaceId, layout);
		/// </code>
		/// </example>
		List<Category> GetCategories(int workspaceId, Layout entity);
	}
}
