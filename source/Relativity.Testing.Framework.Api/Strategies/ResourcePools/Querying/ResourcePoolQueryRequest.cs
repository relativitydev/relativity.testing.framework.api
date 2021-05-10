using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Querying;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolQueryRequest : QueryRequestBase
	{
		public ResourcePoolQueryRequest()
		{
		}

		public ResourcePoolQueryRequest(int resourcePoolId, string resourceType = null)
		{
			ResourcePoolId = resourcePoolId;
			ResourceType = resourceType;
		}

		[JsonIgnore]
		public int WorkspaceId { get; set; } = -1;

		[JsonIgnore]
		public int ResourcePoolId { get; set; }

		[JsonIgnore]
		public string ResourceType { get; set; }

		public List<QueryField> Fields { get; private set; } = new List<QueryField>();

		public override void SetFieldsToFetch(IEnumerable<string> fieldNames)
		{
			Fields = fieldNames.Select(name => new QueryField { Name = name }).ToList();
		}
	}
}
