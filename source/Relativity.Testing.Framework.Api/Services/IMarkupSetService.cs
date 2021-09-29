using Relativity.Testing.Framework.Api.Strategies;
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
		///  Creates the specified [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html).
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to create the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html),
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) to create.</param>
		/// <returns>The [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) entity.</returns>
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
		/// Gets the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html),
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The ArtifactID of the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html).</param>
		/// <returns>The [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// int markupSetArtifactID = 1236575;
		/// MarkupSet markupSet = _markupSetService.Get(workspaceArtifactID, markupSetArtifactID);
		/// </code>
		/// </example>
		MarkupSet Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) by the specified name.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get the markup set,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityName">The name of the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html).</param>
		/// <returns>The [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// string markupSetName = "Existing Markup Set Name";
		/// MarkupSet markupSet = _markupSetService.Get(workspaceArtifactID, markupSetName);
		/// </code>
		/// </example>
		MarkupSet Get(int workspaceId, string entityName);

		/// <summary>
		/// Requires the specified [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html).
		/// <list type="number">
		/// <item><description>If [Artifact.ArtifactID](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Artifact.html#Relativity_Testing_Framework_Models_Artifact_ArtifactID) property of <paramref name="entity"/> has positive value, gets [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) by ArtifactID and updates it.</description></item>
		/// <item><description>If [NamedArtifact.Name](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.NamedArtifact.html#Relativity_Testing_Framework_Models_NamedArtifact_Name) property of <paramref name="entity"/> has a value, gets [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) by name and updates it if it exists.</description></item>
		/// <item><description>Otherwise creates a new [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html).</description></item>
		/// </list>
		/// </summary>
		/// <returns>The [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) entity.</returns>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to require markup set.</param>
		/// <param name="entity">The [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) to require.</param>
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
		/// Determines whether the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) with the specified ArtifactID exists.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to check if the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) exists,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The ArtifactID of the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html).</param>
		/// <returns><see langword="true"/> if a [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) exists; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// int markupSetArtifactID = 1235876;
		/// bool markupSetExists = _markupSetService.Exists(workspaceArtifactID, markupSetArtifactID);
		/// </code>
		/// </example>
		bool Exists(int workspaceId, int entityId);

		/// <summary>
		/// Updates the specified [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html).
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to update the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html),
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) to update.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1234567;
		/// int markupSetArtifactID = 1236575;
		/// MarkupSet markupSetToUpdate = _markupSetService.Get(workspaceArtifactID, markupSetArtifactID);
		/// markupSetToUpdate.Name = "Updated Makup Set Name";
		/// markupSetToUpdate.Order = 200;
		/// markupSetToUpdate.RedactionText = "Updated Redaction Set";
		/// _markupSetService.Update(workspaceArtifactID, markupSetToUpdate);
		/// </code>
		/// </example>
		void Update(int workspaceId, MarkupSet entity);

		/// <summary>
		/// Deletes the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html) by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to delete the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html),
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The ArtifactID of the [MarkupSet](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MarkupSet.html).</param>
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
