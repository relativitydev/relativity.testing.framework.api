using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolGetByIdStrategy : ObjectQueryGetByIdStrategy<ResourcePool>
	{
		public ResourcePoolGetByIdStrategy(IObjectService objectService)
			: base(objectService)
		{
		}
	}
}
