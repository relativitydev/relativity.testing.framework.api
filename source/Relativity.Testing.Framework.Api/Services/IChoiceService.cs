using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the choice API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _choiceService = relativityFacade.Resolve&lt;IChoiceService&gt;();
	/// </code>
	/// </example>
	public interface IChoiceService
	{
		/// <summary>
		/// Creates the specified choice.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new choice,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var fieldName = "Some Field Name";
		/// var existingField = relativityFacade.Resolve&lt;IFieldService&gt;().Get&lt;MultipleChoiceField&gt;(workspaceId, fieldName);
		/// var entity = new Choice
		/// {
		/// 	Name = Randomizer.GetString(),
		/// 	Field = existingField
		/// };
		/// var createdChoice = _choiceService.Create(workspaceId, entity);
		/// </code>
		/// </example>
		Choice Create(int workspaceId, Choice entity);

		/// <summary>
		/// Requires the specified choice.
		/// <list type="number">
		/// <item><description>If [Artifact.ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="entity"/> has positive value, gets entity by ID and updates it.</description></item>
		/// <item><description>If [NamedArtifact.Name](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html#Relativity_Testing_Framework_Models_NamedArtifact_Name) and [Choice.ObjectType](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Choice.html#Relativity_Testing_Framework_Models_Choice_ObjectType) and [Choice.Field](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Choice.html#Relativity_Testing_Framework_Models_Choice_Field) name properties of <paramref name="entity"/> have a value, gets entity by name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new entity using <see cref="ICreateWorkspaceEntityStrategy{T}"/>.</description></item>
		/// </list>
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to require choice,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to require.</param>
		/// <returns>The entity required.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var fieldName = "Some Field Name";
		/// var existingChoiceArtifactId = 1;
		/// var existingField = relativityFacade.Resolve&lt;IFieldService&gt;().Get&lt;MultipleChoiceField&gt;(workspaceId, fieldName);
		/// var entityToUpdateByArtifactId = new Choice
		/// {
		/// 	ArtifactId = existingChoiceArtifactId,
		/// 	Field = existingField,
		/// 	Name = "Some Updated Choice Name"
		/// };
		/// var updatedByArtifactIdChoice = _choiceService.Require(workspaceId, entityToUpdateByArtifactId); // Will get the choice field by ArtifactId, update it and return updated choice.
		/// var entityToUpdateByNameObjectTypeAndField = new Choice
		/// {
		/// 	Field = existingField,
		/// 	Name = "Some Existing Choice Name",
		/// 	ObjectType = new NamedArtifact { Name = "Some Object Type Name },
		/// 	Color = ChoiceColor.Blue
		/// };
		/// var updatedByNameObjectTypeAndField = _choiceService.Require(workspaceId, entityToUpdateByNameObjectTypeAndField); // Will get the choice field by Name, update it and return updated choice.
		/// var entityToCreate = new Choice
		/// {
		/// 	Name = Randomizer.GetString(),
		/// 	Field = existingField
		/// };
		/// var createdEntity =_choiceService.Require(workspaceId, entityToCreate); // Will create new choice entity and return it.
		/// </code>
		/// </example>
		Choice Require(int workspaceId, Choice entity);

		/// <summary>
		/// Deletes the choice by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the choice,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the choice.</param>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var choiceArtifactId = 1;
		/// _choiceService.Delete(workspaceId, choiceArtifactId);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the choice by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the choice,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the choice.</param>
		/// <returns>The [Choice](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Choice.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var choiceArtifactId = 1;
		/// var choice =_choiceService.Get(workspaceId, choiceArtifactId);
		/// </code>
		/// </example>
		Choice Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets all choices for particular object field.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="objectTypeName">Name of the object type.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns>The collection of [Choice](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Choice.html) entities.</returns>
		/// <example>
		/// <code>
		/// var workspaceId = 1015427;
		/// var objectTypeName = "SomeObjectTypeName";
		/// var fieldName = "SomeFieldName";
		/// var choices =_choiceService.GetAll(workspaceId, objectTypeName, fieldName);
		/// </code>
		/// </example>
		IEnumerable<Choice> GetAll(int workspaceId, string objectTypeName, string fieldName);

		/// <summary>
		/// Updates the specified choice.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to update the choice,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to update.</param>
		/// <example>
		/// <code>
		/// var workspaceId = -1;
		/// var toUpdate = _choiceService.Get(workspaceId, someExistingChoiceArtifactId);
		/// toUpdate.Name = "Some Updated Choice Name";
		/// toUpdate.Order = 100;
		/// toUpdate.Color = ChoiceColor.Orange;
		/// _choiceService.Update(workspaceId, toUpdate);
		/// </code>
		/// </example>
		void Update(int workspaceId, Choice entity);
	}
}
