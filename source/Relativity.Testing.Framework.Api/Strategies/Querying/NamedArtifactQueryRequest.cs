using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Api.Querying;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class NamedArtifactQueryRequest : QueryRequestBase
	{
		public NamedArtifactQueryRequest()
		{
			Length = 100;
		}

		public NamedArtifactQueryRequest(int userArtifactId)
		{
			UserArtifactId = userArtifactId;
			Length = 100;
		}

		[JsonIgnore]
		public int UserArtifactId { get; set; }

		public List<QueryField> Fields { get; private set; } = new List<QueryField>();

		public override void SetFieldsToFetch(IEnumerable<string> fieldNames)
		{
			Fields = fieldNames.Select(name => new QueryField { Name = name }).ToList();
		}
	}
}
