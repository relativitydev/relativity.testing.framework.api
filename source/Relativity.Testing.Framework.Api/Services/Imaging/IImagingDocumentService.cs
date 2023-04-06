using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal interface IImagingDocumentService
	{
		/// <summary>
		/// Get status information about the imaging job for a document.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="documentArtifactId">The Document ArtifactID.</param>
		/// <returns>Return a [DocumentStatus](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.DocumentStatus.html) of imaging job for a document.</returns>
		DocumentStatus GetStatus(int workspaceId, int documentArtifactId);
	}
}
