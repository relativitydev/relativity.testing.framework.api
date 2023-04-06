using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ApplicationFieldCodeCreateRequest
	{
		public string FieldCode { get; set; }

		public ApplicationType Application { get; set; }

		public ApplicationFieldCodeOption Option { get; set; }

		public NamedArtifactWithGuids RelativityField { get; set; }

		public IEnumerable<NamedArtifact> ImagingProfiles { get; set; }
	}
}
