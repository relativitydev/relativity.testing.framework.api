using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the optical character recognition API service.
	/// </summary>
	public interface IOcrProfileService
	{
		/// <summary>
		/// Creates the specified OCR profile.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to add the new OCR profile,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entity">The entity to create.</param>
		/// <returns>The created entity.</returns>
		OcrProfile Create(int workspaceId, OcrProfile entity);

		/// <summary>
		/// Deletes the OCR profile by ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to delete the OCR profile,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the OCR profile.</param>
		void Delete(int workspaceId, int entityId);

		/// <summary>
		/// Gets the OCR profile by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the OCR profile,
		/// or use -1 to indicate the admin-level context.</param>
		/// <param name="entityId">The artifact ID of the OCR profile.</param>
		/// <returns>>The <see cref="ObjectType"/> entity or <see langword="null"/>.</returns>
		OcrProfile Get(int workspaceId, int entityId);
	}
}
