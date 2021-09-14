using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class ClientGetByIdStrategyPreOsier : ObjectQueryGetByIdStrategy<Client>
	{
		public ClientGetByIdStrategyPreOsier(IObjectService objectService)
			: base(objectService)
		{
		}
	}
}
