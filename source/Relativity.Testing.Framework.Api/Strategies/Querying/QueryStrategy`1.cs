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
	internal abstract class QueryStrategy<TQueryRequest>
		where TQueryRequest : QueryRequestBase, new()
	{
		protected QueryStrategy(IRestService restService, IObjectMappingService objectMappingService)
		{
			RestService = restService;
			ObjectMappingService = objectMappingService;
		}

		protected IRestService RestService { get; }

		protected IObjectMappingService ObjectMappingService { get; }

		protected IEnumerable<T> QuerySlimAndMap<T>(TQueryRequest request)
		{
			QuerySlimResult result = QuerySlim(request);

			return result.Objects.Select(x => MapObject<T>(x, result.Fields));
		}

		protected abstract QuerySlimResult QuerySlim(TQueryRequest request);

		protected T MapObject<T>(QuerySlimObject queryObject, IEnumerable<QueryResultField> fields)
		{
			T destination = Activator.CreateInstance<T>();

			MapObject(queryObject, destination, fields);

			return destination;
		}

		protected void MapObject<T>(QuerySlimObject queryObject, T destination, IEnumerable<QueryResultField> fields)
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

			ObjectMappingService.Map(propertiesMap, destination);
		}
	}
}
