using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of downloading a file to a file field.
	/// </summary>
	internal interface IFileFieldDownloadStrategy
	{
		/// <summary>
		/// Downloads a file from a file field.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="fileFieldDto">The [FileFieldDTO](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.FileFieldDTO.html) containing information about the File Field of the file to download.
		/// Must contain ArtifactId or Name of the Field and ObjectRef.</param>
		/// <returns>FileField object containing downloaded file.</returns>
		FileFieldDTO DownloadFile(int workspaceId, FileFieldDTO fileFieldDto);
	}
}
