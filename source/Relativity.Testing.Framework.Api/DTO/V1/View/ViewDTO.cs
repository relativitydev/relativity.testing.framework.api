using System.Collections.Generic;
using Newtonsoft.Json;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.DTO
{
	internal class ViewDTO
	{
		public NamedArtifact ObjectIdentifier { get; set; }

		public int ArtifactID { get; set; }

		public int ArtifactTypeID { get; set; }

		public Securable<NamedArtifact> ObjectType { get; set; }

		public int Order { get; set; }

		public bool VisibleInDropdown { get; set; }

		public string QueryHint { get; set; }

		public List<NamedArtifact> RelativityApplicationsList { get; set; }

		public Securable<NamedArtifact> Owner { get; set; }

		public string Name { get; set; }

		public NamedArtifact[] Fields { get; set; }

		public List<Sort> Sorts { get; set; }

		public NamedArtifact Dashboard { get; set; }

		public string GroupDefinitionFieldArtifactID { get; set; }

		public CriteriaCollection SearchCriteria { get; set; }
	}
}
