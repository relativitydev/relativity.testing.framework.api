using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class MotdIsTextOnlyStrategyV1 : IMotdIsTextOnlyStrategy
	{
		private const string _GET_URL = "relativity-infrastructure/v1/workspaces/-1/notifications/is-text-only";
		private readonly IRestService _restService;

		public MotdIsTextOnlyStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public bool IsTextOnly()
		{
			return _restService.Get<bool>(_GET_URL);
		}
	}
}
