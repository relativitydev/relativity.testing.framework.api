﻿using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingJobSubmitSingleDocumentStrategyV1 : IImagingJobSubmitSingleDocumentStrategy
	{
		private readonly IRestService _restService;
		private readonly IArtifactIdValidator _artifactIdValidator;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public ImagingJobSubmitSingleDocumentStrategyV1(
			IRestService restService,
			IArtifactIdValidator artifactIdValidator,
			IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_artifactIdValidator = artifactIdValidator;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public long SubmitSingleDocument(int workspaceId, int documentArtifactId, ImagingJobSubmitSingleDocumentRequest imagingJobSubmitSingleDocumentRequest)
		{
			_workspaceIdValidator.Validate(workspaceId);
			_artifactIdValidator.Validate(documentArtifactId, "Document");

			var dto = BuildDto(imagingJobSubmitSingleDocumentRequest);
			var url = BuildUrl(workspaceId, documentArtifactId);

			var result = _restService.Post<JObject>(url, dto);
			return (long)result["ImagingJobID"];
		}

		public async Task<long> SubmitSingleDocumentAsync(int workspaceId, int documentArtifactId, ImagingJobSubmitSingleDocumentRequest imagingJobSubmitSingleDocumentRequest)
		{
			_workspaceIdValidator.Validate(workspaceId);
			_artifactIdValidator.Validate(documentArtifactId, "Document");

			var dto = BuildDto(imagingJobSubmitSingleDocumentRequest);
			var url = BuildUrl(workspaceId, documentArtifactId);

			var result = await _restService.PostAsync<JObject>(url, dto).ConfigureAwait(false);
			return (long)result["ImagingJobID"];
		}

		private object BuildDto(ImagingJobSubmitSingleDocumentRequest imagingJobSubmitSingleDocumentRequest)
		{
			return new
			{
				ImageOnTheFlyRequest = imagingJobSubmitSingleDocumentRequest
			};
		}

		private string BuildUrl(int workspaceId, int documentArtifactId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/documents/{documentArtifactId}/image";
		}
	}
}
