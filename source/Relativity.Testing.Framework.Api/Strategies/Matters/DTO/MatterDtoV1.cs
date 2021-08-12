using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterDTOV1 : MatterBaseDtoV1
	{
		public int ArtifactID { get; set; }

		public List<HttpAction> Actions { get; set; }

		public NamedArtifact CreatedBy { get; set; }

		public DateTime CreatedOn { get; set; }

		public NamedArtifact LastModifieddBy { get; set; }

		public DateTime LastModifiedOn { get; set; }
	}
}
