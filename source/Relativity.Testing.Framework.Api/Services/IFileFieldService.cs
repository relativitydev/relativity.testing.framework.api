using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the file field API service.
	/// Exposes methods for downloading and uploading files linked to file fields.
	/// </summary>
	/// <example>
	/// <code>
	/// _fileFieldService = relativityFacade.Resolve&lt;IFileFieldService&gt;();
	/// </code>
	/// </example>
	public interface IFileFieldService
	{
		/// <summary>
		/// Downloads a file from a file field.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="fileFieldDto">The [FileFieldDTO](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.FileFieldDTO.html) containing information about the File Field of the file to download.
		/// Must contain ArtifactId or Name of the Field and ObjectRef as well as FileStream that can be written.</param>
		/// <returns>FileField object containing downloaded file.</returns>
		/// <exception cref="ArgumentException">The fileFieldDto does not contain all needed data.</exception>
		/// <example>
		/// <code>
		/// var workspaceId = -1;
		/// var objectRef = relativityFacade.Resolve&lt;IObjectService&gt;()
		/// 	.Query&lt;SomeObjectType&gt;()
		/// 	.Where(x => x.Name, "SomeObjectName");
		/// var fileField = new FileField // This field should exist and have object of the objectRef above (Some Object Type)
		/// {
		/// 	Name = "Some File Field Name" // Can also have ArtifactId filled instead of Name
		/// };
		/// var fileField = new FileField
		/// {
		/// 	Field = fileField,
		/// 	ObjectRef = objectRef,
		/// 	FileStream = new MemoryStream();
		/// };
		///
		/// var result = _sut.DownloadFile(workspaceId, fileFieldDto);
		///
		/// using (result.FileStream)
		/// {
		/// 	Assert.That(result.FileStream.Length > 0);
		/// }
		/// </code>
		/// </example>
		FileFieldDTO DownloadFile(int workspaceId, FileFieldDTO fileFieldDto);

		/// <summary>
		/// Uploads a file to the file field. Saves the objects with the uploadd file.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="fileFieldDto">The [FileFieldDTO](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.FileFieldDTO.html) containing information about the File Field,
		/// file name and filestream with the file to upload. Must contain ArtifactId or Name of the Field and ObjectRef.</param>
		/// <returns>FileField object containing the GUID of the uploaded file.</returns>
		/// <exception cref="ArgumentException">The fileFieldDto does not contain all needed data.</exception>
		/// <example>
		/// <code>
		/// var workspaceId = -1;
		/// var someExstingFileFieldArtifactId = 1;
		/// var = new FileField // This field should exist and have object of the objectRef below (Some Object Type)
		/// {
		/// 	ArtifactId = someExstingFileFieldArtifactId // Can also have Name filled instead of ArtifactId
		/// };
		/// var objectRef = relativityFacade.Resolve&lt;IObjectService&gt;()
		/// 	.Query&lt;SomeObjectType&gt;()
		/// 	.Where(x => x.Name, "SomeObjectName");
		/// var fileFieldDto = new FileFieldDTO
		/// {
		/// 	Field = fileField,
		/// 	ObjectRef = objectRef,
		/// 	FileName = "Some File Name",
		/// };
		///
		/// using (FileStream fileStream = File.OpenRead("C:\sample\path\to\SomeFile"))
		/// {
		/// 	fileFieldDto.FileStream = fileStream;
		///
		/// 	fileFieldDto = _sut.UploadFile(workspaceId, fileFieldDto);
		/// }
		/// </code>
		/// </example>
		FileFieldDTO UploadFile(int workspaceId, FileFieldDTO fileFieldDto);
	}
}
