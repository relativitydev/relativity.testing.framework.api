using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class GetMassImagingJobSizeStrategyV1 : IGetMassImagingJobSizeStrategy
	{
		private readonly IRestService _restService;

		public GetMassImagingJobSizeStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public int Get()
		{
			var url = BuildUrl();
			return _restService.Get<int>(url);
		}

		private string BuildUrl()
		{
			return $"relativity-imaging/v1/environment/jobs/mass-imaging-max-size";
		}
	}
}
