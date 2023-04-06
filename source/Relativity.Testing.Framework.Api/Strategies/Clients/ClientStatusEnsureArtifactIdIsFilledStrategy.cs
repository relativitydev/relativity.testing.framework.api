using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientStatusEnsureArtifactIdIsFilledStrategy : IClientStatusEnsureArtifactIdIsFilledStrategy
	{
		private readonly IClientStatusGetChoiceIdByNameStrategy _clientStatusGetChoiceIdByNameStrategy;

		public ClientStatusEnsureArtifactIdIsFilledStrategy(IClientStatusGetChoiceIdByNameStrategy clientStatusGetChoiceIdByNameStrategy)
		{
			_clientStatusGetChoiceIdByNameStrategy = clientStatusGetChoiceIdByNameStrategy;
		}

		public void Ensure(Client client)
		{
			if (client.Status.ArtifactID == 0)
			{
				client.Status.ArtifactID = GetStatusId(client.Status.Name);
			}
		}

		private int GetStatusId(string status)
		{
			return _clientStatusGetChoiceIdByNameStrategy.GetId(status);
		}
	}
}
