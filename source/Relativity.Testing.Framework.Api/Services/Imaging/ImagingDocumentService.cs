using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ImagingDocumentService : IImagingDocumentService
	{
		private readonly IImagingDocumentStatusGetByIdStrategy _imagingDocumentStatusGetByIdStrategy;

		public ImagingDocumentService(IImagingDocumentStatusGetByIdStrategy imagingDocumentStatusGetByIdStrategy)
		{
			_imagingDocumentStatusGetByIdStrategy = imagingDocumentStatusGetByIdStrategy;
		}

		public DocumentStatus GetStatus(int workspaceId, int documentArtifactId)
			=> _imagingDocumentStatusGetByIdStrategy.Get(workspaceId, documentArtifactId);
	}
}
