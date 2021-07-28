using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.Layouts.DTO
{
	internal class LayoutRelativityApplicationsDTOV1
	{
		public bool HasSecuredItems { get; set; }

		public List<NamedArtifact> ViewableItems { get; set; }
	}
}
