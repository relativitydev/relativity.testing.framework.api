using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class LibraryApplicationGetByGuidStrategyV1 : IGetByGuidStrategy<LibraryApplication>
	{
		private readonly IRestService _restService;

		public LibraryApplicationGetByGuidStrategyV1(
			IRestService restService)
		{
			_restService = restService;
		}

		public LibraryApplication Get(Guid identifier)
		{
			return _restService.Get<LibraryApplication>($"relativity-environment/v1/workspace/-1/libraryapplications/{identifier}");
		}
	}
}
