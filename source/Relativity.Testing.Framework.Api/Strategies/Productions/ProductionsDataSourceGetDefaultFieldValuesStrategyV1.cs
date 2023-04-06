using Relativity.Testing.Framework.Api.DTO;
using Relativity.Testing.Framework.Api.Extensions;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class ProductionsDataSourceGetDefaultFieldValuesStrategyV1 : IProductionsDataSourceGetDefaultFieldValuesStrategy
	{
		private readonly IRestService _restService;

		public ProductionsDataSourceGetDefaultFieldValuesStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public ProductionDataSourceDefaultValues Get(int workspaceArtifactID)
		{
			var dto = _restService.Get<ProductionsDataSourceDefaultFieldValuesDto>($"relativity-productions/V1/workspaces/{workspaceArtifactID}/production-data-sources/defaults");

			return dto.MapToDefaultFieldValue();
		}
	}
}
