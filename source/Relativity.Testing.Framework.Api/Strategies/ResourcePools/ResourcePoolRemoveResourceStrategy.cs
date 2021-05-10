using System.Collections.Generic;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolRemoveResourceStrategy : IResourcePoolRemoveResourceStrategy
	{
		private readonly IRestService _restService;

		public ResourcePoolRemoveResourceStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void Remove(int resourcePoolId, ResourceType resourceType, List<Artifact> resources)
		{
			var dto = new
			{
				resources
			};

			_restService.Post($"relativity.resourcepools/workspace/-1/resource-pools/{resourcePoolId}/{ChoiceNameToEnumMapper.GetName(resourceType)}/remove", dto);
		}
	}
}
