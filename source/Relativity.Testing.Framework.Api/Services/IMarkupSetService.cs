using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the markup API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IMarkupSetService _markupSetService = relativityFacade.Resolve&lt;IMarkupSetService&gt;();
	/// </code>
	/// </example>
	public interface IMarkupSetService
	{
		/// <summary>
		///  Creates the specified <see cref="MarkupSet"/>.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to create the <see cref="MarkupSet"/>,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The <see cref="MarkupSet"/> to create.</param>
		/// <returns>The <see cref="MarkupSet"/> entity.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// var markupSet = new MarkupSet
		/// {
		/// 	Name = "Markup Set Name",
		/// 	Order = 1,
		/// 	RedactionText = "Redaction Text"
		/// };
		/// MarkupSet createdMarkupSet = _markupSetService.Create(workspaceArtifactID, markupSet);
		/// </code>
		/// </example>
		MarkupSet Create(int workspaceId, MarkupSet entity);

		/// <summary>
		/// Gets the <see cref="MarkupSet"/> by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get the <see cref="MarkupSet"/>,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The ArtifactID of the <see cref="MarkupSet"/>.</param>
		/// <returns>The <see cref="MarkupSet"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// int markupSetArtifactID = 1236575;
		/// MarkupSet markupSet = _markupSetService.Get(workspaceArtifactID, markupSetArtifactID);
		/// </code>
		/// </example>
		MarkupSet Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the <see cref="MarkupSet"/> by the specified name.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get the markup set,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the <see cref="MarkupSet"/>.</param>
		/// <returns>The <see cref="MarkupSet"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// string markupSetName = "Existing Markup Set Name";
		/// MarkupSet markupSet = _markupSetService.Get(workspaceArtifactID, markupSetName);
		/// </code>
		/// </example>
		MarkupSet Get(int workspaceId, string entityName);

		/// <summary>
		/// Requires the specified <see cref="MarkupSet"/>.
		/// <list type="number">
		/// <item><description>If <see cref="Artifact.ArtifactID"/> property of <paramref name="entity"/> has positive value, gets <see cref="MarkupSet"/> by ArtifactID and updates it.</description></item>
		/// <item><description>If <see cref="NamedArtifact.Name"/> property of <paramref name="entity"/> has a value, gets <see cref="MarkupSet"/> by name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new <see cref="MarkupSet"/>.</description></item>
		/// </list>
		/// </summary>
		/// <returns>The <see cref="MarkupSet"/> entity.</returns>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to require markup set.</param>
		/// <param name="entity">The <see cref="MarkupSet"/> to require.</param>
		/// <example> This example shows how to update and get updated Markup Set by Require method with MarkupSet entity that has ArtifactID filled.
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// int existingMarkupSetArtifactID = 1235876;
		/// var markupSetToUpdate = new MarkupSet
		/// {
		/// 	ArtifactID = existingMarkupSetArtifactID,
		/// 	Name = "Updated Markup Set Name",
		/// 	Order = 1,
		/// 	RedactionText = "Updated Redaction Text"
		/// };
		/// MarkupSet updatedMarkupSet = _markupSetService.Require(workspaceArtifactID, markupSetToUpdate);
		/// </code>
		/// </example>
		/// <example> This example shows how to update and get updated Markup Set by Require method with MarkupSet entity that has Name filled.
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// var markupSetToUpdate = new MarkupSet
		/// {
		/// 	Name = "Existing Markup Set Name",
		/// 	Order = 20,
		/// 	RedactionText = "Updated Redaction Text"
		/// };
		/// MarkupSet updatedMarkupSet = _markupSetService.Require(workspaceArtifactID, markupSetToUpdate);
		/// </code>
		/// </example>
		/// <example> This example shows how to create and get new Markup Set by Require method with new MarkupSet entity (Name of the given Markup Set does not match any existing Markup Set in given workspace).
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// var markupSetToCreate = new MarkupSet
		/// {
		/// 	Name = "Not Existing Markup Set Name",
		/// 	Order = 34,
		/// 	RedactionText = "Redaction Text"
		/// };
		/// MarkupSet createdMarkupSet = _markupSetService.Require(workspaceArtifactID, markupSetToCreate);
		/// </code>
		/// </example>
		MarkupSet Require(int workspaceId, MarkupSet entity);

		/// <summary>
		/// Determines whether the <see cref="MarkupSet"/> with the specified ArtifactID exists.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to check if the <see cref="MarkupSet"/> exists,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The ArtifactID of the <see cref="MarkupSet"/>.</param>
		/// <returns><see langword="true"/> if a <see cref="MarkupSet"/> exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// int markupSetArtifactID = 1235876;
		/// bool markupSetExists = _markupSetService.Exists(workspaceArtifactID, markupSetArtifactID);
		/// </code>
		/// </example>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified <see cref="MarkupSet"/>.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to update the <see cref="MarkupSet"/>,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The <see cref="MarkupSet"/> to update.</param>
		/// <returns>The updated <see cref="MarkupSet"/> entity.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// int markupSetArtifactID = 1236575;
		/// MarkupSet markupSetToUpdate = _markupSetService.Get(workspaceArtifactID, markupSetArtifactID);
		/// markupSetToUpdate.Name = "Updated Makup Set Name";
		/// markupSetToUpdate.Order = 200;
		/// markupSetToUpdate.RedactionText = "Updated Redaction Set";
		/// MarkupSet updatedMarkupSet = _markupSetService.Update(workspaceArtifactID, markupSetToUpdate);
		/// </code>
		/// </example>
		MarkupSet Update(int workspaceId, MarkupSet entity);

		/// <summary>
		/// Deletes the <see cref="MarkupSet"/> by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to delete the <see cref="MarkupSet"/>,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The ArtifactID of the <see cref="MarkupSet"/>.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// int markupSetArtifactID = 1235876;
		/// _markupSetService.Delete(workspaceArtifactID, markupSetArtifactID);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);
	}
}
