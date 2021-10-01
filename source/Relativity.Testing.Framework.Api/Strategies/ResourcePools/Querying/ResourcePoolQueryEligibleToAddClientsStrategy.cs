using System;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Querying;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolQueryEligibleToAddClientsStrategy : QueryStrategy<ResourcePoolQueryRequest>, IQueryEligibleToAddClientsStrategy
	{
		public ResourcePoolQueryEligibleToAddClientsStrategy(IRestService restService, IObjectMappingService objectMappingService)
			: base(restService, objectMappingService)
		{
		}

		public ResourcePoolQuery<Client> Query()
		{
			ResourcePoolQueryRequest request = new ResourcePoolQueryRequest();

			IQueryExecutor<Client> executor = new QueryExecutor<Client, ResourcePoolQueryRequest>(QuerySlimAndMap<Client>);

			return new ResourcePoolQuery<Client>(request, executor);
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

			return RestService.Post<QuerySlimResult>($"relativity.resourcepools/workspace/{request.WorkspaceId}/resource-pools/query-eligible-clients", wrapper);
		}
	}
}
