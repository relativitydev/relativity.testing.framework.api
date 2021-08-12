using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal abstract class MatterBaseDtoV1
	{
		public string Name { get; set; }

		public string Number { get; set; }

		public Securable<Artifact> Status { get; set; }

		public Securable<Artifact> Client { get; set; }

		public string Keywords { get; set; }

		public string Notes { get; set; }
	}
}
