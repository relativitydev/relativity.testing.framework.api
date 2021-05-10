using System.Linq;
using Relativity.Testing.Framework.Api.ObjectManagement;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class ResourcePoolGetAllStrategy : IGetAllStrategy<ResourcePool>
	{
		private readonly IObjectService _objectService;

		public ResourcePoolGetAllStrategy(IObjectService objectService)
		{
			_objectService = objectService;
		}

		public ResourcePool[] GetAll()
		{
			return _objectService.Query<ResourcePool>().ToArray();
		}
	}
}
