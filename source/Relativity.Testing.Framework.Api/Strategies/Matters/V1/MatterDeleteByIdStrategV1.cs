using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class MatterDeleteByIdStrategyV1 : DeleteByIdStrategy<Matter>
	{
		private readonly IRestService _restService;

		private readonly IArtifactIdValidator _artifactIdValidator;

		public MatterDeleteByIdStrategyV1(IRestService restService, IArtifactIdValidator artifactIdValidator)
		{
			_restService = restService;
			_artifactIdValidator = artifactIdValidator;
	}

		protected override void DoDelete(int id)
		{
			_artifactIdValidator.Validate(id, "Matter");
			_restService.Delete(
				$"relativity-environment/v1/workspaces/-1/matters/{id}");
		}
	}
}
