using Relativity.Testing.Framework.Api.Extensions;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ProductionPlaceholderGetDefaultFieldValuesStrategyV1 : IProductionPlaceholderGetDefaultFieldValuesStrategy
	{
		private readonly IRestService _restService;

		public ProductionPlaceholderGetDefaultFieldValuesStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public DefaultFieldValue<NamedArtifact> Get(int workspaceArtifactID)
		{
			var dto = _restService.Get<ProductionPlaceholderDefaultFieldValuesDto>($"relativity-productions/v1/workspaces/{workspaceArtifactID}/production-placeholders/defaults");

			return dto.MapToDefaultFieldValue();
		}
	}
}
