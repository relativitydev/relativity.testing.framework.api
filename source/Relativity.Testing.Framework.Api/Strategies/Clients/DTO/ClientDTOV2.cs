using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientDTOV2
	{
		public ClientDTOV2(Client client)
		{
			ClientRequest = new ClientRequest(client);
		}

		public ClientRequest ClientRequest { get; set; }
	}
}
