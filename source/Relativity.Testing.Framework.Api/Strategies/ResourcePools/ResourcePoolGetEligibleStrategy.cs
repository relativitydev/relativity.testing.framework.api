using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies.ResourcePools
{
	[VersionRange(">=12.1")]
	public class ResourcePoolGetEligibleStrategy : IResourcePoolGetEligibleStrategy
	{
		private readonly IRestService _restService;

		public ResourcePoolGetEligibleStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public List<NamedArtifact> GetEligibleResourcePools()
		{
			var result = _restService.Get<List<JObject>>($"Relativity.Workspaces/workspace/eligible-resource-pools");
			ConvertResult(result);
			return new List<NamedArtifact>();
		}

		private void ConvertResult(List<JObject> result)
		{
			throw new NotImplementedException();
		}
	}
}
