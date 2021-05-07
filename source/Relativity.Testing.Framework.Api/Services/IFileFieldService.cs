using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the file field API service.
	/// Exposes methods for downloading and uploading files linked to file fields.
	/// </summary>
	public interface IFileFieldService
	{
		/// <summary>
		/// Downloads a file from a file field.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="fileFieldDto">The <see cref="FileFieldDTO"/> containing information about the File Field of the file to download.
		/// Must contain ArtifactId or Name of the Field and ObjectRef.</param>
		/// <returns>FileField object containing downloaded file.</returns>
		/// <exception cref="ArgumentException">The fileFieldDto does not contain all needed data.</exception>
		FileFieldDTO DownloadFile(int workspaceId, FileFieldDTO fileFieldDto);

		/// <summary>
		/// Uploads a file to the file field. Saves the objects with the uploadd file.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="fileFieldDto">The <see cref="FileFieldDTO"/> containing information about the File Field,
		/// file name and filestrean with the file to upload. Must contain ArtifactId or Name of the Field and ObjectRef.</param>
		/// <returns>FileField object containing the GUID of the uploaded file.</returns>
		/// <exception cref="ArgumentException">The fileFieldDto does not contain all needed data.</exception>
		FileFieldDTO UploadFile(int workspaceId, FileFieldDTO fileFieldDto);
	}
}
