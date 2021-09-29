﻿using System;
using System.Collections.Generic;
using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class UserGetGroupsStrategy : IUserGetGroupsStrategy
	{
		private readonly IRestService _restService;
		private readonly IObjectMappingService _objectMappingService;

		public UserGetGroupsStrategy(IRestService restService, IObjectMappingService objectMappingService)
		{
			_restService = restService;
			_objectMappingService = objectMappingService;
		}

		public IList<NamedArtifact> GetGroups(int userId)
		{
			var request = new
			{
				request = new
				{
					Fields = new[] { new { Name = "Name" } },
				},
				start = 1,
				length = 1000
			};

			var result = _restService.Post<QuerySlimResult>($"Relativity.groups/workspace/-1/groups/query-by-user/{userId}", request);
			var groups = result.Objects.Select(x => MapObject<NamedArtifact>(x, result.Fields)).ToList();

			return groups;
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
