using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IImagingDocumentStatusGetByIdStrategy
	{
		DocumentStatus Get(int workspaceId, int documentArtifactId);
	}
}
