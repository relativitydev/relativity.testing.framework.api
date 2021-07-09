using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the Imaging API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _imagingService = relativityFacade.Resolve&lt;IImagingService&gt;();
	/// </code>
	/// </example>
	public interface IImagingService
	{
		/// <summary>
		/// Creates a new Imaging Profile instance, specifying only Basic Profile options. Native Profile options will be set to
		/// default values, as occurs when creating a Basic Profile in the UI.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dto">A <see cref="CreateBasicImagingProfileDTO"/> object corresponding to the desired Imaging Profile object.</param>
		/// <returns>Returns an <see cref="ImagingProfile"/> instance.</returns>
		ImagingProfile CreateBasic(int workspaceId, CreateBasicImagingProfileDTO dto);

		/// <summary>
		/// Creates a new Imaging Profile instance, specifying only Basic Profile options. Native Profile options will be set to
		/// default values, as occurs when creating a Basic Profile in the UI.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dto">A <see cref="CreateBasicImagingProfileDTO"/> object corresponding to the desired Imaging Profile object.</param>
		/// <returns>A <see cref="Task"/> with an <see cref="ImagingProfile"/> instance.</returns>
		Task<ImagingProfile> CreateBasicAsync(int workspaceId, CreateBasicImagingProfileDTO dto);

		/// <summary>
		/// Creates a new Imaging Profile instance, specifying both Basic and Native Profile options.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dto">A <see cref="CreateNativeImagingProfileDTO"/> object corresponding to the desired Imaging Profile object.</param>
		/// <returns>Returns an <see cref="ImagingProfile"/> instance.</returns>
		ImagingProfile CreateNative(int workspaceId, CreateNativeImagingProfileDTO dto);

		/// <summary>
		/// Creates a new Imaging Profile instance, specifying both Basic and Native Profile options.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="dto">A <see cref="CreateNativeImagingProfileDTO"/> object corresponding to the desired Imaging Profile object.</param>
		/// <returns>A <see cref="Task"/> with an <see cref="ImagingProfile"/> instance.</returns>
		Task<ImagingProfile> CreateNativeAsync(int workspaceId, CreateNativeImagingProfileDTO dto);

		/// <summary>
		/// Updates an existing Imaging Profile instance.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfile">An <see cref="ImagingProfile"/> object with updated fields, reflecting the desired final state of the Imaging Profile.</param>
		void Update(int workspaceId, ImagingProfile imagingProfile);

		/// <summary>
		/// Updates an existing Imaging Profile instance.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfile">An <see cref="ImagingProfile"/> object with updated fields, reflecting the desired final state of the Imaging Profile.</param>
		/// <returns>A <see cref="Task"/> with an <see cref="ImagingProfile"/> instance.</returns>
		Task UpdateAsync(int workspaceId, ImagingProfile imagingProfile);

		/// <summary>
		/// Retrieves the <see cref="ImagingProfile"/> with the specified Artifact ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfileId">The Artifact ID of the <see cref="ImagingProfile"/> instance to read.</param>
		/// <returns>Returns an <see cref="ImagingProfile"/> instance.</returns>
		ImagingProfile Get(int workspaceId, int imagingProfileId);

		/// <summary>
		/// Retrieves the <see cref="ImagingProfile"/> with the specified Artifact ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfileId">The Artifact ID of the <see cref="ImagingProfile"/> instance to read.</param>
		/// <returns>A <see cref="Task"/> with an <see cref="ImagingProfile"/> instance.</returns>
		Task<ImagingProfile> GetAsync(int workspaceId, int imagingProfileId);

		/// <summary>
		/// Deletes an Imaging Profile instance.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfileId">The Artifact ID of the <see cref="ImagingProfile"/> instance to delete.</param>
		void Delete(int workspaceId, int imagingProfileId);

		/// <summary>
		/// Deletes an Imaging Profile instance.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="imagingProfileId">The Artifact ID of the <see cref="ImagingProfile"/> instance to delete.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task DeleteAsync(int workspaceId, int imagingProfileId);
	}
}