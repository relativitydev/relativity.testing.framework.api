using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.DTO
{
	internal class ViewDTO
	{
		public int ArtifactID { get; set; }

		public int ArtifactTypeID { get; set; }

		public SecuredValueDTO ObjectType { get; set; }

		public int Order { get; set; }

		public bool VisibleInDropdown { get; set; }

		public string QueryHint { get; set; }

		public List<NamedArtifact> RelativityApplications { get; set; }

		public SecuredValueDTO Owner { get; set; }

		public string Name { get; set; }

		public NamedArtifact[] Fields { get; set; }

		public List<Sort> Sorts { get; set; }

		public NamedArtifact Dashboard { get; set; }

		public string GroupDefinitionFieldArtifactID { get; set; }

		public CriteriaCollection SearchCriteria { get; set; }
	}
}
