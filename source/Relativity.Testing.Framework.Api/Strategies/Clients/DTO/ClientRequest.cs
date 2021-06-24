using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientRequest
	{
		public ClientRequest(Client client)
		{
			Name = client.Name;
			Number = client.Number;
			Status = client.Status;
			Keywords = client.Keywords;
			Notes = client.Notes;
		}

		public string Name { get; set; }

		public string Number { get; set; }

		public NamedArtifact Status { get; set; }

		public string Keywords { get; set; }

		public string Notes { get; set; }
	}
}
