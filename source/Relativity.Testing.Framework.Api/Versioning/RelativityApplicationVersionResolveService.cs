using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Versioning
{
	internal class RelativityApplicationVersionResolveService : IRelativityApplicationVersionResolveService
	{
		private readonly IGetByGuidStrategy<LibraryApplication> _appService;

		public RelativityApplicationVersionResolveService(IRestService restService)
		{
			_appService = new LibraryApplicationGetByGuidStrategyV1(restService);
		}

		public string GetVersion(ApplicationVersionRangeAttribute appVersionInfo)
		{
			return GetVersion(appVersionInfo.ApplicationGuid);
		}

		public string GetVersion(Guid rapGuid)
		{
			LibraryApplication appInfo = _appService.Get(rapGuid);
			return appInfo.Version;
		}
	}
}
