using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class LibraryApplicationGetAllStrategyPreOsier : IGetAllStrategy<LibraryApplication>
	{
		private readonly IRestService _restService;

		private readonly IJsonObjectMappingService _jsonObjectMappingService;

		public LibraryApplicationGetAllStrategyPreOsier(
			IRestService restService,
			IJsonObjectMappingService jsonObjectMappingService)
		{
			_restService = restService;
			_jsonObjectMappingService = jsonObjectMappingService;
		}

		public LibraryApplication[] GetAll()
		{
			var responseObjects = _restService.Get<JObject[]>($"Relativity.LibraryApplications/workspace/-1/libraryapplications");

			var applications = _jsonObjectMappingService.MapTo<LibraryApplication>(responseObjects);

			return applications.ToArray();
		}
	}
}
