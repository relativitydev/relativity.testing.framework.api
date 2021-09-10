using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ImagingJobSubmitMassDocumentStrategyV1 : IImagingJobSubmitMassDocumentStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;

		public ImagingJobSubmitMassDocumentStrategyV1(
			IRestService restService,
			IWorkspaceIdValidator workspaceIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
		}

		public long SubmitMassDocument(int workspaceId, ImagingMassJobRequest imagingMassJobRequest)
		{
			_workspaceIdValidator.Validate(workspaceId);

			var dto = BuildDto(imagingMassJobRequest);
			var url = BuildUrl(workspaceId);

			var result = _restService.Post<JObject>(url, dto);
			return (long)result["ImagingJobID"];
		}

		private object BuildDto(ImagingMassJobRequest imagingMassJobRequest)
		{
			return new
			{
				MassImageRequest = imagingMassJobRequest
			};
		}

		private string BuildUrl(int workspaceId)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/documents/mass-image";
		}
	}
}
