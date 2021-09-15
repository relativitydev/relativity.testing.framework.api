using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the field API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _fieldService = relativityFacade.Resolve&lt;IFieldService&gt;();
	/// </code>
	/// </example>
	public interface IFieldService
	{
		/// <summary>
		/// Creates the specified field.
		/// </summary>
		/// <typeparam name="TFieldModel">The field type.</typeparam>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var fieldForPropagation = _fieldService.Get(workspaceId, "MD5 Hash");
		/// WholeNumberField fieldToCreate = new WholeNumberField
		/// {
		/// 	PropagateTo = new FieldPropagate
		/// 	{
		/// 		ViewableItems = new List&lt;Artifact&gt;
		/// 		{
		/// 			new Artifact
		/// 			{
		/// 				ArtifactID = fieldForPropagation.ArtifactID
		/// 			}
		/// 		}
		/// 	}
		/// };
		/// var createdField = _fieldService.Create(workspaceId, fieldToCreate);
		/// </code>
		/// </example>
		TFieldModel Create<TFieldModel>(int workspaceId, TFieldModel entity)
			where TFieldModel : Field;

		/// <summary>
		/// Deletes the field by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the field.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var someExistingFieldArtifactId = 1;
		/// _fieldService.Delete(workspaceId, someExistingFieldArtifactId);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the field by the specified ID.
		/// Returns only base parameters of field.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the field.</param>
		/// <returns>The <see cref="Field"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var someExistingFieldArtifactId = 1;
		/// var fieldWithBaseParameters = _fieldService.Get(workspaceId, someExistingFieldArtifactId);
		/// </code>
		/// </example>
		Field Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the field of specified type by the specified ID.
		/// </summary>
		/// <typeparam name="TFieldModel">The field type.</typeparam>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the field.</param>
		/// <returns>The <see cref="Field"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var someExistingYesNoFieldArtifactId = 1;
		/// var yesNoField = _fieldService.Get&lt;YesNoField&gt;(workspaceId, someExistingYesNoFieldArtifactId);
		/// </code>
		/// </example>
		TFieldModel Get<TFieldModel>(int workspaceId, int entityId)
			where TFieldModel : Field;

		/// <summary>
		/// Gets the field by the specified name.
		/// Returns only base parameters of field.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the object type.</param>
		/// <returns>The <see cref="Field"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var fieldWithBaseParameters = _fieldService.Get(workspaceId, "Some Existing Field Name");
		/// </code>
		/// </example>
		Field Get(int workspaceId, string entityName);

		/// <summary>
		/// Gets the field of specified type by the specified name.
		/// </summary>
		/// <typeparam name="TFieldModel">The field type.</typeparam>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the field.</param>
		/// <returns>The <see cref="Field"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var currencyField = _fieldService.Get&lt;CurrencyField&gt;(workspaceId, "Some Existing Currency Field Name");
		/// </code>
		/// </example>
		TFieldModel Get<TFieldModel>(int workspaceId, string entityName)
			where TFieldModel : Field;

		/// <summary>
		/// Updates the specified field.
		/// </summary>
		/// <typeparam name="TFieldModel">The field type.</typeparam>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		/// <returns>The updated entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var fieldForPropagationArtifactId = 2;
		/// var userFieldToUpdate = new UserField
		/// {
		/// 	PropagateTo = new FieldPropagate
		/// 	{
		/// 		ViewableItems = new List&lt;Artifact&gt;
		/// 		{
		/// 			new Artifact
		/// 			{
		/// 				ArtifactID = fieldForPropagationArtifactId
		/// 			}
		/// 		}
		/// 	},
		/// 	OpenToAssociations = true,
		/// 	Name = "Updated Name"
		/// };
		/// _fieldService.Update(workspaceId, userFieldToUpdate);
		/// </code>
		/// </example>
		TFieldModel Update<TFieldModel>(int workspaceId, TFieldModel entity)
			where TFieldModel : Field;

		/// <summary>
		/// Requires the specified field.
		/// <list type="number">
		/// <item><description>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/>
		/// has positive value, gets entity by ID and updates it.</description></item>
		/// <item><description>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/>
		/// has a value, gets entity by name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new entity.</description></item>
		/// </list>
		/// </summary>
		/// <typeparam name="TFieldModel">The field type.</typeparam>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the field,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		/// <returns>The entity required.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var fieldForPropagation = _fieldService.Get(workspaceId, "MD5 Hash");
		/// var existingFieldArtifactId = 1;
		/// var existingObjectType = relativityFacade.Resolve&lt;IObjectTypeService&gt;().Get(workspaceId, "Document");
		/// var fieldToUpdate = new SingleChoiceField
		/// {
		/// 	PropagateTo = new FieldPropagate
		/// 	{
		/// 		ViewableItems = new List&lt;Artifact&gt;
		/// 		{
		/// 			new Artifact(fieldForPropagation.ArtifactID)
		/// 		},
		/// 	},
		/// 	Name = "Some Updated Name",
		/// 	ArtifactID = existingFieldArtifactId,
		/// 	ObjectType = existingObjectType
		/// };
		/// var updatedField = _fieldService.Require(workspaceId, fieldToUpdate); // Will get field by ArtifactId, update it and return updated
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var fieldForPropagation = _fieldService.Get(workspaceId, "MD5 Hash");
		/// var existingObjectType = relativityFacade.Resolve&lt;IObjectTypeService&gt;().Get(workspaceId, "Document");
		/// var fieldToUpdate = new MultipleChoiceField
		/// {
		/// 	PropagateTo = new FieldPropagate
		/// 	{
		/// 		ViewableItems = new List&lt;Artifact&gt;
		/// 		{
		/// 			new Artifact(fieldForPropagation.ArtifactID)
		/// 		},
		/// 		Name = "Some Existing Field Name",
		/// 		ObjectType = existingObjectType
		/// };
		/// var updatedField = _fieldService.Require(workspaceId, fieldToUpdate); // Will get field by Name, update it and return updated
		/// </code>
		/// </example>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var fieldForPropagation = _fieldService.Get(workspaceId, "MD5 Hash");
		/// FixedLengthTextField fieldToCreate = new FixedLengthTextField
		/// {
		/// 	PropagateTo = new FieldPropagate
		/// 	{
		/// 		ViewableItems = new List&lt;Artifact&gt;
		/// 		{
		/// 			new Artifact(fieldForPropagation.ArtifactID)
		/// 		}
		/// 	},
		/// 	Unicode = true
		/// };
		/// var createdField = _fieldService.Require(workspaceId, fieldToCreate); // Will create new FixedLengthTextField
		/// </code>
		/// </example>
		TFieldModel Require<TFieldModel>(int workspaceId, TFieldModel entity)
			where TFieldModel : Field;
	}
}
