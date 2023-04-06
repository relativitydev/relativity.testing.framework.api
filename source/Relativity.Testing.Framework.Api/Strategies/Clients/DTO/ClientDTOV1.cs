using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientDTOV1
	{
		public ClientDTOV1(Client client)
		{
			ClientRequest = new ClientRequest(client);
		}

		public ClientRequest ClientRequest { get; set; }
	}
}
