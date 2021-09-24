using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the optical character recognition API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IOcrProfileService _ocrProfileService = relativityFacade.Resolve&lt;IOcrProfileService&gt;();
	/// </code>
	/// </example>
	public interface IOcrProfileService
	{
		/// <summary>
		/// Creates the specified <see cref="OcrProfile"/>.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to add the <see cref="OcrProfile"/>,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The <see cref="OcrProfile"/>entity to create.</param>
		/// <returns>The created <see cref="OcrProfile"/> entity.</returns>
		/// <example> This example shows how to create OCR Profile with some properties set.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// var ocrProfileToCreate = new OcrProfile
		/// {
		/// 	Name = "My script Name",
		/// 	ImageTimeout = 120
		/// };
		/// OcrProfile ocrProfile = _ocrProfileService.Create(workspaceArtifactID, ocrProfileToCreate);
		/// </code>
		/// </example>
		/// <example> This example shows how to create OCR Profile without setting any properties - default values will be assigned.
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// OcrProfile ocrProfile = _ocrProfileService.Create(workspaceArtifactID, new OcrProfile());
		/// </code>
		/// </example>
		OcrProfile Create(int workspaceId, OcrProfile entity);

		/// <summary>
		/// Deletes the <see cref="OcrProfile"/> by ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to delete the <see cref="OcrProfile"/>,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The ArtifactID of the <see cref="OcrProfile"/>.</param>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingOCRProfileArtifactID = 1015427;
		/// _ocrProfileService.Delete(workspaceArtifactID, existingOCRProfileArtifactID);
		/// </code>
		/// </example>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the <see cref="OcrProfile"/> by the specified ArtifactID.
		/// </summary>
		/// <param name="workspaceId">The ArtifactID of the workspace where you want to get the <see cref="OcrProfile"/>,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The ArtifactID of the <see cref="OcrProfile"/>.</param>
		/// <returns>>The <see cref="OcrProfile"/> entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// int workspaceArtifactID = 1015427;
		/// int existingOCRProfileArtifactID = 1015427;
		/// OcrProfile ocrProfile = _ocrProfileService.Get(workspaceArtifactID, existingOCRProfileArtifactID);
		/// </code>
		/// </example>
		OcrProfile Get(int workspaceId, int entityId);
	}
}
