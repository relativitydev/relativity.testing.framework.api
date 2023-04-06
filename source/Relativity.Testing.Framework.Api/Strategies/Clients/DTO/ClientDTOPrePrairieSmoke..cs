using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientDTOPrePrairieSmoke
	{
		public ClientDTOPrePrairieSmoke(Client client)
		{
			ClientDTO = new ClientRequest(client);
		}

		public ClientRequest ClientDTO { get; set; }
	}
}
