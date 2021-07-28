using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.Strategies.Layouts.DTO
{
	internal class LayoutActionDTOV1
	{
		public string Name { get; set; }

		public string Href { get; set; }

		public string Verb { get; set; }

		public bool IsAvailable { get; set; }

		public List<string> Reason { get; set; }
	}
}
