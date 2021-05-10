using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Querying;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolQueryResourcesStrategy : IQueryResourcesStrategy
	{
		private readonly IRestService _restService;
		private readonly IObjectMappingService _objectMappingService;

		public ResourcePoolQueryResourcesStrategy(IRestService restService, IObjectMappingService objectMappingService)
		{
			_restService = restService;
			_objectMappingService = objectMappingService;
		}

		public ResourcePoolQuery<ResourceServer> Query(int resourcePoolId, ResourceType resourceType = ResourceType.AgentWorkerServers)
		{
			var resourceTypeAsString = ChoiceNameToEnumMapper.GetName(resourceType);
			ResourcePoolQueryRequest request = new ResourcePoolQueryRequest(resourcePoolId, resourceTypeAsString);

			IQueryExecutor<ResourceServer> executor = new QueryExecutor<ResourceServer, ResourcePoolQueryRequest>(QuerySlimAndMap<ResourceServer>);

			return new ResourcePoolQuery<ResourceServer>(request, executor);
		}

		private IEnumerable<TObject> QuerySlimAndMap<TObject>(ResourcePoolQueryRequest request)
		{
			QuerySlimResult result = QuerySlim(request);

			return result.Objects.Select(x => MapObject<TObject>(x, result.Fields));
		}

		private QuerySlimResult QuerySlim(ResourcePoolQueryRequest request)
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

			return _restService.Post<QuerySlimResult>($"relativity.resourcepools/workspace/{request.WorkspaceId}/resource-pools/{request.ResourcePoolId}/{request.ResourceType}/query-associated", wrapper);
		}

		private T MapObject<T>(QuerySlimObject queryObject, IEnumerable<QueryResultField> fields)
		{
			T destination = Activator.CreateInstance<T>();

			MapObject(queryObject, destination, fields);

			return destination;
		}

		private void MapObject<T>(QuerySlimObject queryObject, T destination, IEnumerable<QueryResultField> fields)
		{
			Dictionary<string, object> propertiesMap = fields.
				Select((x, index) => new
				{
					Name = ObjectFieldMapping.GetPropertyNameOrNull<T>(x.Name),
					Value = queryObject.Values[index]
				}).
				Where(x => x.Name != null).
				ToDictionary(x => x.Name, x => x.Value);

			if (!propertiesMap.Any(x => x.Key == nameof(Artifact.ArtifactID)) && queryObject.ArtifactID > 0)
			{
				propertiesMap.Add(nameof(Artifact.ArtifactID), queryObject.ArtifactID);
			}

			_objectMappingService.Map(propertiesMap, destination);
		}
	}
}
