using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientRequireStrategy : NamedArtifactRequireStrategy<Client>
	{
		public ClientRequireStrategy(
			ICreateStrategy<Client> createStrategy,
			IGetByNameStrategy<Client> getByNameStrategy,
			IGetByIdStrategy<Client> getByIdStrategy)
			: base(createStrategy, getByNameStrategy, getByIdStrategy)
		{
		}
	}
}
