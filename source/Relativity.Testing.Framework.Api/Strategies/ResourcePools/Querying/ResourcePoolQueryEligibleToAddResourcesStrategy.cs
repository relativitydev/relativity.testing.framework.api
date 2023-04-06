using System;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Querying;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolQueryEligibleToAddResourcesStrategy : QueryStrategy<ResourcePoolQueryRequest>, IQueryEligibleToAddResourcesStrategy
	{
		public ResourcePoolQueryEligibleToAddResourcesStrategy(IRestService restService, IObjectMappingService objectMappingService)
			: base(restService, objectMappingService)
		{
		}

		public ResourcePoolQuery<ResourceServer> Query(int resourcePoolId, ResourceType resourceType = ResourceType.AgentWorkerServers)
		{
			var resourceTypeAsString = ChoiceNameToEnumMapper.GetName(resourceType);
			ResourcePoolQueryRequest request = new ResourcePoolQueryRequest(resourcePoolId, resourceTypeAsString);

			IQueryExecutor<ResourceServer> executor = new QueryExecutor<ResourceServer, ResourcePoolQueryRequest>(QuerySlimAndMap<ResourceServer>);

			return new ResourcePoolQuery<ResourceServer>(request, executor);
		}

		protected override QuerySlimResult QuerySlim(ResourcePoolQueryRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			var wrapper = new
			{
				Request = request,
				request.Start,
				request.Length
			};

			return RestService.Post<QuerySlimResult>($"relativity.resourcepools/workspace/{request.WorkspaceId}/resource-pools/{request.ResourcePoolId}/query-eligible-{request.ResourceType}-to-add", wrapper);
		}
	}
}
