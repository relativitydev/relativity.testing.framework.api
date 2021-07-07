using System.Threading.Tasks;
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
		/// <returns>Return a <see cref="DocumentStatus"/> of imaging job for a document.</returns>
		DocumentStatus GetStatus(int workspaceId, int documentArtifactId);

		/// <summary>
		/// Get status information about the imaging job for a document.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="documentArtifactId">The Document ArtifactID.</param>
		/// <returns>A task with <see cref="DocumentStatus"/>.</returns>
		Task<DocumentStatus> GetStatusAsync(int workspaceId, int documentArtifactId);
	}
}
