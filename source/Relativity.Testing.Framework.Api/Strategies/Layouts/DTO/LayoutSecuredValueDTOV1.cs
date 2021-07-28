using System;
using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.Layouts.DTO
{
	internal class LayoutSecuredValueDTOV1
	{
		public bool Secured { get; set; }

		public NamedArtifactWithGuids Value { get; set; }
	}
}
