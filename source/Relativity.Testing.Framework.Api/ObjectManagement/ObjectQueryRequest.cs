using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Relativity.Testing.Framework.Api.Querying;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	internal class ObjectQueryRequest : QueryRequestBase
	{
		public ObjectQueryRequest(int artifactTypeId)
		{
			ObjectType = new QueryObjectType { ArtifactTypeID = artifactTypeId };
		}

		public ObjectQueryRequest(string objectTypeName)
		{
			ObjectType = new QueryObjectType { Name = objectTypeName };
		}

		public ObjectQueryRequest(Guid objectGuid)
		{
			ObjectType = new QueryObjectType { Guid = objectGuid };
		}

		[JsonIgnore]
		public int WorkspaceId { get; set; } = -1;

		[JsonProperty]
		public QueryObjectType ObjectType { get; set; }

		public List<QueryField> Fields { get; private set; } = new List<QueryField>();

		public static ObjectQueryRequest Of(ArtifactType type)
		{
			return Of((int)type);
		}

		public static ObjectQueryRequest Of(int artifactTypeId)
		{
			return new ObjectQueryRequest(artifactTypeId);
		}

		public static ObjectQueryRequest Of(string objectTypeName)
		{
			return new ObjectQueryRequest(objectTypeName);
		}

		public static ObjectQueryRequest Of(Guid objectGuid)
		{
			return new ObjectQueryRequest(objectGuid);
		}

		public override void SetFieldsToFetch(IEnumerable<string> fieldNames)
		{
			Fields = new List<QueryField>();

			foreach (string fieldName in fieldNames)
			{
				Guid guid;
				int artifactId;

				if (Guid.TryParse(fieldName, out guid))
				{
					Fields.Add(new QueryField { Guid = guid });
				}
				else if (int.TryParse(fieldName, out artifactId))
				{
					Fields.Add(new QueryField { ArtifactID = artifactId });
				}
				else
				{
					Fields.Add(new QueryField { Name = fieldName });
				}
			}
		}
	}
}
