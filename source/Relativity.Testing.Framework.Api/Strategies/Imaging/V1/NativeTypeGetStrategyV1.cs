using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class NativeTypeGetStrategyV1 : INativeTypeGetStrategy
	{
		private readonly IRestService _restService;
		private readonly IWorkspaceIdValidator _workspaceIdValidator;
		private readonly IArtifactIdValidator _artifactIdValidator;

		public NativeTypeGetStrategyV1(IRestService restService, IWorkspaceIdValidator workspaceIdValidator, IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_workspaceIdValidator = workspaceIdValidator;
			_artifactIdValidator = artifactIdValidator;
		}

		public NativeType Get(int workspaceId, int nativeTypeID)
		{
			ValidateInput(workspaceId, nativeTypeID);

			var url = BuildUrl(workspaceId, nativeTypeID);

			return _restService.Get<NativeType>(url);
		}

		public async Task<NativeType> GetAsync(int workspaceId, int nativeTypeID)
		{
			ValidateInput(workspaceId, nativeTypeID);

			var url = BuildUrl(workspaceId, nativeTypeID);

			return await _restService.GetAsync<NativeType>(url).ConfigureAwait(false);
		}

		private void ValidateInput(int workspaceId, int nativeTypeID)
		{
			_workspaceIdValidator.Validate(workspaceId);
			_artifactIdValidator.Validate(nativeTypeID, "NativeType");
		}

		private string BuildUrl(int workspaceId, int nativeTypeID)
		{
			return $"relativity-imaging/v1/workspaces/{workspaceId}/native-types/{nativeTypeID}";
		}
	}
}
