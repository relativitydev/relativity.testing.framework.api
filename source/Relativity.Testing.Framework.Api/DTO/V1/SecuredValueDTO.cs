using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.DTO
{
	internal class SecuredValueDTO
	{
		public bool Secured { get; set; }

		public NamedArtifactWithGuids Value { get; set; }
	}
}
