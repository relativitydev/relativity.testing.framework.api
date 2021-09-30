using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of uploading a file to a file field.
	/// </summary>
	internal interface IFileFieldUploadStrategy
	{
		/// <summary>
		/// Uploads a file to the file field. Saves the objects with the uploadd file.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="fileFieldDto">The [FileFieldDTO](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.FileFieldDTO.html) containing information about the File Field,
		/// file name and filestrean with the file to upload. Must contain ArtifactId or Name of the Field and ObjectRef.</param>
		/// <returns>FileField object containing the GUID of the uploaded file.</returns>
		FileFieldDTO UploadFile(int workspaceId, FileFieldDTO fileFieldDto);
	}
}
