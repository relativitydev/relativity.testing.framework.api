using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolGetByNameStrategy : IGetByNameStrategy<ResourcePool>
	{
		private readonly IObjectService _objectService;

		public ResourcePoolGetByNameStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public ResourcePool Get(string name)
		{
			return _objectService.Query<ResourcePool>().
				Where(x => x.Name, name).
				FirstOrDefault();
		}
	}
}
