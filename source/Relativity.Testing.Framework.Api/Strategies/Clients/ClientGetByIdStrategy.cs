using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ClientGetByIdStrategy : ObjectQueryGetByIdStrategy<Client>
	{
		public ClientGetByIdStrategy(IObjectService objectService)
			: base(objectService)
		{
		}
	}
}
