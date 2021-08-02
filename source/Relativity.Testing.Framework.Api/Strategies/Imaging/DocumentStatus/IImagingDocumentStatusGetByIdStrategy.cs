using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingDocumentStatusGetByIdStrategy
	{
		DocumentStatus Get(int workspaceId, int documentArtifactId);

		Task<DocumentStatus> GetAsync(int workspaceId, int documentArtifactId);
	}
}
