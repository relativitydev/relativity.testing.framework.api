using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingDocumentStatusGetByIdStrategyV1 : IImagingDocumentStatusGetByIdStrategy
	{
		private readonly IRestService _restService;

		public ImagingDocumentStatusGetByIdStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public DocumentStatus Get(int workspaceId, int documentArtifactId)
		{
			var getDocumentStatusUrl = BuildDocumentStatusUrl(workspaceId, documentArtifactId);
			return _restService.Get<DocumentStatus>(getDocumentStatusUrl);
		}

		private string BuildDocumentStatusUrl(int workspaceId, int documentArtifactId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/documents/{documentArtifactId}/status";
		}
	}
}
