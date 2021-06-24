using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IClientStatusEnsureArtifactIdIsFilledStrategy
	{
		void Ensure(Client client);
	}
}
