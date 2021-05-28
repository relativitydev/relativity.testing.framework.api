using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Querying;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	internal class ObjectService : IObjectService
	{
		private readonly IRestService _restService;

		private readonly IObjectMappingService _objectMappingService;

		public ObjectService(IRestService restService, IObjectMappingService objectMappingService)
		{
			_restService = restService;
			_objectMappingService = objectMappingService;
		}

		public void Delete(int workspaceId, int entityId)
		{
			var dto = new
			{
				Request = new
				{
					Object = new
					{
						ArtifactID = entityId
					}
				}
			};

			_restService.Post<string>($"Relativity.Objects/workspace/{workspaceId}/object/delete", dto);
		}

		public void Delete(int workspaceId, IEnumerable<int> entityIds)
		{
			var dto = new
			{
				massRequestByObjectIdentifiers = new
				{
					Objects = entityIds.Select(x => new Artifact { ArtifactID = x })
				}
			};

			_restService.Post<string>($"Relativity.Objects/workspace/{workspaceId}/object/delete", dto);
		}

		public TObject[] GetAll<TObject>()
		{
			return Query<TObject>().ToArray();
		}

		public TObject[] GetAll<TObject>(Expression<Func<TObject, object>> wherePropertySelector, object whereValue)
		{
			return Query<TObject>().Where(wherePropertySelector, whereValue).ToArray();
		}

		public ObjectQuery<TObject> Query<TObject>()
		{
			ObjectQueryRequest request = null;
			IQueryExecutor<TObject> executor = null;

			Guid guid = ObjectTypeGuidResolver.Resolve<TObject>();
			if (guid != Guid.Empty)
			{
				request = ObjectQueryRequest.Of(guid);
			}
			else
			{
				string objectTypeName = ObjectTypeNameResolver.Resolve<TObject>();
				request = ObjectQueryRequest.Of(objectTypeName);
			}

			executor = new QueryExecutor<TObject, ObjectQueryRequest>(QuerySlimAndMap<TObject>);

			return new ObjectQuery<TObject>(request, executor);
		}

		public TObject Create<TObject>(int workspaceId, TObject entity)
			where TObject : Artifact
		{
			string objectTypeName = ObjectTypeNameResolver.Resolve<TObject>();

			var properties = ObjectFieldMapping.GetPropertyNames(typeof(TObject), new ObjectFieldMappingOptions { OnlyReadableSkip = true, UseCapitalized = true });

			var fieldValues = properties.
				Where(x => x != nameof(Artifact.ArtifactID) && x != "ParentObject").
				Select(x => new
				{
					Field = new
					{
						Name = ObjectFieldMapping.GetFieldName<TObject>(x)
					},
					Value = typeof(TObject).GetProperty(x).GetValue(entity)
				}).
				Where(x => x.Value != null).
				ToList();

			var dto = new
			{
				request = new
				{
					ObjectType = new
					{
						Name = objectTypeName
					},
					ParentObject = JObject.FromObject(entity)["ParentObject"],
					FieldValues = fieldValues
				}
			};

			var result = _restService.Post<JObject>($"Relativity.Objects/workspace/{workspaceId}/object/create", dto);
			var artifactId = (int)result["Object"]["ArtifactID"];
			return Query<TObject>().
				For(workspaceId).
				Where(x => x.ArtifactID, artifactId).
				FirstOrDefault();
		}

		public List<int> Create<TObject>(int workspaceId, IEnumerable<TObject> entities)
				where TObject : Artifact
		{
			string objectTypeName = ObjectTypeNameResolver.Resolve<TObject>();

			var properties = ObjectFieldMapping.GetPropertyNames(typeof(TObject), new ObjectFieldMappingOptions { OnlyReadableSkip = true, UseCapitalized = true });

			var fields = properties.
				Where(x => x != nameof(Artifact.ArtifactID) && x != "ParentObject").
				Select(x => new
				{
					Name = ObjectFieldMapping.GetFieldName<TObject>(x)
				}).
				ToList();

			var values = new List<List<object>>();

			foreach (var entity in entities)
			{
				var value = properties.
				Where(x => x != nameof(Artifact.ArtifactID) && x != "ParentObject").
				Select(x => typeof(TObject).GetProperty(x).GetValue(entity)).
				ToList();

				values.Add(value);
			}

			var dto = new
			{
				massRequest = new
				{
					ObjectType = new
					{
						Name = objectTypeName
					},
					ParentObject = JObject.FromObject(entities.First())["ParentObject"],
					Fields = fields,
					ValueLists = values
				}
			};

			var result = _restService.Post<JObject>($"Relativity.Objects/workspace/{workspaceId}/object/create", dto);
			var artifactIds = result["Objects"].Select(x => x.ToObject<Artifact>().ArtifactID).ToList();
			return artifactIds;
		}

		public void Update<TObject>(int workspaceId, TObject entity)
			where TObject : Artifact
		{
			var properties = ObjectFieldMapping.GetPropertyNames(typeof(TObject), new ObjectFieldMappingOptions { OnlyReadableSkip = true, UseCapitalized = true });

			var fieldValues = properties.
				Where(x => x != nameof(Artifact.ArtifactID) && x != "ParentObject").
				Select(x => new
				{
					Field = new
					{
						Name = ObjectFieldMapping.GetFieldName<TObject>(x)
					},
					Value = typeof(TObject).GetProperty(x).GetValue(entity)
				}).
				ToList();

			var dto = new
			{
				request = new
				{
					Object = new
					{
						artifactId = entity.ArtifactID
					},
					FieldValues = fieldValues
				}
			};

			_restService.Post<string>($"Relativity.Objects/workspace/{workspaceId}/object/update", dto);
		}

		public void Update(int workspaceId, MassUpdateByObjectIdentifiersRequest massRequestByObjectIdentifiers)
		{
			var dto = new
			{
				massRequestByObjectIdentifiers
			};

			_restService.Post<string>($"Relativity.Objects/workspace/{workspaceId}/object/update", dto);
		}

		private IEnumerable<TObject> QuerySlimAndMap<TObject>(ObjectQueryRequest request)
		{
			QuerySlimResult result = QuerySlim(request);

			return result.Objects.Select(x => MapObject<TObject>(x, result.Fields));
		}

		private QuerySlimResult QuerySlim(ObjectQueryRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			ObjectQueryRequestWrapper wrapper = new ObjectQueryRequestWrapper
			{
				Request = request,
				Start = request.Start,
				Length = request.Length
			};

			return _restService.Post<QuerySlimResult>($"Relativity.Objects/workspace/{request.WorkspaceId}/object/queryslim", wrapper);
		}

		private T MapObject<T>(QuerySlimObject queryObject, IEnumerable<QueryResultField> fields)
		{
			T destination = Activator.CreateInstance<T>();

			MapObject(queryObject, destination, fields);

			return destination;
		}

		internal void MapObject<T>(QuerySlimObject queryObject, T destination, IEnumerable<QueryResultField> fields)
		{
			Dictionary<string, object> propertiesMap = new Dictionary<string, object>();

			for (int i = 0; i < fields.Count(); i++)
			{
				List<string> identifiers = new List<string>();
				fields.ElementAt(i).Guids?.ForEach(x => identifiers.Add(x.ToString()));
				identifiers.Add(fields.ElementAt(i).ArtifactID.ToString());
				identifiers.Add(fields.ElementAt(i).Name);

				string name = identifiers.FirstOrDefault(x => ObjectFieldMapping.GetPropertyNameOrNull<T>(x) != null);
				if (name != null)
				{
					name = ObjectFieldMapping.GetPropertyNameOrNull<T>(name);
					var value = queryObject.Values[i];
					propertiesMap.Add(name, value);
				}
			}

			if (!propertiesMap.Any(x => x.Key == nameof(Artifact.ArtifactID)) && queryObject.ArtifactID > 0)
			{
				propertiesMap.Add(nameof(Artifact.ArtifactID), queryObject.ArtifactID);
			}

			_objectMappingService.Map(propertiesMap, destination);
		}
	}
}
