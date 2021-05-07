using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class LibraryApplicationGetAllStrategyV1 : IGetAllStrategy<LibraryApplication>
	{
		private readonly IRestService _restService;

		private readonly IJsonObjectMappingService _jsonObjectMappingService;

		public LibraryApplicationGetAllStrategyV1(
			IRestService restService,
			IJsonObjectMappingService jsonObjectMappingService)
		{
			_restService = restService;
			_jsonObjectMappingService = jsonObjectMappingService;
		}

		public LibraryApplication[] GetAll()
		{
			var responseObjects = _restService.Get<JObject[]>($"relativity-environment/v1/workspace/-1/libraryapplications");

			var applications = _jsonObjectMappingService.MapTo<LibraryApplication>(responseObjects);

			return applications.ToArray();
		}
	}
}
